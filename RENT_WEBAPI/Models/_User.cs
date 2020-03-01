using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_RENT_A_CAR.Models
{
    public class _User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool IsNew { get; set; }
    }
}