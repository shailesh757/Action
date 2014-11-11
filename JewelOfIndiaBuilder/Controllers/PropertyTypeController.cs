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
    public class PropertyTypeController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/PropertyType
        public IQueryable<PropertyType> GetPropertyTypes()
        {
            return db.PropertyTypes;
        }

        // GET api/PropertyType/5
        [ResponseType(typeof(PropertyType))]
        public IHttpActionResult GetPropertyType(int id)
        {
            PropertyType propertytype = db.PropertyTypes.Find(id);
            if (propertytype == null)
            {
                return NotFound();
            }

            return Ok(propertytype);
        }

        // PUT api/PropertyType/5
        public IHttpActionResult PutPropertyType(int id, PropertyType propertytype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != propertytype.Id)
            {
                return BadRequest();
            }

            db.Entry(propertytype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyTypeExists(id))
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

        // POST api/PropertyType
        [ResponseType(typeof(PropertyType))]
        public IHttpActionResult PostPropertyType(PropertyType propertytype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PropertyTypes.Add(propertytype);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PropertyTypeExists(propertytype.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = propertytype.Id }, propertytype);
        }

        // DELETE api/PropertyType/5
        [ResponseType(typeof(PropertyType))]
        public IHttpActionResult DeletePropertyType(int id)
        {
            PropertyType propertytype = db.PropertyTypes.Find(id);
            if (propertytype == null)
            {
                return NotFound();
            }

            db.PropertyTypes.Remove(propertytype);
            db.SaveChanges();

            return Ok(propertytype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PropertyTypeExists(int id)
        {
            return db.PropertyTypes.Count(e => e.Id == id) > 0;
        }
    }
}