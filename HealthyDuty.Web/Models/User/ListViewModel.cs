using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthyDuty.Web.Models.User
{
    public class ListViewModel : ListBaseViewModel<HealthyDuty.Web.Business.Models.User.UserWithDetail, ListFilterViewModel>
    {
        public List<SelectListItem> FilterBloodGroupSelectList { get; set; }
        public List<SelectListItem> FilterCitySelectList { get; set; }
        public List<SelectListItem> FilterSexSelectList { get; set; }
    }
}
