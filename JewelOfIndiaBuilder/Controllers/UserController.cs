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
    public class UserController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        public IEnumerable<sp_GetAllUser_Result> GetAllUser()
        {
            var results = db.Database.SqlQuery<sp_GetAllUser_Result>("exec sp_GetAllUser").ToList<sp_GetAllUser_Result>();

            return results;
        }

        public User GetUser(string userName, string password)
        {
            string salt = db.Users.Where(x => x.UserName == userName)
                                         .Select(x => x.Salt)
                                         .Single();


            string pwd = db.Users.Where(x => x.UserName == userName)
                                         .Select(x => x.Password)
                                         .Single();

            bool passwordMatches = System.Web.Helpers.Crypto.VerifyHashedPassword(pwd, password + salt);

            if (passwordMatches)
            {
                return db.Users.Where(x => x.UserName == userName).Single();
            }

                       

            return new User();
        }

        // GET api/User
        //public IQueryable<User> GetUsers()
        //{
       //     return db.Users;
       // }

        /* // GET api/User/5
         [ResponseType(typeof(User))]
         public IHttpActionResult GetUser(long id)
         {
             User user = db.Users.Find(id);
             if (user == null)
             {
                 return NotFound();
             }

             return Ok(user);
         }*/

        // PUT api/User/5
        public IHttpActionResult PutUser(long id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST api/User
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(long id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(long id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}
