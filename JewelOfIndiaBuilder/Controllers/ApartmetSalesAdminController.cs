using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JewelOfIndiaBuilder.Models;

namespace JewelOfIndiaBuilder.Controllers
{
    public class ApartmetSalesAdminController : Controller
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();
        
        public bool Redirect()
        {
            return !ApplicationSecurity.CheckUser(Session["UserType"].ToString(), Session["IsAdmin"].ToString(), "APARTMENTSALES");
        }
        // GET: ApartmetSalesAdmin
        public ActionResult PropertyReport()
        {
            var props =
                db.Database.SqlQuery<sp_GetApartmentWithStatus_Result>("exec sp_GetApartmentWithStatus")
                    .ToList<sp_GetApartmentWithStatus_Result>();
            return View(props.ToList());
        }

        // GET: ApartmetSalesAdmin
        public ActionResult Index()
        {
            if(Redirect())
                 return RedirectToAction("../Home");
            var apartmetSales = db.ApartmetSales.Include(a => a.Apartment).Include(a => a.Customer).Include(a => a.ApartmentSalesType).Include(a => a.User);
            return View(apartmetSales.ToList());
        }

        // GET: ApartmetSalesAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmetSale apartmetSale = db.ApartmetSales.Find(id);
            if (apartmetSale == null)
            {
                return HttpNotFound();
            }
            return View(apartmetSale);
        }

        // GET: ApartmetSalesAdmin/Create
        public ActionResult Create()
        {
            if (Redirect())
                return RedirectToAction("../Home");
            ViewBag.ApartmentId = new SelectList(db.Apartments, "Id", "Description");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.SalesType = new SelectList(db.ApartmentSalesTypes, "Id", "SalesType");
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: ApartmetSalesAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApartmentId,IsBlocked,CustomerId,UserId,BlockStartDate,BlockEndDate,SalesType,Customer_Name,Broker_Name")] ApartmetSale apartmetSale)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (ModelState.IsValid)
            {
                db.ApartmetSales.Add(apartmetSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApartmentId = new SelectList(db.Apartments, "Id", "Description", apartmetSale.ApartmentId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", apartmetSale.CustomerId);
            ViewBag.SalesType = new SelectList(db.ApartmentSalesTypes, "Id", "SalesType", apartmetSale.SalesType);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", apartmetSale.UserId);
            return View(apartmetSale);
        }

        // GET: ApartmetSalesAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmetSale apartmetSale = db.ApartmetSales.Find(id);
            if (apartmetSale == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApartmentId = new SelectList(db.Apartments, "Id", "Description", apartmetSale.ApartmentId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", apartmetSale.CustomerId);
            ViewBag.SalesType = new SelectList(db.ApartmentSalesTypes, "Id", "SalesType", apartmetSale.SalesType);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", apartmetSale.UserId);
            return View(apartmetSale);
        }

        // POST: ApartmetSalesAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ApartmentId,IsBlocked,CustomerId,UserId,BlockStartDate,BlockEndDate,SalesType,Customer_Name,Broker_Name")] ApartmetSale apartmetSale)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (ModelState.IsValid)
            {
                db.Entry(apartmetSale).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApartmentId = new SelectList(db.Apartments, "Id", "Description", apartmetSale.ApartmentId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", apartmetSale.CustomerId);
            ViewBag.SalesType = new SelectList(db.ApartmentSalesTypes, "Id", "SalesType", apartmetSale.SalesType);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", apartmetSale.UserId);
            return View(apartmetSale);
        }

        // GET: ApartmetSalesAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApartmetSale apartmetSale = db.ApartmetSales.Find(id);
            if (apartmetSale == null)
            {
                return HttpNotFound();
            }
            return View(apartmetSale);
        }

        // POST: ApartmetSalesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            ApartmetSale apartmetSale = db.ApartmetSales.Find(id);
            db.ApartmetSales.Remove(apartmetSale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext != null && filterContext.Exception != null)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
                filterContext.ExceptionHandled = true;
                this.View("Error").ViewData["Exception"] = filterContext.Exception.Message;
                this.View("Error").ExecuteResult(this.ControllerContext);
            }
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
