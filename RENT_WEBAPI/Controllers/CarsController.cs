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
    public class CarsController : ApiController
    {
        private rent_a_car_Entities db = new rent_a_car_Entities();

        [HttpGet]
        [Route("api/GetCars")]
        public List<_Car> GetCars()
        {
            try
            {
                var list = db.CARS.Select(c => new _Car()
                {
                    Branch = c.Branch,
                    CarType = c.CarType,
                    Image = c.Image,
                    IsFree = c.IsFree,
                    Isvalid = c.Isvalid,
                    Mileage = c.Mileage,
                    Number = c.Number
                }).ToList();
                return list;
            }
            catch 
            {
                return new List<_Car>();
            }
        }

 

        [HttpPost]
        [Route("api/SaveCar")]
        public bool SaveCar(_Car c)
        {
            try
            {
                if (c.IsNew)
                {
                    db.CARS.Add(new CARS()
                    {
                        Number = c.Number,
                        Branch = c.Branch,
                        CarType = c.CarType,
                        Mileage = c.Mileage,
                        IsFree = c.IsFree,
                        Isvalid = c.Isvalid,
                        Image = c.Image
                    });
                }
                else
                {
                    var curr = db.CARS.Where(w => w.Number == c.Number).FirstOrDefault();
                    curr.Branch = c.Branch;
                    curr.CarType = c.CarType;
                    curr.Mileage = c.Mileage;
                    curr.IsFree = c.IsFree;
                    curr.Isvalid = c.Isvalid;
                    curr.Image = c.Image;
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
        [Route("api/DeleteCar/{id}")]
        public bool DeleteCar(int id)
        {
            CARS car = db.CARS.Find(id);
            if (car == null)
            {
                return false;
            }
            db.CARS.Remove(car);
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

        private bool CARSExists(string id)
        {
            return db.CARS.Count(e => e.Number == id) > 0;
        }
    }
}