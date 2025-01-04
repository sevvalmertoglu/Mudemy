using Mudemy.Core.DTOs;
using Mudemy.Core.Models;
using Mudemy.Core.Repositories;
using Mudemy.Core.Services;
using Mudemy.Core.UnitOfWork;
using SharedLibrary.Dtos;
using System;
using System.Threading.Tasks;

namespace Mudemy.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Payment> _paymentRepository;
        private readonly IGenericRepository<Order> _orderRepository;

        public PaymentService(IUnitOfWork unitOfWork, 
                              IGenericRepository<Payment> paymentRepository, 
                              IGenericRepository<Order> orderRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Response<string>> ProcessPaymentAsync(PaymentDto paymentDto, int orderId)
        {
            if (paymentDto == null)
            {
                return Response<string>.Fail("Invalid payment details", 400, true);
            }

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                return Response<string>.Fail("Order not found", 404, true);
            }

            if (string.IsNullOrWhiteSpace(paymentDto.CardNumber) || paymentDto.CardNumber.Length != 16)
            {
                return Response<string>.Fail("Invalid card number", 400, true);
            }

            if (DateTime.TryParseExact(paymentDto.ExpirationDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime expirationDate))
            {
                if (expirationDate <= DateTime.Now)
                {
                    return Response<string>.Fail("Expiration date must be in the future", 400, true);
                }
            }
            else
            {
                return Response<string>.Fail("Invalid expiration date format", 400, true);
            }

            if (string.IsNullOrWhiteSpace(paymentDto.CVV) || paymentDto.CVV.Length != 3)
            {
                return Response<string>.Fail("Invalid CVV", 400, true);
            }

            var transactionId = Guid.NewGuid().ToString();

            var payment = new Payment
            {
                OrderId = orderId,
                CardHolderName = paymentDto.CardHolderName,
                CardNumber = MaskCardNumber(paymentDto.CardNumber),
                ExpirationDate = paymentDto.ExpirationDate,
                CVV = "***",
                TransactionId = transactionId,
                PaymentDate = DateTime.Now,
                Amount = order.TotalPrice
            };

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.CommmitAsync();

            return Response<string>.Success(transactionId, 201);
        }

        private string MaskCardNumber(string cardNumber)
        {
            return cardNumber.Substring(0, 4) + new string('*', 8) + cardNumber.Substring(12, 4);
        }
    }
}
