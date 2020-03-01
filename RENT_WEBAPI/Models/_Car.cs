using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_RENT_A_CAR.Models
{
    public class _Car
    {
        public string Number { get; set; }
        public int CarType { get; set; }
        public bool Isvalid { get; set; }
        public bool IsFree { get; set; }
        public int Mileage { get; set; }
        public int Branch { get; set; }
        public byte[] Image { get; set; }
        public bool IsNew { get; set; }
    }
}