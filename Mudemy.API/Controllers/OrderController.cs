using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Mudemy.Core.DTOs;
using Mudemy.Core.Services;
using SharedLibrary.Dtos;

namespace Mudemy.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : CustomBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var userClaims = User.Claims;

            var userId = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return ActionResultInstance(Response<IEnumerable<OrderDto>>.Fail(new ErrorDto("User ID is required", true), 400));
            }

            return ActionResultInstance(await _orderService.GetOrdersByUserIdAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            return ActionResultInstance(await _orderService.GetOrderDetailsByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto createOrderDto)
        {
            var userClaims = User.Claims;
            var userId = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return ActionResultInstance(Response<NoDataDto>.Fail(new ErrorDto("User ID is required", true), 400));
            }

            createOrderDto.UserId = userId;

            return ActionResultInstance(await _orderService.PlaceOrderAsync(createOrderDto));
        }
    }
}
