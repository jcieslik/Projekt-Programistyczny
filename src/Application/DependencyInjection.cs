using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFilterService, FilterService>();
            services.AddScoped<IProductImageService, ProductImageService>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IOfferService, OfferService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProvinceService, ProvinceService>();
            return services;
        }
    }
}
