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

            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IEmailService), typeof(EmailService));
            services.AddScoped(typeof(IPet_TypeService), typeof(Pet_TypeService));
            services.AddScoped(typeof(IService_TypeService), typeof(Service_TypeService));

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            return services;
        }
    }
}
