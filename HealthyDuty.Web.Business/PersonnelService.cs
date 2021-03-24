using HealthyDuty.Data;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.Personnel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;

namespace HealthyDuty.Web.Business
{
    public class PersonnelService : IPersonnelService
    {
        private IConfiguration _config;

        public PersonnelService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<PersonnelWithDetail> GetAllPaginatedWithDetailBySearchFilter(PersonnelSearchFilter searchFilter)
        {
            PaginatedList<PersonnelWithDetail> resultList = new PaginatedList<PersonnelWithDetail>(new List<PersonnelWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from p in dbContext.Personnel
                            from h in dbContext.Hospital.Where(x => x.Id == p.HospitalId).DefaultIfEmpty()
                            where h.IsDeleted == false
                            select new PersonnelWithDetail()
                            {
                                Id = p.Id,
                                Name = p.Name,
                                LastName = p.LastName,
                                TC = p.TC,
                                HospitalId = p.HospitalId,
                                Address = p.Address,
                                Phone = p.Phone,
                                UserName =p.UserName,

                                Hospital_Name = h == null ? String.Empty : h.Name
                            };

                // filtering
                if (!string.IsNullOrEmpty(searchFilter.Filter_Name))
                {
                    query = query.Where(r => r.Name.Contains(searchFilter.Filter_Name));
                }
                if (!string.IsNullOrEmpty(searchFilter.Filter_LastName))
                {
                    query = query.Where(r => r.LastName.Contains(searchFilter.Filter_LastName));
                }
                if (searchFilter.Filter_HospitalId.HasValue)
                {
                    query = query.Where(r => r.HospitalId == searchFilter.Filter_HospitalId.Value);
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


                resultList = new PaginatedList<PersonnelWithDetail>(
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

        public List<Personnel> GetAll()
        {
            List<Personnel> resultList = new List<Personnel>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.Personnel.AsNoTracking().ToList());
            }
            return resultList;
        }

        public Personnel GetById(int id)
        {
            Personnel result = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Personnel.Where(a => a.Id == id).AsNoTracking().SingleOrDefault();
            }
            return result;
        }
        public Personnel GetByUserName(string userName)
        {
            Personnel result = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Personnel.Where(a => a.UserName == userName).AsNoTracking().SingleOrDefault();
            }
            return result;
        }

        public Personnel GetByUserNameAndPassword(string userName, string password)
        {
            Personnel result = null;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.Personnel.Where(a => a.UserName == userName && a.Password == password).AsNoTracking().FirstOrDefault();
            }
            return result;
        }

        public int Add(Personnel record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }
            return result;
        }

        public int Update(Personnel record)
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
