using Mudemy.Core.DTOs;
using SharedLibrary.Dtos;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mudemy.Core.Services
{
    public interface IOrderService
    {
        Task<Response<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId);
        Task<Response<OrderDto>> GetOrderDetailsByIdAsync(int id);
        Task<Response<CreateOrderDto>> PlaceOrderAsync(CreateOrderDto createOrderDto);
    }
}
