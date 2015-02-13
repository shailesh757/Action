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
    public class PropertyAdminController : Controller
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        public bool Redirect()
        {
            return !ApplicationSecurity.CheckUser(Session["UserType"].ToString(), Session["IsAdmin"].ToString(), "PROPERTY");
        }
        // GET: /PropertyAdmin/
        public ActionResult Index()
        {
            if (Redirect())
                return RedirectToAction("../Home");
            var properties = db.Properties.Include(p => p.Location).Include(p => p.PropertyType);
            return View(properties.ToList());
        }

        // GET: /PropertyAdmin/Details/5
        public ActionResult Details(long? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: /PropertyAdmin/Create
        public ActionResult Create()
        {
            if (Redirect())
                return RedirectToAction("../Home");
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Address");
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "CodeReference");
            return View();
        }

        // POST: /PropertyAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Feature,Description,LocationId,PropertyTypeId")] Property property)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Address", property.LocationId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "CodeReference", property.PropertyTypeId);
            return View(property);
        }

        // GET: /PropertyAdmin/Edit/5
        public ActionResult Edit(long? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Address", property.LocationId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "CodeReference", property.PropertyTypeId);
            return View(property);
        }

        // POST: /PropertyAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Feature,Description,LocationId,PropertyTypeId")] Property property)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (ModelState.IsValid)
            {
                db.Entry(property).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Address", property.LocationId);
            ViewBag.PropertyTypeId = new SelectList(db.PropertyTypes, "Id", "CodeReference", property.PropertyTypeId);
            return View(property);
        }

        // GET: /PropertyAdmin/Delete/5
        public ActionResult Delete(long? id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: /PropertyAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (Redirect())
                return RedirectToAction("../Home");
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
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
