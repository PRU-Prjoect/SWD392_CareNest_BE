using BOL.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace BLL.Interfaces
{
    public interface ICartService
    {
        public Task<CartResponse> AddServiceToCart(CartRequest cartRequest);
        public Task<CartResponse> GetAllCartServicesByCustomerId(Guid id);
        public Task<CartResponse> RemoveServiceFromCart(CartRequest cartRequest);
    }
}
