using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.NeedForBlood;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface INeedForBloodService
    {
        PaginatedList<NeedForBloodWithDetail> GetAllPaginatedWithDetailBySearchFilter(NeedForBloodSearchFilter searchFilter);
        List<NeedForBlood> GetAll();
        List<NeedForBloodWithDetail> GetAllWithDetailByHospitalIdAndBloodGroupId(int hospitalId, int bloodGroupId);
        NeedForBlood GetById(int id); 
        int Add(NeedForBlood record);
        int Update(NeedForBlood record);

    }
}
