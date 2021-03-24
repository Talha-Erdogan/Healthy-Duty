﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthyDuty.Data.Entity;
using HealthyDuty.Web.Business.Enums;
using HealthyDuty.Web.Business.Interfaces;
using HealthyDuty.Web.Business.Models;
using HealthyDuty.Web.Business.Models.City;
using HealthyDuty.Web.Filters;
using HealthyDuty.Web.Models.City;
using Microsoft.AspNetCore.Mvc;

namespace HealthyDuty.Web.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }


        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CITY_LIST)]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();

            model.Filter = new ListFilterViewModel();
            model.CurrentPage = 1;
            model.PageSize = 10;
            CitySearchFilter searchFilter = new CitySearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            try
            {
                model.DataList = _cityService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<City>(new List<City>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CITY_LIST)]
        [HttpPost]
        public ActionResult List(ListViewModel model)
        {
            // filter bilgilerinin default boş değerlerle doldurulması sağlanıyor
            if (model.Filter == null)
            {
                model.Filter = new ListFilterViewModel();
            }

            if (!model.CurrentPage.HasValue)
            {
                model.CurrentPage = 1;
            }

            if (!model.PageSize.HasValue)
            {
                model.PageSize = 10;
            }

            CitySearchFilter searchFilter = new CitySearchFilter();
            searchFilter.CurrentPage = model.CurrentPage.HasValue ? model.CurrentPage.Value : 1;
            searchFilter.PageSize = model.PageSize.HasValue ? model.PageSize.Value : 10;
            searchFilter.SortOn = model.SortOn;
            searchFilter.SortDirection = model.SortDirection;
            searchFilter.Filter_Name = model.Filter.Filter_Name;

            try
            {
                model.DataList = _cityService.GetAllPaginatedWithDetailBySearchFilter(searchFilter);
            }
            catch
            {
                model.DataList = new PaginatedList<City>(new List<City>(), 0, searchFilter.CurrentPage, searchFilter.PageSize, searchFilter.SortOn, searchFilter.SortDirection);
                ViewBag.ErrorMessage = "Error";
            }
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CITY_ADD)]
        public ActionResult Add()
        {
            Models.City.AddViewModel model = new AddViewModel();
            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CITY_ADD)]
        [HttpPost]
        public ActionResult Add(Models.City.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HealthyDuty.Data.Entity.City city = new HealthyDuty.Data.Entity.City();
            city.Code = model.Code;
            city.Name = model.Name;
            
            try
            {
                _cityService.Add(city);
                return RedirectToAction(nameof(CityController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Saved.";
                return View(model);
            }
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CITY_EDIT)]
        public ActionResult Edit(int id)
        {
            Models.City.AddViewModel model = new AddViewModel();
            try
            {
                var result = _cityService.GetById(id);
                if (result == null)
                {
                    return View("_ErrorNotExist");
                }
                model.Code = result.Code;
                model.Id = result.Id;
                model.Name = result.Name;
            }
            catch
            {
                ViewBag.ErrorMessage = "Record Not Found";
                return View("_ErrorNotExist");
            }

            return View(model);
        }

        //[AppAuthorizeFilter(AuthCodeStatic.PAGE_CITY_EDIT)]
        [HttpPost]
        public ActionResult Edit(Models.City.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var city = _cityService.GetById(model.Id);
                if (city == null)
                {
                    return View("_ErrorNotExist");
                }
                city.Code = model.Code;
                city.Name = model.Name;
                if (model.SubmitType == "Edit")
                {
                    _cityService.Update(city);
                }
                
                return RedirectToAction(nameof(CityController.List));
            }
            catch
            {
                ViewBag.ErrorMessage = "Not Operation.";
                return View(model);
            }

        }
    }
}
