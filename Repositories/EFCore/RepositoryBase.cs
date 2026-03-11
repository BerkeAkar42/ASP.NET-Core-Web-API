using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    //IRepositoriesBase Interface'ini aldık ve burada metotlara dönüştürdük. Daha sonra bu classı kalıttığımızda diğerleri de bunlardan nasiplenecektir.
    public abstract class RepositoryBase<T> : IRepositoriesBase<T> where T : class
    { // Implemenet etmek: Base ifadeyi implemente etmek', bir arayüzdeki (Interface) imzalara gövde kazandırmak demektir.
      // abstract -> Bu class new edilemez!!!

        protected readonly RepositoryContext _context; //Context'i tanımladık.

        //Bu class'ı mireas alan diğer class'lar, _context ifadesine ulaşabilsinler diye privete değil, protected tanımlandı. Anladığım kadarıyla protected da private gibi ancak kalıtım alanlara public bir şey

        public RepositoryBase(RepositoryContext context) //Context'i new leyen program.cs'den verileri aldık.
        {
            _context = context;
        }


        //CRUD işlemleri
        //.Set<T> --> EF Core'a şunu der: "Şu an hangi tiple çalışıyorsak (T), git veritabanındaki o tabloyu bul.
        public void Create(T entity) => _context.Set<T>().Add(entity);


        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        //Tamamını getir
        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().AsNoTracking() : //AsNoTracking() diyerek aslında EF Core'un veriyi izleyerek ram de yer kaplamasını engelledik.
            _context.Set<T>(); //ram de veri beklesin diyor gibiyiz.


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> exception, bool trackChanges) =>
            !trackChanges ?
            _context.Set<T>().Where(exception).AsNoTracking() :
            _context.Set<T>().Where(exception);
        

        public void Update(T entity) => _context.Set<T>().Update(entity);
        
    }
}
