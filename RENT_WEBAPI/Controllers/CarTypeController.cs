using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using API_RENT_A_CAR.DB;
using API_RENT_A_CAR.Models;

namespace API_RENT_A_CAR.Controllers
{
    public class CarTypeController : ApiController
    {
        private rent_a_car_Entities db = new rent_a_car_Entities();

        [HttpGet]
        [Route("api/GetCarTypes")]
        public List<_CarType> GetCarTypes()
        {
            try
            {
                var list = db.CAR_TYPE.Select(c => new _CarType()
                {
                    Id = c.Id,
                    Manufacturer = c.Manufacturer,
                    Model = c.Model,
                    DailyCost = c.DailyCost,
                    DailyPenalty = c.DailyPenalty,
                    Year = c.Year,
                    GearType = c.GearType
                }).ToList();
                return list;
            }
            catch 
            {
                return new List<_CarType>();
            }
        }


        [HttpGet]
        [Route("api/DeleteCarType/{id}")]
        public bool DeleteCarType(int id)
        {
            CAR_TYPE cartype = db.CAR_TYPE.Find(id);
            if (cartype == null)
            {
                return false;
            }
            db.CAR_TYPE.Remove(cartype);
            db.SaveChanges();

            return true;
        }

        [HttpPost]
        [Route("api/SaveCarType")]
        public bool SaveCarType(_CarType c)
        {
            try
            {
                if (c.IsNew)
                {
                    db.CAR_TYPE.Add(new CAR_TYPE()
                    {
                        Id = c.Id,
                        DailyCost = c.DailyCost,
                        DailyPenalty = c.DailyPenalty,
                        GearType = c.GearType,
                        Manufacturer = c.Manufacturer,
                        Model = c.Model,
                        Year = c.Year
                    });
                }
                else
                {
                    var curr = db.CAR_TYPE.Where(w => w.Id == c.Id).FirstOrDefault();
                    curr.Id = c.Id;
                    curr.DailyCost = c.DailyCost;
                    curr.DailyPenalty = c.DailyPenalty;
                    curr.GearType = c.GearType;
                    curr.Manufacturer = c.Manufacturer;
                    curr.Model = c.Model;
                    curr.Year = c.Year;
                }

                db.SaveChanges();
            }
            catch
            {
                return false;
            }
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

        private bool CAR_TYPEExists(int id)
        {
            return db.CAR_TYPE.Count(e => e.Id == id) > 0;
        }
    }
}