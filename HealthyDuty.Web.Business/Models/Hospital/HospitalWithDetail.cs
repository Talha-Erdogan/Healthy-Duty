using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Models.Hospital
{
    public class HospitalWithDetail :Data.Entity.Hospital
    {
        //detail column
        public string City_Name { get; set; }
    }
}
