using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Models.Personnel
{
    public class PersonnelWithDetail : HealthyDuty.Data.Entity.Personnel
    {
        //detail column
        public string Hospital_Name { get; set; }
    }
}
