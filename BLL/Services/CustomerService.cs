using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using BOL.Enums;
using DAL.Interfaces;
using DAL.Models;

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

        public async Task<List<CustomerResponse>> GetAllAsync(
                string? name = null,
                string? gender = null,
                string? email = null)
        {
            var customerQuery = await _unitOfWork._customerRepo.GetAllCustomer();

            var filtered = customerQuery.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                var lowerName = name.ToLower();
                filtered = filtered.Where(c => c.full_name != null && c.full_name.ToLower().Contains(lowerName));
            }

            if (!string.IsNullOrWhiteSpace(gender))
            {
                filtered = filtered.Where(c => c.gender != null && c.gender.ToLower() == gender.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                var lowerEmail = email.ToLower();
                filtered = filtered.Where(c => c.account != null && c.account.email.ToLower().Contains(lowerEmail));
            }

            // Trả kết quả đã map sang DTO
            return _mapper.Map<List<CustomerResponse>>(filtered.ToList());
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
            // Lấy thông tin tài khoản của customer từ cơ sở dữ liệu
            var customerAccount = await _unitOfWork._accountRepo.GetByIdAsync(customerDto.account_id);

            if (customerAccount != null)
            {
                // Cập nhật vai trò của tài khoản thành "shop"
                customerAccount.role = Role.Customer;

                // Cập nhật tài khoản
                await _unitOfWork._accountRepo.UpdateAsync(customerAccount);

                // Thêm cửa hàng vào cơ sở dữ liệu
                await _unitOfWork._customerRepo.AddAsync(customer);

                // Lưu tất cả thay đổi
                return await _unitOfWork.SaveChangeAsync() > 0;
            }
            return false; // Nếu không tìm thấy tài khoản
        }

        public async Task<bool> UpdateAsync(CustomerDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            await _unitOfWork._customerRepo.UpdateAsync(customer);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
