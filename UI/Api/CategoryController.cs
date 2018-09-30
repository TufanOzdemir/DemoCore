using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Interface.Result;
using Interface.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("CategoryTree")]
        public IActionResult GetProductsByCategoryID()
        {
            Result<List<CategoryDO>> result = _categoryService.GetCategoryTree();
            return Json(result);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Result<List<CategoryDO>> result = _categoryService.GetAllWithoutSubCategories();
            return Json(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Create([FromBody]CategoryDO categoryDO)
        {
            Result<string> result = _categoryService.Create(categoryDO);
            return Json(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Edit([FromBody]CategoryDO categoryDO)
        {
            Result<string> result = _categoryService.Edit(categoryDO);
            return Json(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Delete(int id)
        {
            Result<string> result = _categoryService.Delete(id);
            return Json(result);
        }
    }
}