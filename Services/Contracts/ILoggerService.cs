using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ILoggerService
    {
        //Contracts (Sözleşmeler), projedeki "Arayüzler" (Interfaces) demektir.


        //Log kayıtları tutmak için Interface olarak önceden tanımlanması gereken fonksiyonları belirtiyoruz.
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogDebug(string message);
    }
}
