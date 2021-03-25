using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.User
{
    public class ListFilterViewModel
    {
        // filters        
        public string Filter_Name { get; set; }
        public string Filter_LastName { get; set; }
        public int? Filter_BloodGroupId { get; set; }
        public int? Filter_CityCode { get; set; }
    }
}
