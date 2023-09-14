using _2001206945_VanHuuDung.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace _2001206945_VanHuuDung.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        Database1Entities1 db = new Database1Entities1();
        // GET: Admin/Product
        public ActionResult DSSanPham(string searchstring = "", int page = 1)
        {
            if (searchstring != "")
            {
                List<Product>  products = db.Products.Where(n => n.ProductsName.Contains(searchstring)).ToList();

                int NoOfRecordPerPage = 5;
                int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
                int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
                ViewBag.page = page;
                ViewBag.NoOfPages = NoOfPages;
                products = products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
                return View(products);
            }
            else
            {
                List<Product>  products = db.Products.ToList();

                int NoOfRecordPerPage = 5;
                int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(NoOfRecordPerPage)));
                int NoOfRecordToSkip = (page - 1) * NoOfRecordPerPage;
                ViewBag.page = page;
                ViewBag.NoOfPages = NoOfPages;
                products = products.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();
                return View(products);
            }
            
        }

        public ActionResult Create()
        {
            Database1Entities1 db = new Database1Entities1();
            ViewBag.brand = db.Brands.ToList();
            ViewBag.gender = db.Genders.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product pro, HttpPostedFileBase Avatar)
        {
            db.Products.Add(pro);
            db.SaveChanges();
            if (Avatar != null && Avatar.ContentLength > 0)
            {

                int id = int.Parse(db.Products.ToList().Last().ProductsId.ToString());

                string _fileName = "";
                int index = Avatar.FileName.IndexOf('.');
                _fileName = "sp" + id.ToString() + "." + Avatar.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Images/"), _fileName);
                Avatar.SaveAs(_path);
                Product pro1 = db.Products.FirstOrDefault(x => x.ProductsId == id);
                pro1.Avatar = _fileName;
                db.SaveChanges();
            }

            return RedirectToAction("DSSanPham");

        }

        public ActionResult Delete(int id)
        {
            Database1Entities1 db = new Database1Entities1();
            Product pro = db.Products.Where(row => row.ProductsId == id).FirstOrDefault();
            return View(pro);
        }
        [HttpPost]
        public ActionResult Delete(int id, Product pro)
        {
            Database1Entities1 db = new Database1Entities1();
            Product pro2 = db.Products.Where(row => row.ProductsId == id).FirstOrDefault();
            db.Products.Remove(pro2);
            db.SaveChanges();
            return RedirectToAction("DSSanPham");
        }

        public ActionResult Edit(int id)
        {
            Product pro2 = db.Products.Where(row => row.ProductsId == id).FirstOrDefault();

            ViewBag.brand = db.Brands.ToList();
            ViewBag.gender = db.Genders.ToList();
            return View(pro2);
        }
        [HttpPost]
        public ActionResult Edit(Product pro, HttpPostedFileBase Avatar)
        {
            Product pro2 = db.Products.Where(row => row.ProductsId == pro.ProductsId).FirstOrDefault();
            pro2.ProductsId = pro.ProductsId;
            pro2.ProductsName = pro.ProductsName;
            pro2.Price = pro.Price;
            pro2.PriceDiscount = pro.PriceDiscount;
            pro2.ShortDes = pro.ShortDes;
            pro2.FullDescription = pro.FullDescription;
            pro2.BrandsId = pro.BrandsId;
            pro2.GendersId = pro.GendersId;
            if (Avatar != null && Avatar.ContentLength > 0)
            {

                int id = pro.ProductsId;

                string _fileName = "";
                int index = Avatar.FileName.IndexOf('.');
                _fileName = "sp" + id.ToString() + "." + Avatar.FileName.Substring(index + 1);
                string _path = Path.Combine(Server.MapPath("~/Images/"), _fileName);
                Avatar.SaveAs(_path);

                pro.Avatar = _fileName;
            }
            db.SaveChanges();
            return RedirectToAction("DSSanPham");
        }
    }
}