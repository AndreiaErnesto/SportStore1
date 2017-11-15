using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class ProductsController : Controller {
        //need to get the repository - service
        private IProductRepository repository;

        public int PageSize = 4;

        public ProductsController(IProductRepository repository) {
            this.repository = repository;
        }

        public ViewResult List(int page = 1) {
            return View(
                repository.Products
                    .OrderBy(p => p.Price)
                    .Skip(PageSize * (page - 1))
                    .Take(PageSize)
                );
        }
    }
}