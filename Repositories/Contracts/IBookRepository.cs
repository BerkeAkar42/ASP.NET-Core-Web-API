using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    //Contract tanımı yapıyoruz.
    public interface IBookRepository : IRepositoriesBase<Book> //IRepositoriesBase e Book'u vererek aslında CRUD özelliklerini Book classına göre uygun hale getirdik.
    {
        //IQueryable<Book> GetAllBooks(bool trackChanges); //Normel metot
        Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges);
        //Eğer sadece veri listeleyeceksen (trackChanges = false), EF Core'un o veriyi hafızada takip etmesini engelliyoruz (AsNoTracking).
        //Neden? Çünkü takip etme işlemi (tracking) RAM ve CPU harcar. Sadece okuma yaparken bu yükten kurtulmak uygulamayı inanılmaz hızlandırır.
        //_context.Set<T>().AsNoTracking()


        Task<Book> GetOneBookByIdAsync(int id, bool trackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book, bool trackChanges);
        void DeleteOneBook(Book entity);
    }
}
