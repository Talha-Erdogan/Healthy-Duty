using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IProfileService
    {
        PaginatedList<Profile> GetAllPaginatedWithDetailBySearchFilter(ProfileSearchFilter searchFilter);
        List<Profile> GetAll();
        Profile GetById(int id);
        int Add(Profile record);
        int Update(Profile record);
        int Delete(int id, int deletedBy);
    }
}
