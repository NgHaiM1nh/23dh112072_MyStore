using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _23dh112072_MyStore.Models;
using _23dh112072_MyStore.Models.ViewModel;
using PagedList;
using PagedList.Mvc;

namespace _23dh112072_MyStore.Controllers
{
    public class HomeController : Controller
    {
        private MyStoreEntities db = new MyStoreEntities();

        public ActionResult ProductDetails(int ? id,int ? quantity,int ? page)
        {
                if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
                Product pro=db.Products.Find(id);
            if(pro==null)
            {
                return HttpNotFound();
            }
            var products = db.Products.Where(p => p.CategoryID == pro.CategoryID && p.ProductID != pro.ProductID).AsQueryable();
            ProductDetailsVM model= new ProductDetailsVM();

            int pageNumber = page ?? 1;
            int pageSize = model.PageSize;
            model.product = pro;
            model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToPagedList(pageNumber, pageSize).ToList();
            model.TopProducts = products.OrderByDescending(p=>p.OrderDetails.Count()).Take(8).ToPagedList(pageNumber,pageSize);

            if(quantity.HasValue)
            {
                model.quantity=quantity.Value;
            }

            return View(model);
        }
       
        public ActionResult Index(string searchTerm, int ? page)
        {
            var model = new HomeProductVm();
            var products = db.Products.AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                products = products.Where(p =>
                        p.ProductName.Contains(searchTerm) ||
                        p.ProductDescription.Contains(searchTerm) ||
                        p.Category.CategoryName.Contains(searchTerm));
            }

            int pageNumber = page ?? 1;
            int pageSize = 6;

            model.FeaturedProduct=products.OrderByDescending(p => p.OrderDetails.Count()).Take(10).ToList();

            model.NewProducts=products.OrderBy(p => p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}