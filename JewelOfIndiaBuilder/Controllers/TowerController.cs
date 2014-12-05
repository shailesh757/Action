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
    public class TowerController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/Tower
        //public List<sp_GetTower_Result> GetTowers()
        //{
        //    var towers = db.Database.SqlQuery<sp_GetTower_Result>("exec sp_GetTower @PropertyId").ToList<sp_GetTower_Result>();

        //    return towers;
        //}

        // GET api/Tower/5
        
        public List<sp_GetTower_Result> GetTower(long id)
        {
            var towers = db.Database.SqlQuery<sp_GetTower_Result>("exec dbo.sp_GetTower {0}", id).ToList<sp_GetTower_Result>();

            return towers;
        }
        public List<sp_GetTowerFeature_Result> GetTowerFeature(long propertyId)
        {
            var towerFeature = db.Database.SqlQuery<sp_GetTowerFeature_Result>("exec sp_GetTowerFeature {0}", propertyId).ToList<sp_GetTowerFeature_Result>();

            return towerFeature;
        }
       

        // PUT api/Tower/5
        public IHttpActionResult PutTower(long id, Tower tower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tower.Id)
            {
                return BadRequest();
            }

            db.Entry(tower).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TowerExists(id))
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

        // POST api/Tower
        [ResponseType(typeof(Tower))]
        public IHttpActionResult PostTower(Tower tower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Towers.Add(tower);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tower.Id }, tower);
        }

        // DELETE api/Tower/5
        [ResponseType(typeof(Tower))]
        public IHttpActionResult DeleteTower(long id)
        {
            Tower tower = db.Towers.Find(id);
            if (tower == null)
            {
                return NotFound();
            }

            db.Towers.Remove(tower);
            db.SaveChanges();

            return Ok(tower);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TowerExists(long id)
        {
            return db.Towers.Count(e => e.Id == id) > 0;
        }
    }
}