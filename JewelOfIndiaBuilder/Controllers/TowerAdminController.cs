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
    public class TowerAdminController : Controller
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET: /TowerAdmin/
        public ActionResult Index()
        {
            var towers = db.Towers.Include(t => t.Property);
            return View(towers.ToList());
        }

        // GET: /TowerAdmin/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tower tower = db.Towers.Find(id);
            if (tower == null)
            {
                return HttpNotFound();
            }
            return View(tower);
        }

        // GET: /TowerAdmin/Create
        public ActionResult Create()
        {
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Feature");
            return View();
        }

        // POST: /TowerAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,PropertyId,TowerName,TowerDirection,Description")] Tower tower)
        {
            if (ModelState.IsValid)
            {
                db.Towers.Add(tower);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Feature", tower.PropertyId);
            return View(tower);
        }

        // GET: /TowerAdmin/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tower tower = db.Towers.Find(id);
            if (tower == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Feature", tower.PropertyId);
            return View(tower);
        }

        // POST: /TowerAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,PropertyId,TowerName,TowerDirection,Description")] Tower tower)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tower).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyId = new SelectList(db.Properties, "Id", "Feature", tower.PropertyId);
            return View(tower);
        }

        // GET: /TowerAdmin/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tower tower = db.Towers.Find(id);
            if (tower == null)
            {
                return HttpNotFound();
            }
            return View(tower);
        }

        // POST: /TowerAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Tower tower = db.Towers.Find(id);
            db.Towers.Remove(tower);
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
