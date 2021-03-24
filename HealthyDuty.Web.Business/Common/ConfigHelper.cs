using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyDuty.Web.Business.Common
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration { get; set; }

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string AppDbConnectionString
        {
            get
            {
                //return _configuration.GetValue<string>("ConnectionStrings:AppDbConnectionString");
                var result = _configuration.GetSection("ConnectionStrings:AppDbConnectionString");
                return result.Value;
            }
        }
    }
}
