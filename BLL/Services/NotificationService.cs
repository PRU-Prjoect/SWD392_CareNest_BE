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
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<NotificationDTO>> GetAllAsync(Guid? receiverId = null, string? description = null, bool? isRead = null)
        {
            var notifications = await _unitOfWork._notificationRepo.GetAllAsync();

            if (receiverId.HasValue)
            {
                notifications = notifications.Where(n => n.receiver_id == receiverId.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                notifications = notifications.Where(n => n.description != null && n.description.Contains(description, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (isRead.HasValue)
            {
                notifications = notifications.Where(n => n.is_read == isRead.Value).ToList();
            }

            return _mapper.Map<List<NotificationDTO>>(notifications);
        }

        public async Task<NotificationDTO> GetByIdAsync(Guid id)
        {
            var notification = await _unitOfWork._notificationRepo.GetByIdAsync(id);
            return _mapper.Map<NotificationDTO>(notification);
        }

        public async Task<NotificationDTO> CreateAsync(NotificationDTO notificationDTO)
        {
            var entity = _mapper.Map<Notification>(notificationDTO);
            entity.id = Guid.NewGuid();
            entity.created_at = DateTime.UtcNow;

            await _unitOfWork._notificationRepo.AddAsync(entity);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<NotificationDTO>(entity);
        }

        public async Task<NotificationDTO> UpdateAsync(NotificationDTO notificationDTO)
        {
            var entity = await _unitOfWork._notificationRepo.GetByIdAsync(notificationDTO.id);
            if (entity == null) return null;

            entity.description = notificationDTO.description;
            entity.is_read = notificationDTO.is_read;
            entity.updated_at = DateTime.UtcNow;

            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<NotificationDTO>(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _unitOfWork._notificationRepo.GetByIdAsync(id);
            if (entity == null) return false;

            await _unitOfWork._notificationRepo.RemoveAsync(entity);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
