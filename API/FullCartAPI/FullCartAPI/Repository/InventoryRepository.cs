using FullCartAPI.Models;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullCartAPI.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly fullCartDBContext _context;

        public InventoryRepository(fullCartDBContext context)
        {
            _context = context;
        }
        public TblInventory AddProduct(InventoryViewModel inventory)
        {
            String newFilePath = ImageFileAdd(inventory.ImagePath).ToString();

            TblInventory newinventory = new TblInventory();

            newinventory.Name = inventory.Name;
            newinventory.ImagePath = newFilePath;
            newinventory.UserId = inventory.UserId;
            newinventory.CreatedAt = DateTime.Now;
            //newinventory.UpdatedAt = DateTime.Now;
            newinventory.Description = inventory.Description;
            newinventory.Price = inventory.Price;
            newinventory.Status = inventory.Status;
            newinventory.BrandId = inventory.BrandId;
            //newinventory.Brand = inventory.Brand;
            newinventory.CategoryId = inventory.CategoryId;
            newinventory.Quantity = inventory.Quantity;

            _context.TblInventories.Add(newinventory);

            _context.SaveChanges();

            return newinventory;
        }

        public TblInventory DeleteProduct(int id)
        {
            var inevntory = _context.TblInventories.Find(id);

            if (inevntory == null)
            {
                return null;
            }

            _context.TblInventories.Remove(inevntory);
            DeleteImageFile(inevntory.ImagePath);
            _context.SaveChanges();
            return inevntory;
        }

        public List<TblInventory> GetAllProducts()
        {
            var inventoryList = _context.TblInventories.Include(x=>x.User).Include(y=>y.Brand).Include(z=>z.Category).Include(a=>a.User.Role).ToList();

            return inventoryList;
        }

        public TblInventory GetProductById(int id)
        {
            var inventory = _context.TblInventories.Include(x => x.User).Include(y => y.Brand).Include(z => z.Category).Include(a => a.User.Role).FirstOrDefault(b => b.Id == id);

            return inventory;
        }

        public TblInventory UpdateProduct(InventoryResponse inventoryResponse, int id)
        {
            var inventory = _context.TblInventories.FirstOrDefault(b => b.Id == id);

            inventory.Name = inventoryResponse.Name;
            inventory.ImagePath = inventoryResponse.ImagePath;
            inventory.UserId = inventoryResponse.UserId;
            inventory.UpdatedAt = inventoryResponse.UpdatedAt;
            inventory.Status = inventoryResponse.Status;
            inventory.Description = inventoryResponse.Description;
            inventory.Price = inventoryResponse.Price;
            inventory.BrandId = inventoryResponse.BrandId;
            //newinventory.Brand = inventory.Brand;
            inventory.CategoryId = inventoryResponse.CategoryId;
            inventory.Quantity = inventoryResponse.Quantity;

            _context.Entry(inventory).State = EntityState.Modified;

            _context.SaveChanges();

            return inventory;
        }

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
