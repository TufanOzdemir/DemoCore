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
    [Route("api/Product")]
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("ListProductByCategory/{id:int}")]
        public IActionResult GetProductsByCategoryID(int id)
        {
            Result<List<ProductDO>> result = _productService.GetProductListByCategoryID(id);
            result.Data.Reverse();
            return Json(result);
        }

        [HttpGet]
        [Route("List")]
        public IActionResult GetAll()
        {
            Result<List<ProductDO>> result = _productService.GetAll();
            result.Data.Reverse();
            return Json(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult GetByID(int id)
        {
            Result<ProductDO> result = _productService.GetByID(id);
            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Create([FromBody]ProductDO productDO)
        {
            Result<string> result = _productService.Create(productDO);
            return Json(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Edit([FromBody]ProductDO productDO)
        {
            Result<string> result = _productService.Edit(productDO);
            return Json(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin,Moderator")]
        public IActionResult Delete(int id)
        {
            Result<string> result = _productService.Delete(id);
            return Json(result);
        }
    }
}