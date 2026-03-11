using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext : DbContext
    {
        //Burası bizim Veritabanının tablolarını yönettiğimiz yer: Somuttur.

        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }



        //Model oluşturulurken bu model dikkate alınacak. (Sıfırdan)
        protected override void OnModelCreating(ModelBuilder modelBuilder) //Sadece BookConfig i configure edecek.
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
