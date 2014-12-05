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
    public class ApartmentSaleController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/ApartmentSale
        public IQueryable<ApartmetSale> GetApartmetSales()
        {
            return db.ApartmetSales;
        }


        public List<sp_GetApartmentSales_Result> GetApartmetSale(long id)
        {
            var apartmentSale = db.Database.SqlQuery<sp_GetApartmentSales_Result>("exec sp_GetApartmentSales {0}", id).ToList<sp_GetApartmentSales_Result>();

            return apartmentSale;
        }

       
        // PUT api/ApartmentSale/5
        public IHttpActionResult PutApartmetSale(int id, ApartmetSale apartmetsale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != apartmetsale.Id)
            {
                return BadRequest();
            }

            db.Entry(apartmetsale).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmetSaleExists(id))
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

        // POST api/ApartmentSale
        [ResponseType(typeof(ApartmetSale))]
        public IHttpActionResult PostApartmetSale(ApartmetSale apartmetsale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApartmetSales.Add(apartmetsale);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = apartmetsale.Id }, apartmetsale);
        }

        // DELETE api/ApartmentSale/5
        [ResponseType(typeof(ApartmetSale))]
        public IHttpActionResult DeleteApartmetSale(int id)
        {
            ApartmetSale apartmetsale = db.ApartmetSales.Find(id);
            if (apartmetsale == null)
            {
                return NotFound();
            }

            db.ApartmetSales.Remove(apartmetsale);
            db.SaveChanges();

            return Ok(apartmetsale);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApartmetSaleExists(int id)
        {
            return db.ApartmetSales.Count(e => e.Id == id) > 0;
        }
    }
}