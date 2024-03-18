using FullCartAPI.ExcelHelper;
using FullCartAPI.Models;
using FullCartAPI.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace FullCartAPI.Controllers
{
    [Authorize(Roles = "1")]
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelProductsController : ControllerBase
    {
        private readonly fullCartDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExcelProductsController(fullCartDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult> Upload(IFormFile formFile ,CancellationToken ct)
        {
            if (Request.Form.Files.Count == 0) return NoContent();

            var file = Request.Form.Files[0];
            var filePath = SaveFile(file);

            // load product requests from excel file
            var productRequests = ExcelHelperClass.Import<InventoryExcelModel>(filePath);

            // save product requests to database
            foreach (var productRequest in productRequests)
            {
                var product = new TblInventory
                {
                    
                    Name = productRequest.Name,
                    Description = productRequest.Description,
                    Price = productRequest.Price,
                    BrandId = productRequest.BrandId,
                    CategoryId = productRequest.CategoryId,
                    Quantity = productRequest.Quantity,
                    ImagePath = productRequest.ImagePath,
                    UserId = productRequest.UserId,
                    Status = productRequest.Status,
                    CreatedAt = DateTime.Now
                    
                };
                await _context.AddAsync(product, ct);
            }
            await _context.SaveChangesAsync(ct);

            return Ok();
        }


        [HttpGet("download")]
        public async Task<FileResult> Download(CancellationToken ct)
        {
            var products = await _context.TblInventories.ToListAsync(ct);
            var file = ExportExcelHelperClass.CreateFile(products);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllProducts.xlsx");
        }

        // save the uploaded file into wwwroot/uploads folder
        private string SaveFile(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new BadHttpRequestException("File is empty.");
            }

            var extension = Path.GetExtension(file.FileName);

            var webRootPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var folderPath = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"{Guid.NewGuid()}.{extension}";
            var filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return filePath;
        }

        
    }
}
