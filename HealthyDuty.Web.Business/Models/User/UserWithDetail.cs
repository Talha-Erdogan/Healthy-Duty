using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Models.User
{
    public class UserWithDetail :Data.Entity.User
    {
        //detail columns
        public string BloodGroup_Name { get; set; }
        public string  City_Name { get; set; }
    }
}
