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
using EntityState = System.Data.Entity.EntityState;

namespace JewelOfIndiaBuilder.Controllers
{
    public class FeatureTypeController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/FeatureType
        public IQueryable<FeatureType> GetFeatureTypes()
        {
            return db.FeatureTypes;
        }

        // GET api/FeatureType/5
        [ResponseType(typeof(FeatureType))]
        public IHttpActionResult GetFeatureType(int id)
        {
            FeatureType featuretype = db.FeatureTypes.Find(id);
            if (featuretype == null)
            {
                return NotFound();
            }

            return Ok(featuretype);
        }

        // PUT api/FeatureType/5
        public IHttpActionResult PutFeatureType(int id, FeatureType featuretype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != featuretype.Id)
            {
                return BadRequest();
            }

            db.Entry(featuretype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeatureTypeExists(id))
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

        // POST api/FeatureType
        [ResponseType(typeof(FeatureType))]
        public IHttpActionResult PostFeatureType(FeatureType featuretype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FeatureTypes.Add(featuretype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FeatureTypeExists(featuretype.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = featuretype.Id }, featuretype);
        }

        // DELETE api/FeatureType/5
        [ResponseType(typeof(FeatureType))]
        public IHttpActionResult DeleteFeatureType(int id)
        {
            FeatureType featuretype = db.FeatureTypes.Find(id);
            if (featuretype == null)
            {
                return NotFound();
            }

            db.FeatureTypes.Remove(featuretype);
            db.SaveChanges();

            return Ok(featuretype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FeatureTypeExists(int id)
        {
            return db.FeatureTypes.Count(e => e.Id == id) > 0;
        }
    }
}