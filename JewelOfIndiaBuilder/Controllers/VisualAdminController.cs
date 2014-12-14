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
        public ActionResult Index(string Type, string Id)
        {
            ViewData.Add("Type", Type);
            ViewData.Add("Id", Id);
            Int32 TypeId = Convert.ToInt32(Id);

            if (Type == "Property")
            {
                return View(db.Visuals.Where(v => v.Type.Equals("P") && v.TypeId.Equals(TypeId)).ToList());
            }
            if (Type == "Tower")
            {
                return View(db.Visuals.Where(v => v.Type.Equals("T") && v.TypeId.Equals(TypeId)).ToList());
            }
            if (Type == "Apartment")
            {
                return View(db.Visuals.Where(v => v.Type.Equals("A") && v.TypeId.Equals(TypeId)).ToList());
            }
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
        public ActionResult Create(string Type, string Id)
        {
            ViewData.Add("Type", Type);
            ViewData.Add("Id", Id);
            return View();
        }

        // POST: /VisualAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<HttpPostedFileBase> files)
        {
            string type = "";
            string id = "";
            string typevalue = "";
            type = Request.Form["Type"];
            id = Request.Form["Id"];
            if (type == "Property")
            {
                typevalue = "P";
            }
            if (type == "Tower")
            {
                typevalue = "T";
            }
            if (type == "Apartment")
            {
                typevalue = "A";
            }
            if (ModelState.IsValid)
            {
                for(int i=0;i<Request.Files.Count;i++)
                {
                    string path = @"..\..\Images\";

                    HttpPostedFileBase photo = Request.Files[i];

                    if (photo.ContentLength != 0)
                    {
                        photo.SaveAs(Server.MapPath(path) + photo.FileName);
                        Visual v = new Visual();
                        v.DisplayName = photo.FileName;
                        v.Name = Guid.NewGuid().ToString() + photo.FileName.Substring(photo.FileName.IndexOf('.'),4);
                        v.Type = typevalue;
                        v.TypeId = Convert.ToInt32(id);
                        db.Visuals.Add(v);
                        db.SaveChanges();
                    }


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
