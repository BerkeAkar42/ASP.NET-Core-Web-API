using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class ValidationFilterAttribute : ActionFilterAttribute //API'larda kontrol işlemleri yapacak
    {
        public override void OnActionExecuting(ActionExecutingContext context) //Metot (API) çalışmadan önce çalışacak olaran metot.
        {
            var controller = context.RouteData.Values["controller"]; //Aktif şu anda bulunan controllerın key değerini alıyor.
            var action = context.RouteData.Values["action"]; //Hangi metotun çalıştığını öğreniyoruz.

            // DTO
            var param = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value; //Hangi DTO verisi bu.

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null." +
                    $"Controller: {controller}" +
                    $"Action: {action}");
                return; //400
            }

            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState); //422


        }
    }
}
