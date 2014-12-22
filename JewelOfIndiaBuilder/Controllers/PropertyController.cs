using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Description;
using JewelOfIndiaBuilder.Models;
using WebGrease.Css.Ast.Selectors;
using EntityState = System.Data.Entity.EntityState;

namespace JewelOfIndiaBuilder.Controllers
{
    public class PropertyController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/Property

        public List<sp_GetProperties_Result> GetProperties()
        {
            var props = db.Database.SqlQuery<sp_GetProperties_Result>("exec sp_GetProperties").ToList<sp_GetProperties_Result>();
            var visual = db.Visuals;
            foreach (var x in props)
            {
                var image = visual.FirstOrDefault(v => v.Type == "P" && v.TypeId == x.Id);
                if (image != null)
                {
                    x.imagePath = image.Name;
                    x.imageDisplayName = image.DisplayName;
                }
            }
            return props;
        }

        public sp_GetProperties_Result GetPropertyDetail(long detailItemId)
        {
            var props = db.Database.SqlQuery<sp_GetProperties_Result>("exec sp_GetProperties").ToList<sp_GetProperties_Result>();
            return props.FirstOrDefault(x => x.Id == detailItemId);
        }

        public List<sp_GetPropertyFeature_Result> GetPropertyFeature(long propertyId)
        {
            var propertyFeature = db.Database.SqlQuery<sp_GetPropertyFeature_Result>("exec sp_GetPropertyFeature {0}", propertyId).ToList<sp_GetPropertyFeature_Result>();

            return propertyFeature;
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