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

namespace Mudemy.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;

        public OrderService(IUnitOfWork unitOfWork, IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<Response<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Response<IEnumerable<OrderDto>>.Fail("User ID is required", 400, true);
            }

            if (!int.TryParse(userId, out int userIdInt))
            {
                return Response<IEnumerable<OrderDto>>.Fail("Invalid User ID format", 400, true);
            }

            var orders = await _orderRepository.Where(o => o.UserId == userIdInt.ToString())
                                            .Include(o => o.OrderDetails)
                                            .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return Response<IEnumerable<OrderDto>>.Fail("No orders found", 404, true);
            }

            var orderDtos = ObjectMapper.Mapper.Map<IEnumerable<OrderDto>>(orders);

            return Response<IEnumerable<OrderDto>>.Success(orderDtos, 200);
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

            return Response<OrderDto>.Success(orderDto, 200);
        }

        public async Task<Response<NoDataDto>> PlaceOrderAsync(CreateOrderDto createOrderDto)
        {
            if (createOrderDto == null || createOrderDto.CourseIds == null || !createOrderDto.CourseIds.Any())
            {
                return Response<NoDataDto>.Fail("Invalid order details", 400, true);
            }

            var order = new Order
            {
                UserId = createOrderDto.UserId,
                OrderDate = DateTime.Now,
                TotalPrice = 0 
            };

            var orderDetails = new List<OrderDetail>();
            foreach (var courseId in createOrderDto.CourseIds)
            {
                var course = await _unitOfWork.CourseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    return Response<NoDataDto>.Fail($"Course with ID {courseId} not found", 404, true);
                }

                var orderDetail = new OrderDetail
                {
                    CourseId = course.Id,
                    Course = course,
                };

                orderDetails.Add(orderDetail);
                order.TotalPrice += course.Price; 
            }

            await _orderRepository.AddAsync(order);
            foreach (var detail in orderDetails)
            {
                detail.OrderId = order.Id; 
                await _orderDetailRepository.AddAsync(detail);
            }

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(201);
        }
    }
}
