using BLL.Interfaces;
using BLL.Mapper;
using BLL.Services;
using BOL.Config;
using DAL.Interfaces;
using DAL.Repositories;
using System.Configuration;

namespace CareNestAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection Services (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
            services.AddScoped(typeof(IPet_TypeRepository), typeof(Pet_TypeRepository));
            services.AddScoped(typeof(IService_TypeRepository), typeof(Service_TypeRepository));
            services.AddScoped(typeof(IShopRepository), typeof(ShopRepository));
            services.AddScoped(typeof(IStaffRepository), typeof(StaffRepository));
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
            services.AddScoped(typeof(ISub_AddressRepository), typeof(Sub_AddressRepository));
            services.AddScoped(typeof(IServiceRepository), typeof(ServiceRepository));
            services.AddScoped(typeof(IAppointmentsRepository), typeof(AppointmentsRepository));
            services.AddScoped(typeof(IRatingRepository), typeof(RatingRepository));
            services.AddScoped(typeof(IService_AppointmentRepository), typeof(Service_AppointmentRepository));
            services.AddScoped(typeof(IRoomRepository), typeof(RoomRepository));
            services.AddScoped(typeof(IHotelRepository), typeof(HotelRepository));

            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));
            services.AddScoped(typeof(IPet_TypeService), typeof(Pet_TypeService));
            services.AddScoped(typeof(IService_TypeService), typeof(Service_TypeService));
            services.AddScoped(typeof(IShopService), typeof(ShopService));
            services.AddScoped(typeof(IStaffService), typeof(StaffService));
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
            services.AddScoped(typeof(ISub_AddressService), typeof(Sub_AddressService));
            services.AddScoped(typeof(IServiceService), typeof(ServiceService));
            services.AddScoped(typeof(IAppointmentsService), typeof(AppointmentsService));
            services.AddScoped(typeof(IRatingService), typeof(RatingService));
            services.AddScoped(typeof(IService_AppointmentService), typeof(Service_AppointmentService));
            services.AddScoped(typeof(IRoomService), typeof(RoomService));
            services.AddScoped(typeof(ICloudinaryService), typeof(CloudinaryService));
            services.AddScoped(typeof(ICloudinaryService), typeof(CloudinaryService));

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.Configure<CloudinaryConfig>(configuration.GetSection("Cloudinary"));
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
