using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mudemy.Core.DTOs;
using Mudemy.Core.Models;
using Mudemy.Core.Repositories;
using Mudemy.Core.Services;
using Mudemy.Core.UnitOfWork;
using SharedLibrary.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Mudemy.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;

         private readonly UserManager<UserApp> _userManager; 

        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IUnitOfWork unitOfWork, IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository, UserManager<UserApp> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            var currentUser = claimsPrincipal != null ? await _userManager.GetUserAsync(claimsPrincipal) : null;
            if (currentUser == null)
            {
                return Response<IEnumerable<OrderDto>>.Fail("Unauthorized", 401, true);
            }

            var orders = await _orderRepository.Where(o => o.UserId == currentUser.Id)
                                            .Include(o => o.OrderDetails)
                                            .ThenInclude(od => od.Course)
                                            .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return Response<IEnumerable<OrderDto>>.Fail("No orders found", 404, true);
            }

            var orderDtos = ObjectMapper.Mapper.Map<IEnumerable<OrderDto>>(orders);

            return Response<IEnumerable<OrderDto>>.Success(orderDtos, 201);
        }

        public async Task<Response<OrderDto>> GetOrderDetailsByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Response<OrderDto>.Fail("Invalid Order ID", 400, true);
            }

            var order = await _orderRepository.Where(o => o.Id == id)
                                            .Include(o => o.OrderDetails)
                                            .ThenInclude(od => od.Course)
                                            .FirstOrDefaultAsync();

            if (order == null)
            {
                return Response<OrderDto>.Fail("Order not found", 404, true);
            }

            var orderDto = ObjectMapper.Mapper.Map<OrderDto>(order);

            return Response<OrderDto>.Success(orderDto, 201);
        }

        public async Task<Response<CreateOrderDto>> PlaceOrderAsync(CreateOrderDto createOrderDto)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            var currentUser = claimsPrincipal != null ? await _userManager.GetUserAsync(claimsPrincipal) : null;
            if (currentUser == null)
            {
                return Response<CreateOrderDto>.Fail("Unauthorized", 401, true);
            }

            if (createOrderDto == null || createOrderDto.CourseIds == null || !createOrderDto.CourseIds.Any())
            {
                return Response<CreateOrderDto>.Fail("Invalid order details", 400, true);
            }

            var order = new Order
            {
                UserId = currentUser.Id,
                OrderDate = DateTime.Now,
                TotalPrice = 0 
            };

            var orderDetails = new List<OrderDetail>();
            foreach (var courseId in createOrderDto.CourseIds)
            {
                var course = await _unitOfWork.CourseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    return Response<CreateOrderDto>.Fail($"Course with ID {courseId} not found", 404, true);
                }


                var orderDetail = new OrderDetail
                {
                    CourseId = course.Id,
                };

                orderDetails.Add(orderDetail);
                order.TotalPrice += course.Price; 
            }

            await _orderRepository.AddAsync(order);
            await _unitOfWork.CommmitAsync();

            foreach (var detail in orderDetails)
            {
                detail.OrderId = order.Id; 
                await _orderDetailRepository.AddAsync(detail);
            }

            await _unitOfWork.CommmitAsync();

            return Response<CreateOrderDto>.Success(createOrderDto, 201);
        }
    }
}
