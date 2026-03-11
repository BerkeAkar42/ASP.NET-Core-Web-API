using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using System.Net;

namespace WebAPI.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger) //Extension method (genişletme metodu) yazmak için sınıfın ve metodun statik olması gerekir.
        {

            //Bu ifade sayesinde bu metot, Program.cs içinde sanki app nesnesinin kendi metoduymuş gibi (app.ConfigureExceptionHandler(...)) çağrılabilecek.


            app.UseExceptionHandler(appError => 
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch //Burada Switch Case yapısı kullanıldı
                        {
                            NotFoundExceptionException => StatusCodes.Status404NotFound, //Burası bir Case
                            _ => StatusCodes.Status500InternalServerError, //Burası bir Case
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }

                });
            });

            //UseExceptionHandler: ASP.NET Core'un içinde hazır gelen, "bir hata olduğunda buraya gir" diyen komuttur.
            //context.Response.StatusCode: Hata olduğunda kullanıcıya varsayılan olarak 500 (Internal Server Error) döneceğimizi belirtiyoruz.
            //context.Response.ContentType: Hata mesajını düz yazı veya HTML olarak değil, modern standart olan JSON formatında göndereceğimizi söylüyoruz.

            //IExceptionHandlerFeature: Bu arayüz, o an meydana gelen hatanın tüm detaylarını (hata mesajı, hangi satırda olduğu vb.) içinde barındırır.
            //contextFeature.Error: İşte asıl "patlayan" hata nesnesi buradadır.
            //logger.LogError: Senin LoggerManager sınıfını kullanarak, bu hatayı o anki tarihle beraber logs klasöründeki dosyaya mühürlüyoruz.
        }


    }
}
