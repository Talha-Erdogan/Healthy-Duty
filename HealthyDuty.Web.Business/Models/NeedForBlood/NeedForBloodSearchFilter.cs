using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Models.NeedForBlood
{
    public class NeedForBloodSearchFilter
    {
        // paging
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        // sorting
        public string SortOn { get; set; }
        public string SortDirection { get; set; }

        // filters        
        public int? Filter_BloodGroupId { get; set; }
        public int? Filter_HospitalId { get; set; }
        public string Filter_HospitalName { get; set; }
        public int? Filter_IsFound { get; set; }

    }
}
