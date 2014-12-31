using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JewelOfIndiaBuilder.Models;

namespace JewelOfIndiaBuilder.Controllers
{
    public class UserAdminController : Controller
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET: /UserAdmin/
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ViewResult Login()
        {
            return View();
        }

        public ViewResult ChangePassword()
        {
            return View();
        }

        public ActionResult Logoff()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("../Home");
        }

        [HttpPost]
        public ActionResult ChangePassword(User user)
        {
            bool validEmail = db.Users.Any(x => x.UserName == user.UserName);

            if (!validEmail)
            {
                return RedirectToAction("ChangePassword");
            }

            //var salt = GetSaltForUserFromDatabase(username);
            //var hashedPassword = GetHashedPasswordForUserFromDatabase(username);
            //var saltedPassword = password + salt;

            string salt = db.Users.Where(x => x.UserName == user.UserName)
                                         .Select(x => x.Salt)
                                         .Single();


            string password = db.Users.Where(x => x.UserName == user.UserName)
                                         .Select(x => x.Password)
                                         .Single();

            bool? isAdmin = db.Users.Where(x => x.UserName == user.UserName)
                                         .Select(x => x.IsOwner).Single();


            bool passwordMatches = System.Web.Helpers.Crypto.VerifyHashedPassword(password, user.Password + salt);

            if (!passwordMatches)
            {
                return RedirectToAction("ChangePassword");
            }

           

            var userToUpdate= db.Users.FirstOrDefault(x => x.UserName == user.UserName);

            var newSalt = System.Web.Helpers.Crypto.GenerateSalt();
            var saltedPassword = user.newPassword + newSalt;
            var hashedPassword = System.Web.Helpers.Crypto.HashPassword(saltedPassword);
            userToUpdate.Password = hashedPassword;
            userToUpdate.Salt = newSalt;

            db.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("../Home");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            bool validEmail = db.Users.Any(x => x.UserName == user.UserName);

            if (!validEmail)
            {
                return RedirectToAction("Login");
            }

            //var salt = GetSaltForUserFromDatabase(username);
            //var hashedPassword = GetHashedPasswordForUserFromDatabase(username);
            //var saltedPassword = password + salt;

            string salt = db.Users.Where(x => x.UserName == user.UserName)
                                         .Select(x => x.Salt)
                                         .Single();


            string password = db.Users.Where(x => x.UserName == user.UserName)
                                         .Select(x => x.Password)
                                         .Single();

            bool? isAdmin = db.Users.Where(x => x.UserName == user.UserName)
                                         .Select(x => x.IsOwner).Single();


            bool passwordMatches = System.Web.Helpers.Crypto.VerifyHashedPassword(password, user.Password + salt);

            if (!passwordMatches)
            {
                return RedirectToAction("Login");
            }

            string authId = Guid.NewGuid().ToString();

            Session["AuthID"] = authId;
            Session["IsAdmin"] = isAdmin;

            return RedirectToAction("../Home");
        }

        // GET: /UserAdmin/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /UserAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UserAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,UserName,Password,Salt,Question,Answer,EmailId,IsOwner,MobileNo,DOB")] User user)
        {
            if (ModelState.IsValid)
            {
                var salt = System.Web.Helpers.Crypto.GenerateSalt();
                var saltedPassword = user.Password + salt;
                var hashedPassword = System.Web.Helpers.Crypto.HashPassword(saltedPassword);
                user.Password = hashedPassword;
                user.Salt = salt;

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /UserAdmin/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /UserAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,UserName,Password,Salt,Question,Answer,EmailId,IsOwner,MobileNo,DOB")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: /UserAdmin/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /UserAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
