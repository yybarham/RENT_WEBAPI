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
using API_RENT_A_CAR.DB;
using API_RENT_A_CAR.Models;

namespace API_RENT_A_CAR.Controllers
{
    public class UsersController : ApiController
    {
        private rent_a_car_Entities db = new rent_a_car_Entities();


        [HttpGet]
        [Route("api/GetUsers")]
        public List<_User> GetUsers()
        {
            var list = db.USERS.Select(u => new _User()            {
                Id = u.Id,
                FullName = u.FullName,
                UserName = u.UserName,
                Email = u.Email,
                Gender = u.Gender,
                Password = u.Password,
                Role = u.Role

            }).ToList();
            return list;
        }
       
        [HttpPost]
        [Route("api/SaveUser")]
        public bool SaveUser(_User u)
        {
            try
            {
                if (u.IsNew)
                {
                    db.USERS.Add(new USERS()
                    {
                        Id = u.Id,
                        FullName = u.FullName,
                        Email = u.Email,
                        Gender = u.Gender,
                        Password = u.Password,
                        Role = u.Role,
                        UserName = u.UserName
                    });
                }
                else
                {
                    var curr = db.USERS.Where(a => a.Id == u.Id).FirstOrDefault();                    
                    curr.FullName = u.FullName;
                    curr.Email = u.Email;
                    curr.Gender = u.Gender;
                    curr.Password = u.Password;
                    curr.Role = u.Role;
                    curr.UserName = u.UserName;
                }

                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }


        [HttpGet]
        [Route("api/DeleteUser/{id}")]
        public bool DeleteUser(int id)
        {
            USERS user = db.USERS.Find(id);
            if (user == null)
            {
                return false;
            }
            db.USERS.Remove(user);
            db.SaveChanges();

            return true;
        }

        [HttpPost]
        [Route("api/Login")]
        public int Login(_User u)
        {
            try
            {
                var curr = db.USERS.Where(a => a.UserName == u.UserName.ToLower()).FirstOrDefault();
                if (curr.Password == u.Password)
                {
                    return curr.Role;
                }
            }
            catch
            {
            }
            return 0;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool USERSExists(int id)
        {
            return db.USERS.Count(e => e.Id == id) > 0;
        }
    }
}