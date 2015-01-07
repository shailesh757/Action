﻿using System;
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
            ApartmetSale aptSale = new ApartmetSale();
            aptSale.ApartmentId = apartmentId;
            aptSale.UserId = userId;
            aptSale.IsBlocked = true;
            aptSale.BlockStartDate = System.DateTime.Now;
            aptSale.BlockEndDate = System.DateTime.Now.AddDays(7);
            db.ApartmetSales.Add(aptSale);
            db.SaveChanges();
            return "success";
        }
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpGet]
        public string GetApartmetSalesForDelete(int id, int appId)
        {
            ApartmetSale aptSale = new ApartmetSale();
            aptSale = db.ApartmetSales.Where(x => x.ApartmentId == appId).Single();
            if (aptSale.UserId == id)
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
        public List<sp_GetApartmentSalesByUser_Result> GetApartmentSalesByUser(long id)
        {
            var apartments = db.Database.SqlQuery<sp_GetApartmentSalesByUser_Result>("exec sp_GetApartmentSalesByUser {0}", id).ToList<sp_GetApartmentSalesByUser_Result>();
            return apartments;
        }

        // PUT api/ApartmentSale/5
        //public IHttpActionResult PutApartmetSale(int id, int userid)
        //{
        //    var apartmentSale = db.Database.ExecuteSqlCommand("exec sp_GetApartmentSales {0} {1}", id, userid);
        //    return StatusCode(HttpStatusCode.OK);
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