using AutoMapper;
using BLL.Interfaces;
using BOL.DTOs;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class Service_AppointmentService : IService_AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Service_AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Service_AppointmentDTO>> GetAllAsync(
            Guid? serviceId = null,
            Guid? appointmentId = null,
            DateTime? startDate = null)
        {
            var data = await _unitOfWork._service_AppointmentRepo.GetAllAsync(); // Trả về List

            if (serviceId.HasValue)
            {
                data = data.Where(sa => sa.service_id == serviceId.Value).ToList();
            }

            if (appointmentId.HasValue)
            {
                data = data.Where(sa => sa.appointment_id == appointmentId.Value).ToList();
            }

            if (startDate.HasValue)
            {
                data = data.Where(sa => sa.start_time.Date == startDate.Value.Date).ToList();
            }

            return _mapper.Map<List<Service_AppointmentDTO>>(data);
        }


        public async Task<Service_AppointmentDTO> GetByIdAsync(Guid serviceId)
        {
            var data = await _unitOfWork._service_AppointmentRepo.GetByIdAsync(serviceId);
            return _mapper.Map<Service_AppointmentDTO>(data);
        }

        public async Task<bool> CreateAsync(Service_AppointmentDTO dto)
        {
            var entity = _mapper.Map<Service_Appointment>(dto);
            await _unitOfWork._service_AppointmentRepo.AddAsync(entity);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Service_AppointmentDTO dto)
        {
            var entity = _mapper.Map<Service_Appointment>(dto);
            await _unitOfWork._service_AppointmentRepo.UpdateAsync(entity);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid serviceId)
        {
            var entity = await _unitOfWork._service_AppointmentRepo.GetByIdAsync(serviceId);
            if (entity == null) return false;
            await _unitOfWork._service_AppointmentRepo.RemoveAsync(entity);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
