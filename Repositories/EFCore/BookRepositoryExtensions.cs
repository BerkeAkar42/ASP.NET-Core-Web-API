using Entities.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public static class BookRepositoryExtensions
    {
        /*         Extension Method Kuralları
        * Bu "büyünün" çalışması için 3 altın kural vardır:
        * Statik Sınıf: Metodun bulunduğu sınıf mutlaka static olmalıdır.
        * Statik Metot: Metodun kendisi static olmalıdır.
        * Sihirli this: İlk parametrenin başında this olmalıdır.Bu, "ben bu tipe ekleniyorum" demektir.
        */

        //this kelimesi artık bir genişletme ifadesidir.
        //Repo katmanı içerisideki "FindByCondition()" fonksiyonu "IQueryable" döndüğü için biz de bunu IQueryable tanımladık.
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>
            books.Where(book => (book.Price >= minPrice) && (book.Price <= maxPrice)); //Filtrelemede bir Extension metot tanımladık.
    }
}
