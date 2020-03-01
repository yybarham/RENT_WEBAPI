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
    public class OrdersController : ApiController
    {
        private rent_a_car_Entities db = new rent_a_car_Entities();

        [HttpGet]
        [Route("api/GetOrders")]
        public List<_Order> GetOrders()
        {
            try
            {
                var list = db.ORDERS.Select(o => new _Order()
                {
                    Id = o.Id,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    ActualDate = o.ActualDate.Value,
                    OrderId = o.OrderId,
                    Number = o.Number,
                    Payed = o.Payed.Value
                }).ToList();
                return list;
            }
            catch 
            {
                return new List<_Order>();
            }
        }

        [HttpPost]
        [Route("api/SaveOrder")]
        public bool SaveOrder(_Order o)
        {
            try
            {
                db.ORDERS.Add(new ORDERS()
                {
                    Id = o.Id,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    ActualDate = o.ActualDate,
                    Number = o.Number,
                    Payed = o.Payed
                });

                var curr = db.CARS.Where(a => a.Number == o.Number).FirstOrDefault();
                curr.IsFree = false;

                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        [Route("api/ReturnCar")]
        public bool ReturnCar(_Order o)
        {
            try
            {
                var curr = db.ORDERS.Where(a => a.OrderId == o.OrderId).FirstOrDefault();
                curr.ActualDate = o.ActualDate;
                curr.Payed = o.Payed;
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //// GET: api/Orders/5
        //[ResponseType(typeof(ORDERS))]
        //public IHttpActionResult GetORDERS(int id)
        //{
        //    ORDERS oRDERS = db.ORDERS.Find(id);
        //    if (oRDERS == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(oRDERS);
        //}

        //// PUT: api/Orders/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutORDERS(int id, ORDERS oRDERS)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != oRDERS.OrderId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(oRDERS).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ORDERSExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}



        //// DELETE: api/Orders/5
        //[ResponseType(typeof(ORDERS))]
        //public IHttpActionResult DeleteORDERS(int id)
        //{
        //    ORDERS oRDERS = db.ORDERS.Find(id);
        //    if (oRDERS == null)
        //    {
        //        return NotFound();
        //    }

        //    db.ORDERS.Remove(oRDERS);
        //    db.SaveChanges();

        //    return Ok(oRDERS);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ORDERSExists(int id)
        {
            return db.ORDERS.Count(e => e.OrderId == id) > 0;
        }
    }
}