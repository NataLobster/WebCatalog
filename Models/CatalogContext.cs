using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCatalog.Models
{
    public class CatalogContext : DbContext
    {
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CatalogProd> CatalogProds { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        public CatalogContext()
        {
        }
    }
}
