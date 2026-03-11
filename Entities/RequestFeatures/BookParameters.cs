namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters
    {
        //Filtreleme işmemleri için gerekli propertyler
        public uint MinPrice { get; set; }
        public uint MaxPrice { get; set; } = 1000;

        public bool ValidPriceRange => MaxPrice > MinPrice;
    }
}
