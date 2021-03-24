using HealthyDuty.Data;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.BloodGroup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace HealthyDuty.Web.Business
{
    public class BloodGroupService : IBloodGroupService
    {
        private IConfiguration _config;

        public BloodGroupService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<BloodGroup> GetAllPaginatedWithDetailBySearchFilter(BloodGroupSearchFilter searchFilter)
        {
            PaginatedList<BloodGroup> resultList = new PaginatedList<BloodGroup>(new List<BloodGroup>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from a in dbContext.BloodGroup
                            select a;

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
                }

                // asnotracking
                query = query.AsNoTracking();

                //total count
                var totalCount = query.Count();

                //sorting
                if (!string.IsNullOrEmpty(searchFilter.SortOn))
                {
                    // using System.Linq.Dynamic.Core; nuget paketi ve namespace eklenmelidir, dynamic order by yapmak icindir
                    query = query.OrderBy(searchFilter.SortOn + " " + searchFilter.SortDirection.ToUpper());
                }
                else
                {
                    // deefault sıralama vermek gerekiyor yoksa skip metodu hata veriyor ef 6'da -- 28.10.2019 15:40
                    // https://stackoverflow.com/questions/3437178/the-method-skip-is-only-supported-for-sorted-input-in-linq-to-entities
                    query = query.OrderBy(r => r.Id);
                }

                //paging
                query = query.Skip((searchFilter.CurrentPage - 1) * searchFilter.PageSize).Take(searchFilter.PageSize);


                resultList = new PaginatedList<BloodGroup>(
                    query.ToList(),
                    totalCount,
                    searchFilter.CurrentPage,
                    searchFilter.PageSize,
                    searchFilter.SortOn,
                    searchFilter.SortDirection
                    );
            }

            return resultList;
        }

        public List<BloodGroup> GetAll()
        {
            List<BloodGroup> resultList = new List<BloodGroup>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.BloodGroup.AsNoTracking().ToList());
            }
            return resultList;
        }

        public BloodGroup GetById(int id)
        {
            BloodGroup result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.BloodGroup.Where(a => a.Id == id).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(BloodGroup record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(BloodGroup record)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }
    }
}
