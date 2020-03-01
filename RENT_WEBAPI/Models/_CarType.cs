using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_RENT_A_CAR.Models
{
    public class _CarType
    {

        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public double DailyCost { get; set; }
        public double DailyPenalty { get; set; }
        public int Year { get; set; }
        public int GearType { get; set; }
        public bool IsNew { get; set; }

    }
}