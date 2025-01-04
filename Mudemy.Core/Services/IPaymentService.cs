using Mudemy.Core.DTOs;
using SharedLibrary.Dtos;
using System.Threading.Tasks;

namespace Mudemy.Core.Services
{
    public interface IPaymentService
    {
        Task<Response<string>> ProcessPaymentAsync(PaymentDto paymentDto, int orderId);
    }
}
