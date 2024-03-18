using FullCartAPI.Repository;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [Authorize(Roles = "1,2")]

        [HttpGet("getall")]

        public IActionResult GetAllProducts()
        {
            var productList = _inventoryRepository.GetAllProducts();
            if (productList == null)
            {
                return NotFound("No Category Found");
            }
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            string host = baseUrl + "/Assest/images/inventory/";

            foreach (var list in productList)
            {
                list.ImagePath = host + list.ImagePath.ToString();
            }

            return Ok(productList);
        }
        [Authorize(Roles = "1,2")]

        [HttpGet("getby/{id}")]

        public IActionResult GetProductById(int id)
        {
            var product = _inventoryRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound($"Product Not Found of {id}.");
            }
            string baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
            string host = baseUrl + "/Assest/images/brand/";

            product.ImagePath = host + product.ImagePath.ToString();

            return Ok(product);
        }
        [Authorize(Roles = "1")]

        [HttpPost("addnew")]

        public IActionResult AddNewProduct([FromForm] InventoryViewModel inventoryViewModel)
        {
            var newProduct = _inventoryRepository.AddProduct(inventoryViewModel);

            if (inventoryViewModel == null)
            {
                return NotFound();
            }

            return Ok(newProduct);
        }
        [Authorize(Roles = "1")]

        [HttpPut("updateproduct")]
        public IActionResult UpdateProduct(int id, [FromForm] InventoryViewModel inventoryViewModel)
        {
            if (inventoryViewModel == null)
            {
                return NotFound();
            }

            //var updatedCategory = _categoryRepository.UpdateCategory(categoryViewModel, id);
            var product = _inventoryRepository.GetProductById(id);

            if (product != null)
            {
                if (inventoryViewModel.ImagePath.ToString() == product.ImagePath)
                {
                    var updateProductViewModel = new InventoryResponse()
                    {
                        Name = inventoryViewModel.Name,
                        UserId = inventoryViewModel.UserId,
                        ImagePath = inventoryViewModel.ImagePath.ToString(),
                        UpdatedAt = DateTime.Now,
                        Description = inventoryViewModel.Description,
                        Status = inventoryViewModel.Status,
                        Price = inventoryViewModel.Price,
                        BrandId = inventoryViewModel.BrandId,
                        CategoryId = inventoryViewModel.CategoryId,
                        Quantity = inventoryViewModel.Quantity

                    };
                    _inventoryRepository.UpdateProduct(updateProductViewModel, id);

                    return Ok(updateProductViewModel);
                }
                else
                {
                    Console.WriteLine(inventoryViewModel.ImagePath.ToString());

                    DeleteImageFile(inventoryViewModel.ImagePath.ToString());

                    string newFilePath = ImageFileAdd(inventoryViewModel.ImagePath);


                    var updateProductViewModel = new InventoryResponse()
                    {
                        Name = inventoryViewModel.Name,
                        UserId = inventoryViewModel.UserId,
                        ImagePath = inventoryViewModel.ImagePath.ToString(),
                        UpdatedAt = DateTime.Now,
                        Description = inventoryViewModel.Description,
                        Status = inventoryViewModel.Status,
                        Price = inventoryViewModel.Price,
                        BrandId = inventoryViewModel.BrandId,
                        CategoryId = inventoryViewModel.CategoryId,
                        Quantity = inventoryViewModel.Quantity
                    };
                    _inventoryRepository.UpdateProduct(updateProductViewModel, id);
                    return Ok(updateProductViewModel);
                }

            }
            return NotFound("User Not Found");
        }
        [Authorize(Roles = "1")]

        [HttpDelete("deleteproduct")]

        public IActionResult DeleteCategory(int id)
        {
            var product = _inventoryRepository.DeleteProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

/*        public IActionResult SearchProduct([FromQuery] SearchViewModel searchViewModel)
        {
            if (string.IsNullOrWhiteSpace(searchViewModel.Name))
                {
                   return BadRequest();
                }

            var result = _inventoryRepository.SearchProducts(searchViewModel);
            return Ok(result);
        }*/

        [NonAction]
        public String ImageFileAdd(IFormFile ImagePath)
        {

            try
            {
                var contentPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Assest/images/inventory");


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
            ImagePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Assest/images/inventory", ImagePath);

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
