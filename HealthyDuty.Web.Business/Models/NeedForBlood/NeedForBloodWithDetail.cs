using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Models.NeedForBlood
{
    public class NeedForBloodWithDetail :Data.Entity.NeedForBlood
    {
        //detail columns
        public string BloodGroup_Name { get; set; }
        public string Hospital_Name { get; set; }
        
    }
}
