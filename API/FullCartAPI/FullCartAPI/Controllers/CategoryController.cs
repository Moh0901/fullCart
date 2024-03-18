using FullCartAPI.Models;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing.Design;

namespace FullCartAPI.Controllers
{
    [Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("getall")]

        public IActionResult GetCategory()
        {
            var categoryList = _categoryRepository.GetAllCategory();
            if (categoryList == null)
            {
                return NotFound("No Category Found");
            }
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            string host = baseUrl + "/Assest/images/category/";



            foreach (var list in categoryList)
            {
                list.ImagePath = host + list.ImagePath.ToString();
            }

           

            return Ok(categoryList);
        }

        [HttpGet("getby/{id}")]

        public IActionResult GetCategoryById(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound($"Category Not Found of {id}.");
            }
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            string host = baseUrl + "/Assest/images/category/";

            category.ImagePath = host + category.ImagePath.ToString();

           

            return Ok(category);
        }

        [HttpPost("AddNew")]

        public IActionResult AddCategory([FromForm] CategoryViewModel categoryViewModel)
        {
            var newcategory = _categoryRepository.AddCategory(categoryViewModel);

            if (categoryViewModel == null)
            {
                return NotFound();
            }

            return Ok(newcategory);
        }

        [HttpPut("updatecategory")]

        public IActionResult UpdateCategory(int id, [FromForm] CategoryViewModel categoryViewModel)
        {
            if (categoryViewModel == null)
            {
                return NotFound();
            }

            //var updatedCategory = _categoryRepository.UpdateCategory(categoryViewModel, id);
            var category = _categoryRepository.GetCategoryById(id);

            if (category != null)
            {
                if(categoryViewModel.ImagePath.ToString() == category.ImagePath)
                {
                    var updatecategoryViewModel = new CategoryResponse()
                    {
                        CategoryName = categoryViewModel.CategoryName,
                        UserId = categoryViewModel.UserId,
                        ImagePath = categoryViewModel.ImagePath.ToString(),
                        UpdatedAt = DateTime.Now
                    };
                    _categoryRepository.UpdateCategory(updatecategoryViewModel,id);
                   
                    return Ok(updatecategoryViewModel);
                }
                else
                {
                    Console.WriteLine(categoryViewModel.ImagePath.ToString());
                   
                    DeleteImageFile(categoryViewModel.ImagePath.ToString());

                    string newFilePath = ImageFileAdd(categoryViewModel.ImagePath);


                    var updatecategoryViewModel = new CategoryResponse()
                    {
                        CategoryName = categoryViewModel.CategoryName,
                        UserId = categoryViewModel.UserId,
                        ImagePath = newFilePath,
                        UpdatedAt = DateTime.Now
                    };
                    _categoryRepository.UpdateCategory(updatecategoryViewModel, id);
                    return Ok(updatecategoryViewModel);
                }
              
                
               
            }
            return NotFound("User Not Found");
        }

        [HttpDelete("deletecategory")]

        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepository.DeleteCategory(id);

            if(category == null)
            {
                return NotFound();
            }

            return Ok("Category Deleted Successfully");
        }

        [NonAction]
        public String ImageFileAdd(IFormFile ImagePath)
        {

            try
            {
                var contentPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Assest/images/category");


                if (!Directory.Exists(contentPath))
                {
                    Directory.CreateDirectory(contentPath);
                }

                // Check the  extenstion
                var ext = Path.GetExtension(ImagePath.FileName);

                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(contentPath, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                ImagePath.CopyTo(stream);
                stream.Close();
                return newFileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }

        }
        [NonAction]
        public void DeleteImageFile(String ImagePath)
        {
            ImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Assest/images/category", ImagePath);

            Console.WriteLine("-----------------------------------------------\n", ImagePath);

            try
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(ImagePath))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(ImagePath);
                    Console.WriteLine("File deleted.");
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }

        }

    }
}
