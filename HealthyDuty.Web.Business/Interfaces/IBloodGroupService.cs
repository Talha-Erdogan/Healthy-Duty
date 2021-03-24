using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.BloodGroup;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IBloodGroupService
    {
        PaginatedList<BloodGroup> GetAllPaginatedWithDetailBySearchFilter(BloodGroupSearchFilter searchFilter);
        List<BloodGroup> GetAll();
        BloodGroup GetById(int id);
        int Add(BloodGroup record);
        int Update(BloodGroup record);
    }
}
