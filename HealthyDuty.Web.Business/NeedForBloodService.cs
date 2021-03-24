using HealthyDuty.Data;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.NeedForBlood;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace HealthyDuty.Web.Business
{
    public class NeedForBloodService : INeedForBloodService
    {
        private IConfiguration _config;

        public NeedForBloodService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<NeedForBloodWithDetail> GetAllPaginatedWithDetailBySearchFilter(NeedForBloodSearchFilter searchFilter)
        {
            PaginatedList<NeedForBloodWithDetail> resultList = new PaginatedList<NeedForBloodWithDetail>(new List<NeedForBloodWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from nfb in dbContext.NeedForBlood
                            from h in dbContext.Hospital.Where(x => x.Id == nfb.HospitalId).DefaultIfEmpty()
                            from bg in dbContext.BloodGroup.Where(x => x.Id == nfb.BloodGroupId).DefaultIfEmpty()
                            select new NeedForBloodWithDetail()
                            {
                                Id = nfb.Id,
                                BloodGroupId = nfb.BloodGroupId,
                                HospitalId = nfb.HospitalId,
                                DonorName = nfb.DonorName,
                                DonorLastName = nfb.DonorLastName,
                                DonorPhone = nfb.DonorPhone,
                                FoundDateTime = nfb.FoundDateTime,
                                IsFound = nfb.IsFound,

                                BloodGroup_Name = bg == null ? string.Empty : bg.Name,
                                Hospital_Name = h == null ? string.Empty : h.Name
                            };

                // filtering
                if (searchFilter.Filter_IsFound.HasValue)
                {
                    bool _isFound = searchFilter.Filter_IsFound.Value == 1 ? true : false;
                    query = query.Where(r => r.IsFound == _isFound);
                }
                if (searchFilter.Filter_BloodGroupId.HasValue)
                {
                    query = query.Where(r => r.BloodGroupId == searchFilter.Filter_BloodGroupId.Value);
                }
                if (searchFilter.Filter_HospitalId.HasValue)
                {
                    query = query.Where(r => r.HospitalId == searchFilter.Filter_HospitalId.Value);
                }
                if (!string.IsNullOrEmpty(searchFilter.Filter_HospitalName))
                {
                    query = query.Where(r => r.Hospital_Name.Contains(searchFilter.Filter_HospitalName));
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
                resultList = new PaginatedList<NeedForBloodWithDetail>(
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

        public List<NeedForBlood> GetAll()
        {
            List<NeedForBlood> resultList = new List<NeedForBlood>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.NeedForBlood.AsNoTracking().ToList());
            }
            return resultList;
        }

        public List<NeedForBloodWithDetail> GetAllWithDetailByHospitalIdAndBloodGroupId(int hospitalId, int bloodGroupId)
        {
            List<NeedForBloodWithDetail> resultList = new List<NeedForBloodWithDetail>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from nfb in dbContext.NeedForBlood
                            from h in dbContext.Hospital.Where(x => x.Id == nfb.HospitalId).DefaultIfEmpty()
                            from bg in dbContext.BloodGroup.Where(x => x.Id == nfb.BloodGroupId).DefaultIfEmpty()
                            where nfb.HospitalId == hospitalId && nfb.BloodGroupId == bloodGroupId
                            select new NeedForBloodWithDetail()
                            {
                                Id = nfb.Id,
                                BloodGroupId = nfb.BloodGroupId,
                                HospitalId = nfb.HospitalId,
                                DonorName = nfb.DonorName,
                                DonorLastName = nfb.DonorLastName,
                                DonorPhone = nfb.DonorPhone,
                                FoundDateTime = nfb.FoundDateTime,
                                IsFound = nfb.IsFound,

                                BloodGroup_Name = bg == null ? string.Empty : bg.Name,
                                Hospital_Name = h == null ? string.Empty : h.Name
                            };

                resultList = query.ToList();
            }
            return resultList;
        }

        public NeedForBlood GetById(int id)
        {
            NeedForBlood result = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.NeedForBlood.Where(a => a.Id == id).AsNoTracking().SingleOrDefault();
            }
            return result;
        }

        public int Add(NeedForBlood record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }
            return result;
        }

        public int Update(NeedForBlood record)
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
