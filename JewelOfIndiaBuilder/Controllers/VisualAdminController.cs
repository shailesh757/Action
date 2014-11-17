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
    public class VisualAdminController : Controller
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET: /VisualAdmin/
        public ActionResult Index()
        {
            return View(db.Visuals.ToList());
        }

        // GET: /VisualAdmin/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visual visual = db.Visuals.Find(id);
            if (visual == null)
            {
                return HttpNotFound();
            }
            return View(visual);
        }

        // GET: /VisualAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /VisualAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                for(int i=0;i<Request.Files.Count;i++)
                {
                    string path = @"D:\CTS\";

                    HttpPostedFileBase photo = Request.Files[i];

                    if (photo.ContentLength != 0)
                        photo.SaveAs(path + photo.FileName);

                }
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: /VisualAdmin/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visual visual = db.Visuals.Find(id);
            if (visual == null)
            {
                return HttpNotFound();
            }
            return View(visual);
        }

        // POST: /VisualAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageName")] Visual visual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visual).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visual);
        }

        // GET: /VisualAdmin/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visual visual = db.Visuals.Find(id);
            if (visual == null)
            {
                return HttpNotFound();
            }
            return View(visual);
        }

        // POST: /VisualAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Visual visual = db.Visuals.Find(id);
            db.Visuals.Remove(visual);
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
