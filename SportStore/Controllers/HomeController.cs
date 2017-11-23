using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class HomeController : Controller
    {
        //need to get the repository - service
        private IProductRepository repository;

        public HomeController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View(
                    repository.Products.Where(p => p.Price < 30)
                );
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
