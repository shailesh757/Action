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
    public class MessageNotificationsController : ApiController
    {
        private JewelOfIndiaEntities db = new JewelOfIndiaEntities();

        // GET: api/MessageNotifications
        public IEnumerable<sp_GetMessageBasedOnUser_Result> GetMessageBasedOnUser(int id)
        {
            var results = db.Database.SqlQuery<sp_GetMessageBasedOnUser_Result>("exec sp_GetMessageBasedOnUser {0}",id).ToList<sp_GetMessageBasedOnUser_Result>();

            return results;
        }

        public string GetMessageForSave(string userids, string messageText)
        {
            //var results = db.Database.SqlQuery<sp_GetMessageBasedOnUser_Result>("exec sp_GetMessageBasedOnUser {0}", id).ToList<sp_GetMessageBasedOnUser_Result>();
            var users = userids.Split('~');
            foreach (var user in users)
            {
                var messageNotification = new MessageNotification
                {
                    MessageText = messageText,
                    UserId = Convert.ToInt64(user)
                };
                db.MessageNotifications.Add(messageNotification);
                

            }
            db.SaveChanges();
            return "success";
        }

        // GET: api/MessageNotifications/5
 

       
        // POST: api/MessageNotifications
        [ResponseType(typeof(MessageNotification))]
        public IHttpActionResult PostMessageNotification(MessageNotification messageNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MessageNotifications.Add(messageNotification);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = messageNotification.Id }, messageNotification);
        }

        // DELETE: api/MessageNotifications/5
        [ResponseType(typeof(MessageNotification))]
        public IHttpActionResult DeleteMessageNotification(int id)
        {
            MessageNotification messageNotification = db.MessageNotifications.Find(id);
            if (messageNotification == null)
            {
                return NotFound();
            }

            db.MessageNotifications.Remove(messageNotification);
            db.SaveChanges();

            return Ok(messageNotification);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageNotificationExists(int id)
        {
            return db.MessageNotifications.Count(e => e.Id == id) > 0;
        }
    }
}