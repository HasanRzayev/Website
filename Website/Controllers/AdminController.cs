using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Linq;
using Website.Data;
using Website.Models;
using Website.Models.Entity;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class AdminController : Controller
    {
        public async static Task<string> UploadFile(IFormFile file) { return "url"; }
        private AppDbContext baza;
        public AdminController(AppDbContext dbContext)
        {
            baza = dbContext;
        }

        public IActionResult UpdateProduct(int Id)
        {
            var lazim = (baza.Products.ToList().Find(p => p.Id == Id));
            ViewBag.nomre = Id;
            ViewBag.Categories = new SelectList(baza.Catalogues, "Id", "Name");
            return View(lazim);
        }
        [HttpPost, ActionName("UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {

            if (product.Name != null)
            {

                Product lazim = baza.Products.Find(product.Id);
                lazim.Name = product.Name;
                lazim.catalogue_id = product.catalogue_id;
                baza.SaveChanges();
            }
            return RedirectToAction("Product");
        }

        
        public IActionResult UpdateCatalogue(int Id)
        {
            var lazim=(baza.Catalogues.ToList().Find(p => p.Id == Id));
            ViewBag.nomre = Id;
            return View(lazim);
        }
        [HttpPost, ActionName("UpdateCatalogue")]
        public IActionResult UpdateCatalogue(Catalogue catalogue)
        {

            if (catalogue.Name != null)
            {

                Catalogue lazim = baza.Catalogues.Find(catalogue.Id);
                lazim.Name = catalogue.Name;
                baza.SaveChanges();
            }
            return RedirectToAction("Catalogue");
        }
        [HttpGet]
        public IActionResult AddCatalogue()
        {
            return View();
        }

        //public ActionResult FileUpload(HttpPostedFileBase file)
        //{
        //    if (file != null)
        //    {
        //        string pic = System.IO.Path.GetFileName(file.FileName);
        //        string path = System.IO.Path.Combine(
        //                               Server.MapPath("~/images/profile"), pic);
        //        // file is uploaded
        //        file.SaveAs(path);

        //        // save the image path path to the database or you can send image 
        //        // directly to database
        //        // in-case if you want to store byte[] ie. for DB
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            file.InputStream.CopyTo(ms);
        //            byte[] array = ms.GetBuffer();
        //        }

        //    }
        //    // after successfully uploading redirect the user
        //    return RedirectToAction("actionname", "controller name");
        //}

        [HttpPost]
        public IActionResult AddCatalogue(AddCatalogueViewModel catalogue)
        {
            if (ModelState.IsValid)
            {
               
                Catalogue lazim = new Catalogue();
                lazim.Name = catalogue.Name;
                baza.Catalogues.Add(lazim);
                baza.SaveChanges();
                
                return RedirectToAction("Catalogue");
            }
            return View();

        }
        public IActionResult Catalogue()
        {
            var produtcs = new List<Catalogue>();
            foreach (var ct in baza.Catalogues.ToList())
            {
                produtcs.Add(new Catalogue()
                {
                    Id = ct.Id,
                    Name = ct.Name,
                });
            }
           
            return View(produtcs);
        }

        public IActionResult Product()
        {

            var model = baza.Catalogues.Include(a => a.Products).ToList();
            var produtcs = new List<Product>();
            foreach (var ct in baza.Products.ToList())
            {
                produtcs.Add(new Product()
                {
                    Id = ct.Id,
                    Name = ct.Name,
                    price=ct.price,
                    Catalogue=ct.Catalogue,
                });
            }

            return View(produtcs);
        }
        public ActionResult DeleteProduct(int Id)
        {

            baza.Products.Remove(baza.Products.ToList().Find(p => p.Id == Id));
            baza.SaveChanges();
            return RedirectToAction("Product", new { id = 0 });
        }
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(baza.Catalogues, "Id", "Name");
            return View();
        }
        [HttpPost, ActionName("AddProduct")]
        public IActionResult AddProduct(AddProduceViewModel product)
        {

            if (product.Name != null)
            {


                Product lazim = new Product();
                lazim.Name = product.Name;
                lazim.catalogue_id = product.catolugue_id;

                baza.Products.Add(lazim);
                baza.SaveChanges();


            }
            return RedirectToAction("Product");
        }
        public ActionResult Delete(int Id)
        {
            //var lazim = baza.Products.ToList().Find(p => p.catalogue_id == Id);
            //if (baza.Products.First())
            //{

            //}
            baza.Catalogues.Remove(baza.Catalogues.ToList().Find(p => p.Id == Id));
            baza.SaveChanges();
            return RedirectToAction("Catalogue", new { id = 0 });
        }

      
       
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}