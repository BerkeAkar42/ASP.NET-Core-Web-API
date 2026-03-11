using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book> //Bu sadece Book varlığını tespit eder ve onu configuration uygular.
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Karagöz ve Hacivat", Price = 75 },
                new Book { Id = 2, Title = "Mesnevi", Price = 175 },
                new Book { Id = 3, Title = "Devlet", Price = 375 }
                );
        }
    }
}


//-- GERÇEK DÜNYA PROJELERİNDE BULUNAN CONFİG ÖRNEĞİ --

//using Entities.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Repositories.EFCore.Config
//{
//    public class BookConfig : IEntityTypeConfiguration<Book>
//    {
//        public void Configure(EntityTypeBuilder<Book> builder)
//        {
//            // 1. Tablo ve Key Yapılandırması
//            builder.ToTable("Books"); // Tablo adını açıkça belirtiyoruz
//            builder.HasKey(b => b.Id); // Primary Key (Convention olsa da açıkça yazmak iyidir)

//            // 2. Özellik (Property) Kısıtlamaları
//            builder.Property(b => b.Title)
//                .IsRequired()             // Boş geçilemez (NOT NULL)
//                .HasMaxLength(150)        // nvarchar(max) yerine nvarchar(150) -> Veritabanı şişmez!
//                .HasColumnName("BookTitle"); // SQL tarafındaki sütun adını özelleştirdik

//            builder.Property(b => b.Price)
//                .IsRequired()
//                .HasColumnType("decimal(18,2)"); // Para birimi için hassas veri tipi

//            // 3. Performans ve Hız (Indeksleme)
//            builder.HasIndex(b => b.Title)
//                .IsUnique(); // Aynı isimde iki kitap girilemez ve aramalarda çok daha hızlı bulunur!

//            // 4. Seed Data (Tohum Veri)
//            // Gerçek dünyada HasData'yı genellikle sabitler (ROLLER, KATEGORİLER) için kullanırız.
//            builder.HasData(
//                new Book { Id = 1, Title = "Karagöz ve Hacivat", Price = 75 },
//                new Book { Id = 2, Title = "Mesnevi", Price = 175 },
//                new Book { Id = 3, Title = "Devlet", Price = 375 }
//            );
//        }
//    }
//}