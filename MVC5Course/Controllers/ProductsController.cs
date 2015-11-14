using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using Omu.ValueInjecter;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities db = new FabricsEntities();
        private ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(string search)
        {
            //return View(db.Product.ToList());

            //var data = db.Product.AsQueryable();
            var data = repo.GetBySearchKey(search);
            //data = data.Where(p => p.ProductName.Contains("100"));
            //data = data.Where(p => p.ProductId < 10);
            //if (!string.IsNullOrEmpty(search))
            //    data = data.Where(p => p.ProductName.Contains(search));
            data = data.OrderByDescending(p => p.ProductName);

            //var data1 = from p in db.Product
            //            where p.ProductName.Contains("100")
            //            orderby p.ProductName
            //            select p;
            
            //var data2 = from p in db.Product
            //            where p.ProductName.Contains("100")
            //            orderby p.ProductName
            //            select new NewProductVM
            //            {
            //                ProductName = p.ProductName,
            //                Price = p.Price
            //            };

            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.GetById(id);
            //Product product = db.Product.Where(p => p.ProductId == id && p.Active == true).FirstOrDefault();
            //Product product = db.Product.FirstOrDefault(p => p.ProductId == id && p.Active == true);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);

            // 方法一
            //var orderLines = product.OrderLine.Where(p => p.ProductId == product.ProductId);
            // 方法二
            //var orderLines = product.OrderLine.ToList();
            //foreach (var item in orderLines)
            //{
            //    db.OrderLine.Remove(item);
            //}
            // 方法三
            db.OrderLine.RemoveRange(product.OrderLine);

            db.Product.Remove(product);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(NewProductVM product)
        {
            if (ModelState.IsValid) {

                //var prod = new Product();
                //prod.ProductName = product.ProductName;
                //prod.Price = product.Price;

                var prod = Mapper.Map<Product>(product); 

                prod.Stock = 1;
                prod.Active = true;

                db.Product.Add(prod);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    //throw ex;
                    var allErrors = new List<string>();

                    foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
                    {
                        foreach (DbValidationError err in re.ValidationErrors)
                        {
                            allErrors.Add(err.ErrorMessage);
                        }
                    }

                    ViewBag.Errors = allErrors;

                    return Content(String.Join("<br>", allErrors.ToArray()));
                }

                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult BatchUpdate()
        {
            //較有效能寫法
            //db.Database.ExecuteSqlCommand("update product set price=5 where productid < @p0", 10);
            //效能較差寫法
            //var data = db.Product.AsQueryable();
            //data = data.Where(p => p.ProductId<10);
            var data = repo.Get取得前面10筆範例資料();
            foreach (var item in data)
            {
                item.Price = 5;
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //throw ex;
                var allErrors = new List<string>();

                foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in re.ValidationErrors)
                    {
                        allErrors.Add(err.ErrorMessage);
                    }
                }

                ViewBag.Errors = allErrors;

                return Content(String.Join("<br>", allErrors.ToArray()));
            }

            return RedirectToAction("Index");
        }

    }
}
