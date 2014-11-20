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
    public class FeatureTypeAdminController : Controller
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET: /FeatureTypeAdmin/
        public ActionResult Index()
        {
            return View(db.FeatureTypes.ToList());
        }

        // GET: /FeatureTypeAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featuretype = db.FeatureTypes.Find(id);
            if (featuretype == null)
            {
                return HttpNotFound();
            }
            return View(featuretype);
        }

        // GET: /FeatureTypeAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /FeatureTypeAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Code,Name,Description")] FeatureType featuretype)
        {
            if (ModelState.IsValid)
            {
                db.FeatureTypes.Add(featuretype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(featuretype);
        }

        // GET: /FeatureTypeAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featuretype = db.FeatureTypes.Find(id);
            if (featuretype == null)
            {
                return HttpNotFound();
            }
            return View(featuretype);
        }

        // POST: /FeatureTypeAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Code,Name,Description")] FeatureType featuretype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(featuretype).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(featuretype);
        }

        // GET: /FeatureTypeAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FeatureType featuretype = db.FeatureTypes.Find(id);
            if (featuretype == null)
            {
                return HttpNotFound();
            }
            return View(featuretype);
        }

        // POST: /FeatureTypeAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeatureType featuretype = db.FeatureTypes.Find(id);
            db.FeatureTypes.Remove(featuretype);
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
