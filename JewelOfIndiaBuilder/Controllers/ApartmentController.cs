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
    public class ApartmentController : ApiController
    {
        private readonly JewelOfIndiaEntities _db = new JewelOfIndiaEntities();

        // GET api/Apartment
        public IQueryable<Apartment> GetApartments()
        {
            return _db.Apartments;
        }

        
        public List<sp_GetApartments_Result> GetApartment(long id)
        {
            var apartments = _db.Database.SqlQuery<sp_GetApartments_Result>("exec sp_GetApartments {0}", id).ToList<sp_GetApartments_Result>();

            return apartments;
        }

        public List<sp_GetApartmentFeature_Result> GetApartmentFeature(long propertyId)
        {
            var apartmentFeature = _db.Database.SqlQuery<sp_GetApartmentFeature_Result>("exec sp_GetApartmentFeature {0}", propertyId).ToList<sp_GetApartmentFeature_Result>();

            return apartmentFeature;
        }

        // PUT api/Apartment/5
        public IHttpActionResult PutApartment(long id, Apartment apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != apartment.Id)
            {
                return BadRequest();
            }

            _db.Entry(apartment).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
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

        // POST api/Apartment
        [ResponseType(typeof(Apartment))]
        public IHttpActionResult PostApartment(Apartment apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Apartments.Add(apartment);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = apartment.Id }, apartment);
        }

        // DELETE api/Apartment/5
        [ResponseType(typeof(Apartment))]
        public IHttpActionResult DeleteApartment(long id)
        {
            Apartment apartment = _db.Apartments.Find(id);
            if (apartment == null)
            {
                return NotFound();
            }

            _db.Apartments.Remove(apartment);
            _db.SaveChanges();

            return Ok(apartment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApartmentExists(long id)
        {
            return _db.Apartments.Count(e => e.Id == id) > 0;
        }
    }
}