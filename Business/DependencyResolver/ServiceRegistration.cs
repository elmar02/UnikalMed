using Business.Abstract;
using Business.Concrete;
using Business.Utilities.Uploader;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.PostgresSQL;
using DataAccess.Concrete.SQLServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<ICategoryDAL,EFCategoryDAL>();
            services.AddScoped<ICategoryService, CategoryManager>();

            services.AddScoped<ISubCategoryDAL, EFSubCategoryDAL>();
            services.AddScoped<ISubCategoryService, SubCategoryManager>();

            services.AddScoped<IProductDAL, EFProductDAL>();
            services.AddScoped<IProductService, ProductManager>();

            services.AddScoped<IBrandDAL, EFBrandDAL>();
            services.AddScoped<IBrandService, BrandManager>();
            

            services.AddScoped<IPartnerDAL, EFPartnerDAL>();
            services.AddScoped<IPartnerService, PartnerManager>();

            services.AddScoped<IAdvertDAL, EFAdvertDAL>();
            services.AddScoped<IAdvertService, AdvertManager>();

            services.AddScoped<IServiceDAL, EFServiceDAL>();
            services.AddScoped<IServiceService, ServiceManager>();

            services.AddScoped<IStaffDAL, EFStaffDAL>();
            services.AddScoped<IStaffService, StaffManager>();

            services.AddScoped<IReferenceDAL, EFReferenceDAL>();
            services.AddScoped<IReferenceService, ReferenceManager>();

            services.AddScoped<IBlogDAL, EFBlogDAL>();
            services.AddScoped<IBlogService, BlogManager>();

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<IRoleService, RoleManager>();
            services.AddScoped<IFileService, FileManager>();
        }
    }
}
