using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext context) : base(context)
        {

        }

        public void CreateOneBook(Book book) => Create(book);

        public void DeleteOneBook(Book book) => Delete(book);


        //normalde Task<IEnumerable<Book>> vardı. Bu verileri okumamıza yarıyordu. Biz Pagination yaptığımızdan ötürü kendi List<T> classımızı yazdık.
        public async Task<PagedList<Book>> GetAllBooksAsync(BookParameters bookParameters, bool trackChanges)
        {
            var books = await FindAll(trackChanges)
                .FilterBooks( //minPrice: maxPrice: yazarak onun yerini garantiledik.
                minPrice: bookParameters.MinPrice, 
                maxPrice: bookParameters.MaxPrice) //Bu metot bizim genişletme metotumuzdur. (kendimiz IQueryable'a yazdık) - Bu meteot da EFCore > BookRepositoryExtensions.cs' de yazmakta.
                .Search(bookParameters.SearchTerm) //Bu arama metotumuz.
                .OrderBy(b => b.Id)
                .ToListAsync();
            //.Skip((bookParameters.PageNumber - 1) * bookParameters.PageSize) //Sayfalama mantığı burada. 
            //.Take(bookParameters.PageSize) //Belirlediğimiz PageSize kadar veriyi al diyoruz.

            return PagedList<Book>
                .ToPagedList(books, bookParameters.PageNumber, bookParameters.PageSize);

            //(bookParameters.PageNumber - 1) --> Index numarasından dolayı -1 dedik.
            //* bookParameters.PageSize --> Mesela her sayfa 5 kayıt alıyor olsun. Biz de 3. sayfadaki verileri alalım istiyoruz. O zaman 2*5 = 10. Yani 10 kayıt atlayıp diğer verileri almasını söylüyoruz.
        }



        public async Task<Book> GetOneBookByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(b => b.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateOneBook(Book book) => Update(book);

        public void UpdateOneBook(Book book, bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}
