﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class OrderLinesController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        //[ChildActionOnly]
        // GET: OrderLines
        public ActionResult Index(int productId, string OrderStatus)
        {
            ViewBag.OrderStatusSelected = OrderStatus;

            ViewBag.productId = productId;

            var lsOrderStatus = from row in db.OrderLine
                                where row.ProductId == productId
                                group row by row.Order.OrderStatus into g
                                select g.Key;

            ViewBag.OrderStatus = new SelectList(lsOrderStatus); 

            //var orderLine = db.OrderLine.Include(o => o.Order).Include(o => o.Product);
            var orderLine = db.OrderLine.Where(p => p.ProductId == productId &&
                                               (!string.IsNullOrEmpty(OrderStatus) ? p.Order.OrderStatus == OrderStatus : true));
            return PartialView("Index", orderLine.ToList());
        }

        // GET: OrderLines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = db.OrderLine.Find(id);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // GET: OrderLines/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Order, "OrderId", "OrderStatus");
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName");
            return View();
        }

        // POST: OrderLines/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,LineNumber,ProductId,Qty,LineTotal")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                db.OrderLine.Add(orderLine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Order, "OrderId", "OrderStatus", orderLine.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", orderLine.ProductId);
            return View(orderLine);
        }

        // GET: OrderLines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = db.OrderLine.Find(id);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Order, "OrderId", "OrderStatus", orderLine.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", orderLine.ProductId);
            return View(orderLine);
        }

        // POST: OrderLines/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,LineNumber,ProductId,Qty,LineTotal")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderLine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Order, "OrderId", "OrderStatus", orderLine.OrderId);
            ViewBag.ProductId = new SelectList(db.Product, "ProductId", "ProductName", orderLine.ProductId);
            return View(orderLine);
        }

        //// GET: OrderLines/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderLine orderLine = db.OrderLine.Find(id);
        //    if (orderLine == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderLine);
        //}

        // POST: OrderLines/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int OrderId, int LineNumber, string OrderStatus)
        {
            OrderLine ol = db.OrderLine.Find(OrderId, LineNumber);
            db.OrderLine.Remove(ol);
            db.SaveChanges();
            //return RedirectToAction("Index");

            //var productId = ol.ProductId;
            //ViewBag.productId = productId;

            //var lsOrderStatus = from row in db.OrderLine
            //                    where row.ProductId == productId
            //                    group row by row.Order.OrderStatus into g
            //                    select g.Key;

            //ViewBag.OrderStatus = new SelectList(lsOrderStatus);

            ////var orderLine = db.OrderLine.Include(o => o.Order).Include(o => o.Product);
            //var orderLine = db.OrderLine.Where(p => p.ProductId == productId &&
            //                                   (!string.IsNullOrEmpty(OrderStatus) ? p.Order.OrderStatus == OrderStatus : true));
            //return PartialView("Index", orderLine.ToList());
            return Index(ol.ProductId, OrderStatus);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
