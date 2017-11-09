using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class EFProductRepository : IProductRepository{
        private ApplicationDbContext dbContext; //contexto da Base de dados

        //Construtor do contexto da base de dados
        public EFProductRepository(ApplicationDbContext dbContext) //recebe como parametro a base de dados
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Product> Products => dbContext.Products;
    }
}
