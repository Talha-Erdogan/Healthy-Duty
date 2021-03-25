using HealthyDuty.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Interfaces
{
    public interface ISexService
    {
        List<Sex> GetAll();
        Sex GetById(int id);
    }
}
