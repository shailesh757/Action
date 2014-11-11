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
    public class PropertyController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/Property
        public IQueryable<Property> GetProperties()
        {
            return db.Properties;
        }

        // GET api/Property/5
        [ResponseType(typeof(Property))]
        public IHttpActionResult GetProperty(long id)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            return Ok(property);
        }

        // PUT api/Property/5
        public IHttpActionResult PutProperty(long id, Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != property.Id)
            {
                return BadRequest();
            }

            db.Entry(property).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
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

        // POST api/Property
        [ResponseType(typeof(Property))]
        public IHttpActionResult PostProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Properties.Add(property);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = property.Id }, property);
        }

        // DELETE api/Property/5
        [ResponseType(typeof(Property))]
        public IHttpActionResult DeleteProperty(long id)
        {
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return NotFound();
            }

            db.Properties.Remove(property);
            db.SaveChanges();

            return Ok(property);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyExists(long id)
        {
            return db.Properties.Count(e => e.Id == id) > 0;
        }
    }
}