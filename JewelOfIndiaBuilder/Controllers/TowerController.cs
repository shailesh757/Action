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
    public class TowerController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET api/Tower
        public IQueryable<Tower> GetTowers()
        {
            return db.Towers;
        }

        // GET api/Tower/5
        [ResponseType(typeof(Tower))]
        public IHttpActionResult GetTower(long id)
        {
            Tower tower = db.Towers.Find(id);
            if (tower == null)
            {
                return NotFound();
            }

            return Ok(tower);
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