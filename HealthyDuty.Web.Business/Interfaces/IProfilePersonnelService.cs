using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.ProfilePersonnel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IProfilePersonnelService
    {
        PaginatedList<Personnel> GetAllPersonnelPaginatedWithDetailBySearchFilter(ProfilePersonnelSearchFilter searchFilter);
        PaginatedList<Personnel> GetAllPersonnelWhichIsNotIncludedPaginatedWithDetailBySearchFilter(ProfilePersonnelSearchFilter searchFilter);
        List<Profile> GetAllProfileByCurrentUser(int personnelId);
        List<Profile> GetAllProfileByPersonnelIdAndAuthCode(int personnelId, string authCode);
        int Add(ProfilePersonnel record);
        int DeleteByProfileIdAndPersonnelId(int profileId, int personnelId);
    }
}
