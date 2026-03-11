using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    //abstract, classın newlenmesini önler
    public abstract partial class NotFoundExceptionException : Exception
    {
        protected NotFoundExceptionException(string message) : base(message)
        {

        }
    }
}
