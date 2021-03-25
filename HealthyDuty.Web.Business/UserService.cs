using HealthyDuty.Data;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HealthyDuty.Web.Business
{
    public class UserService : IUserService
    {
        private IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public PaginatedList<UserWithDetail> GetAllPaginatedWithDetailBySearchFilter(UserSearchFilter searchFilter)
        {
            PaginatedList<UserWithDetail> resultList = new PaginatedList<UserWithDetail>(new List<UserWithDetail>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                var query = from u in dbContext.User
                            from b  in dbContext.BloodGroup.Where(x => x.Id == u.BloodGroupId).DefaultIfEmpty()
                            from  c in dbContext.City.Where(x => x.Code == u.CityCode).DefaultIfEmpty()
                            select new UserWithDetail()
                            {
                                 Id = u.Id,
                                 Age = u.Age,
                                 BloodGroupId = u.BloodGroupId,
                                 CityCode =u.CityCode,
                                 Name =u.Name,
                                 LastName = u.LastName,
                                 Email =u.Email,
                                 Phone =u.Phone,
                                 UserName =u.UserName,
                                 SexId = u.SexId,
                                 BloodGroup_Name = b ==null ? string.Empty : b.Name,
                                 City_Name = c ==null ? string.Empty : c.Name,
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
                if (searchFilter.Filter_BloodGroupId.HasValue )
                {
                    query = query.Where(r => r.BloodGroupId == searchFilter.Filter_BloodGroupId.Value);
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


                resultList = new PaginatedList<UserWithDetail>(
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

        public List<User> GetAll()
        {
            List<User> resultList = new List<User>();
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                resultList.AddRange(dbContext.User.AsNoTracking().ToList());
            }
            return resultList;
        }

        public User GetById(int id)
        {
            User result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.User.Where(a => a.Id == id).AsNoTracking().SingleOrDefault();
            }

            return result;
        }
        public User GetByUserName(string userName)
        {
            User result = null;

            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                result = dbContext.User.Where(a => a.UserName == userName).AsNoTracking().SingleOrDefault();
            }

            return result;
        }

        public int Add(User record)
        {
            int result = 0;
            using (AppDBContext dbContext = new AppDBContext(_config))
            {
                dbContext.Entry(record).State = EntityState.Added;
                result = dbContext.SaveChanges();
            }

            return result;
        }

        public int Update(User record)
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
