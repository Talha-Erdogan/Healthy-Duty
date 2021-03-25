using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.NeedForBlood
{
    public class ListForCurrentUserViewModel : ListBaseViewModel<HealthyDuty.Web.Business.Models.NeedForBlood.NeedForBloodWithDetail, ListForCurrentUserFilterViewModel>
    {
        //select list
        public List<SelectListItem> FilterBloodGroupSelectList { get; set; }
        public List<SelectListItem> FilterIsFoundSelectList { get; set; }
    }
}
