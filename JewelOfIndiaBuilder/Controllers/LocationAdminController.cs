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
    public class LocationAdminController : Controller
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        public bool Redirect()
        {
            return !ApplicationSecurity.CheckUser(Session["UserType"].ToString(), Session["IsAdmin"].ToString(), "LOCATION");
        }
        // GET: /LocationAdmin/
        public ActionResult Index()
        {
            if (Redirect())
                return RedirectToAction("../Home");
            return View(db.Locations.ToList());
        }

        // GET: /LocationAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: /LocationAdmin/Create
        public ActionResult Create()
        {
            if (Redirect())
                return RedirectToAction("../Home");
            return View();
        }

        // POST: /LocationAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Address,ZipCode,state,Country,Latitude,Longitude")] Location location)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: /LocationAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: /LocationAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Address,ZipCode,state,Country,Latitude,Longitude")] Location location)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (ModelState.IsValid)
            {
                db.Entry(location).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: /LocationAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: /LocationAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
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
