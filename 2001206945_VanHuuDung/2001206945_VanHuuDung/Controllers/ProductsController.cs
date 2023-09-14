using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _2001206945_VanHuuDung.Models;

namespace _2001206945_VanHuuDung.Controllers
{
    public class ProductsController : Controller
    {
        Database1Entities1 db = new Database1Entities1();
        // GET: Products
        public ActionResult Detail(int Id)
        {
            var objproduct = db.Products.Where(n => n.ProductsId == Id).FirstOrDefault();
            return View(objproduct);
        }

        public ActionResult Gender(int Id)
        {
            var objproduct = db.Products.Where(n => n.GendersId == Id).ToList();
            return View(objproduct);
        }

        public ActionResult index(int page = 1)
        {
            List<Product> objproduct = db.Products.ToList();

            int NoOfRecordPerPage = 10;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(objproduct.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
            ViewBag.page = page;
            ViewBag.NoOfPages = NoOfPages;
            objproduct = objproduct.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            return View(objproduct);
        }
    }
}