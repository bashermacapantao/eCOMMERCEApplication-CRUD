using eCOMMERCEApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;


namespace eCOMMERCEApplication.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            using (ProductDBEntities db = new ProductDBEntities())
            {
                List<tblProduct> ProductList = (from data in db.tblProducts select data).ToList();

                return View(ProductList);
            }
            
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new tblProduct());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(tblProduct product, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add insert logic here
                string extension = Path.GetExtension(postedFile.FileName);
                if (extension.Equals(".jpg") || extension.Equals(".png"))
                {
                    string filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                    string savepath = Server.MapPath("~/Content/Images/");
                    postedFile.SaveAs(savepath + filename);
                    product.prod_pic = filename;
                    using (ProductDBEntities db = new ProductDBEntities())
                    {
                        db.tblProducts.Add(product);
                        db.SaveChanges();
                    }

                }
                else
                {
                    return Content("<h1>You can only upload JPG or PNG Files!</h1>");
                    
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            using (ProductDBEntities db = new ProductDBEntities())
            {
                //tblProduct product = (from data in db.tblProducts where data.prod_id == id select data).Single();
                tblProduct product = db.tblProducts.Find(id);
                return View(product);
            }
            
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(tblProduct product, HttpPostedFileBase postedFile)
        {
            try
            {
                // TODO: Add update logic here
                string filename = "";
                if (postedFile !=null)
                {
                    string extension = Path.GetExtension (postedFile.FileName);
                    if (extension.Equals(".jpg") || extension.Equals("png"))
                    {
                        filename = "IMG-" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + extension;
                        string savepath = Server.MapPath("~/Content/Images/");
                        postedFile.SaveAs (savepath + filename);
                    }
                }
                using (ProductDBEntities db = new ProductDBEntities())
                {
                    tblProduct tbl = db.tblProducts.Find(product.prod_id);
                    tbl.prod_name = product.prod_name;
                    tbl.prod_price = product.prod_price;
                    tbl.prod_qty = product.prod_qty;
                    if (!filename.Equals(""))
                    {
                        tbl.prod_pic = filename;
                    }
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (ProductDBEntities db = new ProductDBEntities())
            {
                db.tblProducts.Remove(db.tblProducts.Find(id));
                db.SaveChanges ();
                return RedirectToAction("Index");
            }
           
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
