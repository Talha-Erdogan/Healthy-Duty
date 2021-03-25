using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.NeedForBlood
{
    public class ListForCurrentUserFilterViewModel
    {
        public int? Filter_BloodGroupId { get; set; }
        public int? Filter_IsFound { get; set; }
    }
}
