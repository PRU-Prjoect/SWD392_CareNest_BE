using BLL.Interfaces;
using BLL.Mapper;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;

namespace CareNestAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection Services (this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
            services.AddScoped(typeof(IPet_TypeRepository), typeof(Pet_TypeRepository));
            services.AddScoped(typeof(IService_TypeRepository), typeof(Service_TypeRepository));
            services.AddScoped(typeof(IShopRepository), typeof(ShopRepository));
            services.AddScoped(typeof(IStaffRepository), typeof(StaffRepository));
            services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));

            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));
            services.AddScoped(typeof(IPet_TypeService), typeof(Pet_TypeService));
            services.AddScoped(typeof(IService_TypeService), typeof(Service_TypeService));
            services.AddScoped(typeof(IShopService), typeof(ShopService));
            services.AddScoped(typeof(IStaffService), typeof(StaffService));
            services.AddScoped(typeof(ICustomerService), typeof(CustomerService));

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            return services;
        }
    }
}
