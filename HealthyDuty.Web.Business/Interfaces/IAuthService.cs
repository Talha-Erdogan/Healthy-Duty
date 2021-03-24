using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IAuthService
    {
        PaginatedList<Auth> GetAllPaginatedWithDetailBySearchFilter(AuthSearchFilter searchFilter);
        List<Auth> GetAll();
        Auth GetById(int id);
        int Add(Auth record);
        int Update(Auth record);
        int Delete(int id, int deletedBy);
    }
}
