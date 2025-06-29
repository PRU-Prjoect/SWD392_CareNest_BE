﻿using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountRepository _accountRepository;
        private readonly IPet_TypeRepository _pet_TypeRepository;
        private readonly IService_TypeRepository _service_TypeRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISub_AddressRepository _sub_AddressRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IAppointmentsRepository _appointmentsRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IService_AppointmentRepository _service_AppointmentRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IPet_Service_RoomRepository _pet_Service_RoomRepository;
        private readonly IRoom_BookingRepository _room_BookingRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IImageGalleryRepository _imageGalleryRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IService_CartRepository _serviceCartRepository;

        public UnitOfWork(ApplicationDbContext Context,
            IAccountRepository AccountRepository,
            IPet_TypeRepository Pet_TypeRepository,
            IService_TypeRepository service_TypeRepository,
            IShopRepository shopRepository,
            IStaffRepository staffRepository,
            ICustomerRepository customerRepository,
            ISub_AddressRepository sub_AddressRepository,
            IServiceRepository serviceRepository,
            IAppointmentsRepository appointmentsRepository,
            IRatingRepository ratingRepository,
            IService_AppointmentRepository service_AppointmentRepository,
            IRoomRepository roomRepository,
            IHotelRepository hotelRepository,
            IPet_Service_RoomRepository pet_Service_RoomRepository,
            IRoom_BookingRepository room_BookingRepository,
            INotificationRepository notificationRepository,
            IImageGalleryRepository imageGalleryRepository,
            ICartRepository cartRepository,
            IService_CartRepository serviceCartRepository)
        {
            _context = Context;
            _accountRepository = AccountRepository;
            _pet_TypeRepository = Pet_TypeRepository;
            _service_TypeRepository = service_TypeRepository;
            _shopRepository = shopRepository;
            _staffRepository = staffRepository;
            _customerRepository = customerRepository;
            _sub_AddressRepository = sub_AddressRepository;
            _serviceRepository = serviceRepository;
            _appointmentsRepository = appointmentsRepository;
            _ratingRepository = ratingRepository;
            _service_AppointmentRepository = service_AppointmentRepository;
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _pet_Service_RoomRepository = pet_Service_RoomRepository;
            _room_BookingRepository = room_BookingRepository;
            _notificationRepository = notificationRepository;
            _imageGalleryRepository = imageGalleryRepository;
            _cartRepository = cartRepository;
            _serviceCartRepository = serviceCartRepository;
        }

        public IAccountRepository _accountRepo => _accountRepository;
        public IPet_TypeRepository _pet_TypeRepo => _pet_TypeRepository;
        public IService_TypeRepository _service_TypeRepo => _service_TypeRepository;
        public IShopRepository _shopRepo => _shopRepository;
        public IStaffRepository _staffRepo => _staffRepository;
        public ICustomerRepository _customerRepo => _customerRepository;
        public ISub_AddressRepository _sub_AddressRepo => _sub_AddressRepository;
        public IServiceRepository _serviceRepo => _serviceRepository;
        public IAppointmentsRepository _appointmentsRepo => _appointmentsRepository;
        public IRatingRepository _ratingRepo => _ratingRepository;
        public IService_AppointmentRepository _service_AppointmentRepo => _service_AppointmentRepository;
        public IRoomRepository _roomRepo => _roomRepository;
        public IHotelRepository _hotelRepo => _hotelRepository;
        public IPet_Service_RoomRepository _pet_Service_RoomRepo => _pet_Service_RoomRepository;
        public IRoom_BookingRepository _room_BookingRepo => _room_BookingRepository;
        public INotificationRepository _notificationRepo => _notificationRepository;
        public IImageGalleryRepository _imageGalleryRepo => _imageGalleryRepository;
        public ICartRepository _cartRepo => _cartRepository;
        public IService_CartRepository _serviceCartRepo => _serviceCartRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
