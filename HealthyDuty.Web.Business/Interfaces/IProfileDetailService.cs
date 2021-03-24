using HealthyDuty.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface IProfileDetailService
    {
        List<Auth> GetAllAuthByCurrentUser(int personnelId);
        List<Auth> GetAllAuthByProfileId(int profileId);
        List<Auth> GetAllAuthByProfileIdWhichIsNotIncluded(int profileId);
        string GetAllAuthCodeByPersonnelIdAsConcatenateString(int personnelId);
        int Add(ProfileDetail record);
        int DeleteByProfileIdAndAuthId(int profileId, int authId);
    }
}
