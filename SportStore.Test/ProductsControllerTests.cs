using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Models;
using System;
using Xunit;
using System.Linq;
using SportStore.Controllers;
using System.Collections.Generic;

namespace SportStore.Test
{
    public class ProductsControllerTests
    {
        [Fact] //UM TESTE QUE QUERO CORRER
        public void CanPaginate()
        {
            //Regra dos três As
            //Arrange - o que for necessário para o teste - variáveis
            Mock<IProductRepository> mockProdRep = new Mock<IProductRepository>();

            mockProdRep.Setup(m => m.Products)
                .Returns(
                    new Product[]
                    {
                        new Product { ProductID = 1, Name = "P1" },
                        new Product { ProductID = 2, Name = "P2" },
                        new Product { ProductID = 3, Name = "P3" },
                        new Product { ProductID = 4, Name = "P4" },
                        new Product { ProductID = 5, Name = "P5" },
                        new Product { ProductID = 6, Name = "P6" },
                        new Product { ProductID = 7, Name = "P7" },
                        new Product { ProductID = 8, Name = "P8" }
                    }
            );

            IProductRepository repository = mockProdRep.Object;

            ProductsController controller = new ProductsController(repository);  


            //Act - fazer o teste
            controller.PageSize = 3;

            ViewResult viewResult = controller.List(3);

            IEnumerable<Product> productsList = (IEnumerable<Product>) viewResult.Model;

            

            //Assert - confirmar se o teste funcionou
            Product[] products = productsList.ToArray();    

            Assert.Equal(products.Count(),2);
    
            Assert.True(products[0].Name == "P7");
            //P7


            Assert.True(products[1].Name == "P8");
            //P8
        }
    }
}
