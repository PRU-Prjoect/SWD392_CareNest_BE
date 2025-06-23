using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using BOL.Enums;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Web;

namespace BLL.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartResponse> AddServiceToCart(CartRequest cartRequest)
        {
            var customer = await _unitOfWork._customerRepo.GetByIdAsync(cartRequest.customer_id);
            if (customer == null)
            {
                throw new Exception("customer not found");
            }
            var service = await _unitOfWork._serviceRepo.GetServiceByIdAsync(cartRequest.service_id);
            if (service == null)
            {
                throw new Exception("service not foound");
            }
            var cart = await _unitOfWork._cartRepo.GetByCustomerIdAsync(cartRequest.customer_id);
            if (cart == null)
            {
                cart = new Cart
                {
                    customer_id = cartRequest.customer_id,
                    total = 0,
                    service_Carts = [],
                };
                await _unitOfWork._cartRepo.AddAsync(cart);
                await _unitOfWork.SaveChangeAsync();
            }
            if (cart.service_Carts.FirstOrDefault(a => a.service_id == cartRequest.service_id) != null)
            {
                throw new Exception("Service already exist in cart!");
            }

            cart.total = cart.service_Carts.Sum(sc => sc.service?.Price ?? 0) + service.Price;

            var newService = new Service_Cart
            {
                cart_id = cart.id,
                service_id = cartRequest.service_id,
            };
            await _unitOfWork._serviceCartRepo.AddAsync(newService);
            await _unitOfWork.SaveChangeAsync();

            var cartResponse = new CartResponse
            {
                cart_id = cart.id,
                customer_id = cart.customer_id,
                total = cart.total,
                services = cart.service_Carts.Select(a => _mapper.Map<ServiceDTO>(a.service)).ToList(),
            };


            return cartResponse;
        }

        public async Task<CartResponse> GetAllCartServicesByCustomerId(Guid id)
        {
            var customer = await _unitOfWork._customerRepo.GetByIdAsync(id);
            if (customer == null)
            {
                throw new Exception("customer not found");
            }
            var cart = await _unitOfWork._cartRepo.GetByCustomerIdAsync(id);

            if (cart == null)
            {
                cart = new Cart
                {
                    customer_id = id,
                    total = 0,
                    service_Carts = [],
                };
                await _unitOfWork._cartRepo.AddAsync(cart);
                await _unitOfWork.SaveChangeAsync();
            }
            cart.total = cart.service_Carts.Sum(sc => sc.service?.Price ?? 0);
            var cartResponse = new CartResponse
            {
                cart_id = cart.id,
                customer_id = cart.customer_id,
                total = cart.total,
                services = cart.service_Carts.Select(a => _mapper.Map<ServiceDTO>(a.service)).ToList(),
            };
            return cartResponse;
        }

        public async Task<CartResponse> RemoveServiceFromCart(CartRequest cartRequest)
        {
            var customer = await _unitOfWork._customerRepo.GetByIdAsync(cartRequest.customer_id);
            if (customer == null)
            {
                throw new Exception("customer not found");
            }
            var service = await _unitOfWork._serviceRepo.GetServiceByIdAsync(cartRequest.service_id);
            if (service == null)
            {
                throw new Exception("service not foound");
            }
            var cart = await _unitOfWork._cartRepo.GetByCustomerIdAsync(cartRequest.customer_id);
            if (cart == null)
            {
                throw new Exception("Please add service to cart");
            }
            var service_cart = cart.service_Carts.FirstOrDefault(a => a.service_id == cartRequest.service_id);
            if (service_cart == null)
            {
                throw new Exception("Service already removed!");
            }
            cart.total = cart.service_Carts.Sum(sc => sc.service?.Price ?? 0) - service.Price;
            await _unitOfWork._serviceCartRepo.RemoveAsync(service_cart);
            await _unitOfWork.SaveChangeAsync();

            var cartResponse = new CartResponse
            {
                cart_id = cart.id,
                customer_id = cart.customer_id,
                total = cart.total,
                services = cart.service_Carts.Select(a => _mapper.Map<ServiceDTO>(a.service)).ToList(),
            };
            return cartResponse;
        }
    }
}
