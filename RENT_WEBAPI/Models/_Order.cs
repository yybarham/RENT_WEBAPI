using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_RENT_A_CAR.Models
{
    public class _Order
    {
        public int OrderId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public System.DateTime ? ActualDate { get; set; }
        public int Id { get; set; }
        public string Number { get; set; }
        public double?  Payed { get; set; }

    }
}