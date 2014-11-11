using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JewelOfIndiaBuilder.Models;

namespace JewelOfIndiaBuilder.Controllers
{
    public class VisualController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/Visual
        public IQueryable<Visual> GetVisuals()
        {
            return db.Visuals;
        }

        // GET api/Visual/5
        [ResponseType(typeof(Visual))]
        public IHttpActionResult GetVisual(long id)
        {
            Visual visual = db.Visuals.Find(id);
            if (visual == null)
            {
                return NotFound();
            }

            return Ok(visual);
        }

        // PUT api/Visual/5
        public IHttpActionResult PutVisual(long id, Visual visual)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visual.Id)
            {
                return BadRequest();
            }

            db.Entry(visual).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisualExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Visual
        [ResponseType(typeof(Visual))]
        public IHttpActionResult PostVisual(Visual visual)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Visuals.Add(visual);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VisualExists(visual.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = visual.Id }, visual);
        }

        // DELETE api/Visual/5
        [ResponseType(typeof(Visual))]
        public IHttpActionResult DeleteVisual(long id)
        {
            Visual visual = db.Visuals.Find(id);
            if (visual == null)
            {
                return NotFound();
            }

            db.Visuals.Remove(visual);
            db.SaveChanges();

            return Ok(visual);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VisualExists(long id)
        {
            return db.Visuals.Count(e => e.Id == id) > 0;
        }
    }
}