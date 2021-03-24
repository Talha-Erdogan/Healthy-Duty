using HealthyDuty.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Common
{
    // Asp.net Session StateServer kullanimi icin Serializable attribute eklenir
    [Serializable]
    public class SessionUser
    {
        public int Id { get; set; }
        public string TC { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public int HospitalId { get; set; }
        public List<Auth> AuthList { get; set; }
        public List<Profile> ProfileList { get; set; }
    }
}
