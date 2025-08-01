﻿namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IAccountRepository _accountRepo { get; }
        public IPet_TypeRepository _pet_TypeRepo { get; }
        public IService_TypeRepository _service_TypeRepo { get; }
        public IShopRepository _shopRepo { get; }
        public IStaffRepository _staffRepo { get; }
        public ICustomerRepository _customerRepo { get; }
        public ISub_AddressRepository _sub_AddressRepo { get; }
        public IServiceRepository _serviceRepo { get; }
        public IAppointmentsRepository _appointmentsRepo { get; }
        public IRatingRepository _ratingRepo { get; }
        public IRoomRepository _roomRepo { get; }
        public IService_AppointmentRepository _service_AppointmentRepo { get; }
        public IHotelRepository _hotelRepo { get; }
        public IPet_Service_RoomRepository _pet_Service_RoomRepo { get; }
        public IRoom_BookingRepository _room_BookingRepo { get; }
        public INotificationRepository _notificationRepo { get; }
        public IImageGalleryRepository _imageGalleryRepo { get; }
        public ICartRepository _cartRepo { get; }
        public IService_CartRepository _serviceCartRepo { get; }
        public Task<int> SaveChangeAsync();
    }
}
