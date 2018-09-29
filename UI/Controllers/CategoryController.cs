using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Interface.Result;
using Interface.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Service.IdentityServices;
using UI.Extensions;
using UI.ViewModels;

namespace UI.Controllers
{
    [Authorize(Roles ="Admin")]
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
        
        public IActionResult Details(int id)
        {
            return RedirectToAction("Index", "Product", new { id });
        }

        [Route("[Controller]/KategoriAgaci")]
        public IActionResult CategoryTree()
        {
            return View();
        }

        public async Task<IActionResult> CategoryTreePage()
        {
            Result<string> result = new Result<string>("");
            result.Html = await PartialView("_CategoryTree", result).ToString(ControllerContext);
            return Json(result);
        }

        public IActionResult CategoryTreeAsync()
        {
            Result<List<CategoryDO>> result = _categoryService.GetCategoryTree();
            result.Html = $"<pre> {CategoryTreeInitializer(result.Data, 0)} </pre>";
            result.Data = null;
            return Json(result);
        }

        private string CategoryTreeInitializer(List<CategoryDO> list, int spaceCount)
        {
            string categoryString = "";
            int count = ++spaceCount;
            foreach (var item in list)
            {
                for (int i = 0; i < spaceCount; i++)
                {
                    categoryString += "       ";
                }
                categoryString += $"<a href=\"Details/{item.Id }\">{item.Name}</a>";
                categoryString += "<br/>";
                categoryString += CategoryTreeInitializer(item.SubCategoryList, count);
            }
            return categoryString;
        }
        
        public async Task<IActionResult> List()
        {
            Result<List<CategoryDO>> result = _categoryService.GetAllWithoutSubCategories();
            result.Html = await PartialView("_List", result).ToString(ControllerContext);
            return Json(result);
        }

        [Route("Kategori/Ekle")]
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
            result = categoryList.IsSuccess ? new Result<CategoryCreateViewModel>(categoryCreateViewModel) : new Result<CategoryCreateViewModel>(categoryList.IsSuccess, categoryList.ResultType, categoryList.Message);
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
            return View(id);
        }

        public async Task<IActionResult> Update(int id)
        {
            Result<CategoryCreateViewModel> result;
            CategoryCreateViewModel categoryCreateViewModel = new CategoryCreateViewModel();
            Result<CategoryDO> category = _categoryService.GetByIDNaturalMode(id);
            if (category.IsSuccess)
            {
                Result<List<CategoryDO>> categoryList = _categoryService.GetAllWithoutSubCategories();
                if (categoryList.IsSuccess)
                {
                    categoryCreateViewModel.CategoryDO = category.Data;
                    categoryCreateViewModel.CategoryList = categoryList.Data;
                    result = new Result<CategoryCreateViewModel>(categoryCreateViewModel);
                }
                else
                {
                    result = new Result<CategoryCreateViewModel>(categoryList.IsSuccess, categoryList.ResultType, categoryList.Message);
                }

            }
            else
            {
                result = new Result<CategoryCreateViewModel>(category.IsSuccess, category.ResultType, category.Message);
            }
            result.Html = await PartialView("_Edit", result).ToString(ControllerContext);
            return Json(result);
        }

        [HttpPost]
        public IActionResult Edit(Result<CategoryCreateViewModel> model)
        {
            model.Data.CategoryDO.ParentId = model.Data.IsSubCategory ? model.Data.CategoryDO.ParentId : null;
            Result<string> result = _categoryService.Edit(model.Data.CategoryDO);
            return Json(result);
        }

        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Index", "Category");
        }
    }
}