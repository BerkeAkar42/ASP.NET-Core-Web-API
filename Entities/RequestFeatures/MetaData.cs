using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class MetaData //Pagination ile frontend tarafına bilgi verme işlemi / [FromQuery]
    {
        public int CurrentPage { get; set; } //Mevcut sayfanın index numarası
        public int TotalPage { get; set; } //Toplam Sayfa
        public int PageSize { get; set; } //Her bir sayfada gösterilecek kayıt sayısı
        public int TotalCount { get; set; } //Toplam kayıt sayısı

        public bool HasPrevious => CurrentPage > 1; //True ise geride sayfa var. False ise ilk sayfadır falan.
        public bool HasPage => CurrentPage < TotalPage; //True ise İleride sayfa var. False ise Son sayfadır falan.
    }
}
