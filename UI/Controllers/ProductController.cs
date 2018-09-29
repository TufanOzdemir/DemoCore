using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Domain.Models;
using Interface.Result;
using Interface.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.Helpers;
using UI.ViewModels;

namespace UI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;
        IHostingEnvironment _hostingEnvironment;
        public ProductController(IProductService productService, ICategoryService categoryService, IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(int id = 0)
        {
            Result<List<ProductDO>> result = id == 0 ? _productService.GetAll() : _productService.GetProductListByCategoryID(id);
            result.Data.Reverse();
            return View(result.Data);
        }

        [Route("[Controller]/{id:int}")]
        [Authorize(Roles ="Admin,Moderator")]
        public IActionResult Details(int id)
        {
            Result<ProductDO> result = _productService.GetByID(id);
            return View(result.Data);
        }

        [Route("[Controller]/UrunEkle")]
        public IActionResult Create()
        {
            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel();
            Result<List<CategoryDO>> categoryList = _categoryService.GetAll();
            productCreateViewModel.ProductDO = new ProductDO();
            productCreateViewModel.CategoryList = categoryList.IsSuccess ? categoryList.Data : new List<CategoryDO>();
            return View(productCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model, IFormFile pic)
        {
            ImageHelper imageHelper = new ImageHelper(_hostingEnvironment);
            model.ProductDO.ImageUrl = await imageHelper.ImageUploader(pic, "Content\\Product");
            _productService.Create(model.ProductDO);
            return RedirectToAction("Index", "Product");
        }
        
        public IActionResult Edit(int id)
        {
            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel();
            Result<List<CategoryDO>> categoryList = _categoryService.GetAll();
            Result<ProductDO> product = _productService.GetByID(id);
            productCreateViewModel.ProductDO = product.IsSuccess ? product.Data : new ProductDO();
            productCreateViewModel.CategoryList = categoryList.IsSuccess ? categoryList.Data : new List<CategoryDO>();
            return View(productCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateViewModel model, IFormFile pic)
        {
            ImageHelper imageHelper = new ImageHelper(_hostingEnvironment);
            model.ProductDO.ImageUrl = await imageHelper.ImageUploader(pic, "Content\\Product");
            _productService.Edit(model.ProductDO);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index", "Product");
        }
    }
}