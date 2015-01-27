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
        //public IQueryable<ApartmetSale> GetApartmetSales()
        //{
        //    return db.ApartmetSales;
        //}


        //public List<sp_GetApartmentSales_Result> GetApartmetSale(long id)
        //{
        //    var apartmentSale = db.Database.SqlQuery<sp_GetApartmentSales_Result>("exec sp_GetApartmentSales {0}", id).ToList<sp_GetApartmentSales_Result>();

        //    return apartmentSale;
        //}

        public string GetApartmetSales(int userId, int apartmentId)
        {
            bool? isOwner = false;

            var firstOrDefault = db.Users.FirstOrDefault(x => x.Id == userId);
            var salesType = db.ApartmentSalesTypes;
            Int16 salesId = 0;
            if (firstOrDefault.IsOwner != null)
            {
                isOwner =  firstOrDefault.IsOwner;
            }
            if (isOwner==true)
            {
                var apartmentSalesType = salesType.FirstOrDefault(x => x.SalesType.ToUpper() == "HOLD");
                if (apartmentSalesType != null)
                    salesId = apartmentSalesType.Id;
            }
            else
            {
                var apartmentSalesType = salesType.FirstOrDefault(x => x.SalesType.ToUpper() == "REQUEST FOR HOLD");
                if (apartmentSalesType != null)
                    salesId = apartmentSalesType.Id;
            }
            var aptSale = new ApartmetSale
            {
                ApartmentId = apartmentId,
                UserId = userId,
                IsBlocked = (bool) isOwner,
                BlockStartDate = System.DateTime.Now,
                BlockEndDate = System.DateTime.Now.AddDays(10),
                SalesType = salesId
                
            };
            db.ApartmetSales.Add(aptSale);
            db.SaveChanges();
            return "success";
        }
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpGet]

        public string GetApartmetSalesForDelete(int id, int appId)
        {
            ApartmetSale aptSale = new ApartmetSale();
            aptSale = db.ApartmetSales.FirstOrDefault(x => x.ApartmentId == appId);
            var appUser = db.Users.FirstOrDefault(user => user.Id == id);
            if (appUser != null && (aptSale != null && (aptSale.UserId == id || appUser.IsOwner == true)))
            {
                db.ApartmetSales.Remove(aptSale);
                db.SaveChanges();
                return "success";
            }
            else
            {
                return "error";
            }
        }
        //public string GetApartmetSalesForDelete(int id, int appId)
        //{
        //    ApartmetSale aptSale = new ApartmetSale();
        //    aptSale = db.ApartmetSales.Where(x => x.ApartmentId == appId).Single();
        //    if (aptSale.UserId == id)
        //    {
        //        db.ApartmetSales.Remove(aptSale);
        //        db.SaveChanges();
        //        return "success";
        //    }
        //    else
        //    {
        //        return "error";
        //    }
        //}
        public List<sp_GetApartmentSalesByUser_Result> GetApartmentSalesByUser(long id)
        {
            var apartments = db.Database.SqlQuery<sp_GetApartmentSalesByUser_Result>("exec sp_GetApartmentSalesByUser {0}", id).ToList<sp_GetApartmentSalesByUser_Result>();
            return apartments;
        }

        //public List<sp_GetApartmentSalesByUser_Result> GetApartmentSalesByUser(long id)
        //{
        //    var apartments = db.Database.SqlQuery<sp_GetApartmentSalesByUser_Result>("exec sp_GetApartmentSalesByUser {0}", id).ToList<sp_GetApartmentSalesByUser_Result>();
        //    return apartments;
        //}
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