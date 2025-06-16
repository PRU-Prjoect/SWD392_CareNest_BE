using BOL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetAllAsync(Guid? receiverId = null, string? description = null, bool? isRead = null);
        Task<NotificationDTO> GetByIdAsync(Guid id);
        Task<NotificationDTO> CreateAsync(NotificationDTO notificationDTO);
        Task<NotificationDTO> UpdateAsync(NotificationDTO notificationDTO);
        Task<bool> DeleteAsync(Guid id);
    }
}
