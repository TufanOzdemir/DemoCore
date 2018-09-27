﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Interface.Result;
using Interface.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IdentityServices;
using UI.Extensions;
using UI.ViewModels;

namespace UI.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            Result<List<CategoryDO>> result = _categoryService.GetAllWithoutSubCategories();
            result.Html = await PartialView("_List", result).ToString(ControllerContext);
            return Json(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CreatePage()
        {
            Result<CategoryCreateViewModel> result;
            CategoryCreateViewModel categoryCreateViewModel = new CategoryCreateViewModel();
            Result<List<CategoryDO>> categoryList = _categoryService.GetAllWithoutSubCategories();
            categoryCreateViewModel.CategoryDO = new CategoryDO();
            categoryCreateViewModel.CategoryList = categoryList.Data;
            result = categoryList.IsSuccess? new Result<CategoryCreateViewModel>(categoryCreateViewModel): new Result<CategoryCreateViewModel>(categoryList.IsSuccess, categoryList.ResultType, categoryList.Message);
            result.Html = await PartialView("_Create", result).ToString(ControllerContext);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Create(Result<CategoryCreateViewModel> model)
        {
            model.Data.CategoryDO.ParentId = model.Data.IsSubCategory ? model.Data.CategoryDO.ParentId : null;
            Result<string> result = _categoryService.Create(model.Data.CategoryDO);
            return Json(result);
        }

        public IActionResult Edit(int id)
        {
            CategoryCreateViewModel categoryCreateViewModel = new CategoryCreateViewModel();
            Result<CategoryDO> category = _categoryService.GetByID(id);
            if (category.IsSuccess)
            {
                Result<List<CategoryDO>> categoryList = _categoryService.GetAll();
                categoryCreateViewModel.CategoryDO = category.Data;
                categoryCreateViewModel.CategoryList = categoryList.Data ?? new List<CategoryDO>();
                return View(categoryCreateViewModel);
            }
            else
            {
                return RedirectToAction("Index", "Category");
            }
        }

        [HttpPost]
        public IActionResult Edit(CategoryCreateViewModel model)
        {
            Result<string> result = _categoryService.Edit(model.CategoryDO);
            return Json(result);
        }

        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index", "Category");
        }
    }
}