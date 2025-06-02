using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerDTO>> GetAllAsync()
        {
            var customers = await _unitOfWork._customerRepo.GetAllAsync();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> GetByIdAsync(Guid account_id)
        {
            var customer = await _unitOfWork._customerRepo.GetByIdAsync(account_id);
            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<bool> CreateAsync(Guid account_id, CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            customer.account_id = account_id;

            await _unitOfWork._customerRepo.AddAsync(customer);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _unitOfWork._customerRepo.UpdateAsync(customer);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
