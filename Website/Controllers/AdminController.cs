using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Website.Data;
using Website.Models;
using Website.Models.Entity;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class AdminController : Controller
    {
        
        private AppDbContext baza;
        public AdminController(AppDbContext dbContext)
        {
            baza = dbContext;


        }
        public IActionResult Product()
        {
            var produtcs = new List<ProduceViewModel>();
            foreach (var produce in baza.Products.Include("Catalogue"))
            {
                produtcs.Add(new ProduceViewModel()
                {
                    Name = produce.Name,
                    Price = produce.price,
                    image_url = produce.image_url,
                    Catalogue_Name = produce.Catalogue?.Name
                });
            }
            ViewData["allproduce"] = produtcs;
            return View();
        }
        public IActionResult Catalogue()
        {
            return View();
        }
        [HttpPost, ActionName("AddProduct")]
        public IActionResult AddProduct(Product product)
        {
            
            if (product.Name != null)
            {


                Product lazim = new Product();
                lazim.Name = product.Name;
                lazim.catalogue_id = product.catalogue_id;
                baza.Products.Add(lazim);
                baza.SaveChanges();
                

            }
            return RedirectToAction("AllProducts");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}