using HealthyDuty.Data;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Hospital;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HealthyDuty.Web.Business
{
    public class HospitalService : IHospitalService
    {
        private IConfiguration _config;

        public HospitalService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<HospitalWithDetail> GetAllPaginatedWithDetailBySearchFilter(HospitalSearchFilter searchFilter)
        {
            PaginatedList<HospitalWithDetail> resultList = new PaginatedList<HospitalWithDetail>(new List<HospitalWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from h in dbContext.Hospital
                            from c in dbContext.City.Where(x => x.Code == h.CityCode).DefaultIfEmpty()
                            where h.IsDeleted == false
                            select new HospitalWithDetail()
                            {
                                Id = h.Id,
                                Name = h.Name,
                                CityCode =h.CityCode,
                                Address=h.Address,
                                DeletedBy=h.DeletedBy,
                                IsDeleted =h.IsDeleted,
                                Phone =h.Phone,
                                City_Name = c == null ? String.Empty : c.Name
                            };

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
                }
                if (searchFilter.Filter_CityCode.HasValue)
                {
                    query = query.Where(r => r.CityCode == searchFilter.Filter_CityCode.Value);
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


                resultList = new PaginatedList<HospitalWithDetail>(
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

        public List<Hospital> GetAll()
        {
            List<Hospital> resultList = new List<Hospital>();

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = dbContext.Hospital.Where(x => x.IsDeleted == false).AsNoTracking();
                resultList.AddRange(query.ToList());
            }
            return resultList;
        }

        public Hospital GetById(int id)
        {
            Hospital result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Hospital.Where(a => a.Id == id && a.IsDeleted == false).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(Hospital record)
        {
            int result = 0;
            record.IsDeleted = false;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(Hospital record)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }

            return result;
        }
        public int Delete(int id, int deletedBy)
        {
            int result = 0;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var hospital = dbContext.Hospital.Where(x => x.Id == id).FirstOrDefault();
                hospital.IsDeleted = true;
                hospital.DeletedBy = deletedBy;
                hospital.DeletedDateTime = DateTime.Now;
                dbContext.Entry(hospital).State = EntityState.Modified;
                result = dbContext.SaveChanges();
            }
            return result;
        }

    }
}
