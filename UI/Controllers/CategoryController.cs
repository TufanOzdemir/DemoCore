using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Interface.Result;
using Interface.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IdentityServices;
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
            Result<List<CategoryDO>> categoryList = _categoryService.GetAll();
            return View(categoryList.Data);
        }

        public IActionResult Create()
        {
            CategoryCreateViewModel categoryCreateViewModel = new CategoryCreateViewModel();
            Result<List<CategoryDO>> categoryList = _categoryService.GetAll();
            categoryCreateViewModel.CategoryList = categoryList.Data ?? new List<CategoryDO>();
            categoryCreateViewModel.CategoryDO = new CategoryDO();
            return View(categoryCreateViewModel);
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel model)
        {
            model.CategoryDO.ParentId = model.IsSubCategory ? model.CategoryDO.ParentId : null;
            Result<string> result = _categoryService.Create(model.CategoryDO);
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
            _categoryService.Edit(model.CategoryDO);
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index", "Category");
        }
    }
}