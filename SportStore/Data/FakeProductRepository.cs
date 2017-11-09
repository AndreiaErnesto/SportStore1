using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class FakeProductRepository : IProductRepository //implements é :
    {
        public IEnumerable<Product> Products => new List<Product>{
            new Product { Name = "Football", Price = 25},
            new Product { Name = "Football", Price = 179},
            new Product { Name = "Football", Price = 95, Description="You will be a champion!!"}
        };
    }
}
