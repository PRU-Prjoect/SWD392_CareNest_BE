﻿using BOL.DTOs;

namespace BLL.Interfaces
{
    public interface IService_TypeService
    {
        Task<List<Service_TypeResponse>> GetAllAsync(string? name, bool? is_public);           // Get all service types
        Task<Service_TypeResponse> GetByIdAsync(Guid id);         // Get a service type by ID
        Task<Service_TypeResponse> CreateAsync(Service_TypeRequest serviceTypeDTO);
        Task<Service_TypeResponse> UpdateAsync(string id, Service_TypeRequest serviceTypeDTO);
        Task<bool> DeleteAsync(Guid id);
    }
}
