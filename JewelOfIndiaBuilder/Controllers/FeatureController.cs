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
    public class FeatureController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/Feature
        public IQueryable<Feature> GetFeatures()
        {
            return db.Features;
        }

        // GET api/Feature/5
        [ResponseType(typeof(Feature))]
        public IHttpActionResult GetFeature(long id)
        {
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return NotFound();
            }

            return Ok(feature);
        }

        // PUT api/Feature/5
        public IHttpActionResult PutFeature(long id, Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feature.Id)
            {
                return BadRequest();
            }

            db.Entry(feature).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeatureExists(id))
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

        // POST api/Feature
        [ResponseType(typeof(Feature))]
        public IHttpActionResult PostFeature(Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Features.Add(feature);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FeatureExists(feature.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = feature.Id }, feature);
        }

        // DELETE api/Feature/5
        [ResponseType(typeof(Feature))]
        public IHttpActionResult DeleteFeature(long id)
        {
            Feature feature = db.Features.Find(id);
            if (feature == null)
            {
                return NotFound();
            }

            db.Features.Remove(feature);
            db.SaveChanges();

            return Ok(feature);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeatureExists(long id)
        {
            return db.Features.Count(e => e.Id == id) > 0;
        }
    }
}