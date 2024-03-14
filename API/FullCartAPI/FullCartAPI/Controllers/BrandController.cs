using FullCartAPI.Repository;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    //[Authorize(Roles = "2")]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet("allbrands")]

        public IActionResult GetBrand()
        {
            var brandList = _brandRepository.GetAllBrand();
            if (brandList == null)
            {
                return NotFound("No Category Found");
            }
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            string host = baseUrl + "/Assest/images/brand/";

            foreach (var list in brandList)
            {
                list.ImagePath = host + list.ImagePath.ToString();
            }

            return Ok(brandList);
        }

        [HttpGet("getby/{id}")]

        public IActionResult GetBrandById(int id)
        {
            try
            {
                var brand = _brandRepository.GetBrandById(id);

                if (brand == null)
                {
                    return NotFound($"Brand Not Found of {id}.");
                }
                string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
                string host = baseUrl + "/Assest/images/brand/";

                brand.ImagePath = host + brand.ImagePath.ToString();

                return Ok(brand);
            }

            catch (Exception ex)
            {
                 return Ok(ex.Message);
            }
            
        }
        [HttpPost("addnew")]

        public IActionResult AddNewBrand([FromForm] BrandViewModel brandViewModel)
        {
            if (brandViewModel == null)
            {
                return NotFound();
            }
            var newbrand = _brandRepository.AddBrand(brandViewModel);

            return Ok(newbrand);
        }

        [HttpPut("updatebrand")]
        public IActionResult UpdateCategory(int id, [FromForm] BrandViewModel brandViewModel)
        {
            if (brandViewModel == null)
            {
                return NotFound();
            }

            //var updatedCategory = _categoryRepository.UpdateCategory(categoryViewModel, id);
            var brand = _brandRepository.GetBrandById(id);

            if (brand != null)
            {
                if (brandViewModel.ImagePath.ToString() == brand.ImagePath)
                {
                    var updatebrandViewModel = new BrandResponse()
                    {
                        BrandName = brandViewModel.BrandName,
                        UserId = brandViewModel.UserId,
                        ImagePath = brandViewModel.ImagePath.ToString(),
                        UpdatedAt = DateTime.Now
                    };
                    _brandRepository.UpdateBrand(updatebrandViewModel, id);

                    return Ok(updatebrandViewModel);
                }
                else
                {
                    Console.WriteLine(brandViewModel.ImagePath.ToString());

                    DeleteImageFile(brandViewModel.ImagePath.ToString());

                    string newFilePath = ImageFileAdd(brandViewModel.ImagePath);


                    var updatebrandViewModel = new BrandResponse()
                    {
                        BrandName = brandViewModel.BrandName,
                        UserId = brandViewModel.UserId,
                        ImagePath = newFilePath,
                        UpdatedAt = DateTime.Now
                    };
                    _brandRepository.UpdateBrand(updatebrandViewModel, id);
                    return Ok(updatebrandViewModel);
                }

            }
            return NotFound("User Not Found");
        }

        [HttpDelete("deletebrand")]

        public IActionResult DeleteCategory(int id)
        {
            var brand = _brandRepository.DeleteBrand(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [NonAction]
        public String ImageFileAdd(IFormFile ImagePath)
        {

            try
            {
                var contentPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Assest/images/brand");


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
            ImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Assest/images/brand", ImagePath);

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
