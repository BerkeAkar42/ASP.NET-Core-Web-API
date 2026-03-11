
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Repositories.EFCore;
using Services.Contracts;
using WebAPI.Extensions;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Modern ve tavsiye edilen kullaným:
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            //LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


            // Add services to the container.

            builder.Services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true; //Ýçerik pazarlýðýna API'lerimizi açtýk
                config.ReturnHttpNotAcceptable = true; //Belirlenen türler dýþýnda teklif gelirse reddedilecek. Reddedilirse 406 kodu dönecek.
            })
                .AddCustomCsvFormatter()
                .AddXmlDataContractSerializerFormatters() //xml formatýný destekleyen tek satýrlýk kod.
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly) //
                .AddNewtonsoftJson();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();



            //Validation servis ayarý
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; //Bu ayarla [ApiController]'ýn otomatik olarak 400 hatasý dmnmesini engeller, bizim 422 hatasý dönmemizi (JSON'dan gelen veri baþarýyla geldi ancak koþullarý saðlamýyor der) saðlar.
            });


            //Servis kaydý.
            builder.Services.ConfigureSqlContext(builder.Configuration);
            builder.Services.ConfigureRepositoryManager();
            builder.Services.ConfigureServiceManager();
            builder.Services.ConfigureLoggerService();
            builder.Services.AddAutoMapper(typeof(Program)); //AutoMapper servis kaydý.
            builder.Services.ConfigureActionFilters();
            builder.Services.ConfigureCors(); //Polisy serivisi tanýmalasý

            var app = builder.Build();

            //Program.cs de constructor olmadýðý için buradan nesnenin atamasýný yapýyoruz
            var logger = app.Services.GetRequiredService<ILoggerService>();
            app.ConfigureExceptionHandler(logger); //ExceptionMiddlewareExtensions daki metota buradan parametre gönderiyoruz.


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


            if (app.Environment.IsProduction())
            {
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseCors("CorsPolisy"); //Polisy serivisi tanýmalasý

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
