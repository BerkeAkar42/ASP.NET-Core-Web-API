using Entities.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore.Extensions
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
            books.Where(book => book.Price >= minPrice && book.Price <= maxPrice); //Filtrelemede bir Extension metot tanımladık.

        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return books;

            var lowerCaseTerm = searchTerm.Trim().ToLower(); //kelime ne olursa olsun boşluklarını temizle ve küçük harfe çevir. Bu şekilde işleme al.

            return books
                .Where(b => b.Title
                .ToLower() //başlığı küçük yap
                .Contains(searchTerm)); //İstenilen kelime başlık içeriyor mu diye bak.
        }


        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b => b.Id); //Eğer kullanıcı hiçbir sıralama belirtmediyse default olarak id ye göre sırala.

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

            if (orderQuery is null) //Eğer kullanıcının girdiği sıralama terimleri yoksa default olarak id ye göre sırala.
                return books.OrderBy(b => b.Id);

            return books.OrderBy(orderQuery);
        }

    }
}
