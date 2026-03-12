using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Extensions
{
    public static class OrderQueryBuilder //Bu class, yeni eklenen entitylerin de sıralanabilmesini sağlayan genişletilmiş bir classtır.
    { //Normalde repo içerisinde olan kodu aynen dışarıya aktardık. <T> Olarak tanımlanması, her türü desteklediğini söyler.
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            var orderParams = orderByQueryString.Trim().Split(','); //gelen değerin boşluklarını yok et ve aralarına virgül koy. Bunu dizi olarak dön. ["price desc", " title"]


            var propertyInfos = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance); //Kitabın propertylerine eriş. Etiştiğimn proplardan public olan ve newlendikten sonra erişilebilenleri al. Bu işlem olmayan bir parametreyi sorguya dahil etmeyi engellemektir.

            var orderQueryBuilder = new StringBuilder();


            //title asceding, price descending, id ascending, -> sondaki virgülden kurtulmak için döngüden sonraki adım yapıldı.
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(' ')[0]; //Eğer kullanıcı "price desc" yazdıysa, boşluktan bölüp sadece ilk kelimeyi (price) alıyoruz.

                var objectProperty = propertyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName,
                    StringComparison.InvariantCultureIgnoreCase)); //Büyük küçük ayrımı yapmasın diyoruz.

                if (objectProperty is null) //Eğer kullanıcı saçma sapan, bizde olmayan bir alan ismi yazdıysa onu görmezden gel ve bir sonraki parametreye geç.
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending"; //Eğer parametrenin sonunda " desc" varsa yönü descending (büyükten küçüğe) yap. Yoksa varsayılan olarak ascending (küçükten büyüğe) kabul et.

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},"); //Elimizdeki tertemiz verilerle bir cümle kuruyoruz: "Price descending," veya "Title ascending,". Bunu listeye ekliyoruz.
            }

            //Sorgu sonucunda en son eklenen virgülü kaldır.
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery; //Generic olarak <T> gelen veri içerisindeki proplardan ne gelirse onu dönecektir.
        }


    }
}
