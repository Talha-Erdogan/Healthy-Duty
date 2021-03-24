using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.Hospital
{
    public class ListFilterViewModel
    {
        // filters        
        public string Filter_Name { get; set; }
        public int? Filter_CityCode { get; set; }
    }
}
