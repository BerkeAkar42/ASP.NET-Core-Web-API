using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoriesBase<T> // Soyuttur, Generic olarak aldığı değere göre şekillenecektir.
    {
        //CRUD işlemleri tanımlandı.
        IQueryable<T> FindAll(bool trackChanges); //EFCore'daki değişiklikleri takip etmeyi kapatmak, (SaveChange() dememek) performansı arttıracaktır.
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> exception, bool trackChanges);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
