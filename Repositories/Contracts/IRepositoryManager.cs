using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    //Contracts uygulama detaylarıyla ilgilenmezler. Hangi metotları implamente etmeniz gerektiğini belirtir.
    //implamente etmek: Metotları olduğu gibi getirip, içerisinde değişiklik yapmamıza olanak sağlarlar
    public interface IRepositoryManager
    {
        IBookRepository Book { get; }
        Task SaveAsync(); 
    }
}
