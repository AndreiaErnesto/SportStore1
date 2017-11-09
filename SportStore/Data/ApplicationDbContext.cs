using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class ApplicationDbContext : DbContext{
        //Para puder criar a base de dados
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //Para as tabelas - só tenho uma tabela de produtos, se tivesse mais tinha de as colocar abaixo
        public DbSet<Product> Products { get; set; }
    }
}
