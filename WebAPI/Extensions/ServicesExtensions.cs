using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Repositories.Contracts;
using Repositories.EFCore;
using Services;
using Services.Contracts;

namespace WebAPI.Extensions
{
    public static class ServicesExtensions //Burası program.cs'i servis kayıtlarıyla kirletmemek için, uzun kodları buraya yazdığımız bir classtır.
    {
        //Hangi sınıfı genişletiyorsak "this" ile o parametreyi veriyoruz.
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }


        //this olduğu için o parametreyi vermek zorunda değiliz.
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }


        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerManager>();
        }

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>(); //IoC
            services.AddSingleton<LogFilterAttribute>();
        }


        //API'mıza kimlerin istek atabilecekleri hakkında polisies (kurallar / politikalar) yazıyoruz
        public static void ConfigureCors(this IServiceCollection services) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolisy", builder => 
                    builder
                    //.WithOrigins("https://app.berkeakar.tr") --> bu satır hangi adresten gelen istekleri kabul edeceğini söyler.
                    .AllowAnyOrigin() //Bütün kökenlere izin ver
                    .AllowAnyMethod() //Bütün metotlara (GET, POST, SET vs...)
                    .AllowAnyHeader() //Bütün headerlardan gelen verilere izin ver.
                    .WithExposedHeaders("X-Pagination")
                );
            });
        }

    }
}
