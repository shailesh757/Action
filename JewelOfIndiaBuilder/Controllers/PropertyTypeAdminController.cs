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
    public class PropertyTypeAdminController : Controller
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET: /PropertyTypeAdmin/
        public ActionResult Index()
        {
            return View(db.PropertyTypes.ToList());
        }

        // GET: /PropertyTypeAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyType propertytype = db.PropertyTypes.Find(id);
            if (propertytype == null)
            {
                return HttpNotFound();
            }
            return View(propertytype);
        }

        // GET: /PropertyTypeAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PropertyTypeAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,CodeReference")] PropertyType propertytype)
        {
            if (ModelState.IsValid)
            {
                db.PropertyTypes.Add(propertytype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(propertytype);
        }

        // GET: /PropertyTypeAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyType propertytype = db.PropertyTypes.Find(id);
            if (propertytype == null)
            {
                return HttpNotFound();
            }
            return View(propertytype);
        }

        // POST: /PropertyTypeAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,CodeReference")] PropertyType propertytype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(propertytype).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(propertytype);
        }

        // GET: /PropertyTypeAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PropertyType propertytype = db.PropertyTypes.Find(id);
            if (propertytype == null)
            {
                return HttpNotFound();
            }
            return View(propertytype);
        }

        // POST: /PropertyTypeAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PropertyType propertytype = db.PropertyTypes.Find(id);
            db.PropertyTypes.Remove(propertytype);
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
    }
}
