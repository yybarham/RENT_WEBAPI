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
                    UserName = o.UserName,
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
                if (o.IsNew)
                {
                    db.ORDERS.Add(new ORDERS()
                    {
                        UserName = o.UserName,
                        StartDate = o.StartDate,
                        EndDate = o.EndDate,
                        ActualDate = o.ActualDate,
                        Number = o.Number,
                        Payed = o.Payed
                    });
                    var curr = db.CARS.Where(a => a.Number == o.Number).FirstOrDefault();
                    curr.IsFree = false;
                }
                else {
                    ORDERS order = db.ORDERS.Find(o.OrderId);
                    if (order == null)
                    {
                        return false;
                    }
                    order.UserName = o.UserName;
                    order.StartDate = o.StartDate;
                    order.EndDate = o.EndDate;
                    order.ActualDate = o.ActualDate;
                    order.Number = o.Number;
                    order.Payed = o.Payed;
                }

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

                var carToFree = db.CARS.Where(c => c.Number == curr.Number).FirstOrDefault();
                if (carToFree != null)
                    carToFree.IsFree = true;

                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        [Route("api/DeleteOrder/{id}")]
        public bool DeleteOrder(int id)
        {
            ORDERS order = db.ORDERS.Find(id);
            if (order == null)
            {
                return false;
            }
            db.ORDERS.Remove(order);
            db.SaveChanges();

            return true;
        }

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