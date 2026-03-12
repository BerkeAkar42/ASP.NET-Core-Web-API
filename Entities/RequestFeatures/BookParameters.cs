namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters
    {
        //Filtreleme (Filtering) işmemleri için gerekli propertyler
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; } = 1000;

        public bool ValidPriceRange => MaxPrice > MinPrice;


        //Arama (Searching) işmemleri için gerekli propertyler
        public string? SearchTerm { get; set; }


        //Sıralama (Sorting) işmemleri için gerekli propertyler
        public BookParameters()
        {
            OrderBy = "id"; //Linq sorgusuna API'den null değer dönmesin diye başlangıç olarak id verdik.
        }

    }
}
