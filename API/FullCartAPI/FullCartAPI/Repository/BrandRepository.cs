using FullCartAPI.Models;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullCartAPI.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly fullCartDBContext _context;

        public BrandRepository(fullCartDBContext context)
        {
            _context = context;
        }

        public TblBrand AddBrand(BrandViewModel brand)
        {
            String newFilePath = ImageFileAdd(brand.ImagePath).ToString();

            TblBrand newBrand = new TblBrand();

            newBrand.BrandName = brand.BrandName;
            newBrand.ImagePath = newFilePath;
            newBrand.UserId = brand.UserId;
            newBrand.CreatedAt = DateTime.Now;

            _context.TblBrands.Add(newBrand);

            _context.SaveChanges();

            return newBrand;
        }

        public TblBrand DeleteBrand(int id)
        {
            var brand = _context.TblBrands.Find(id);

            if (brand == null)
            {
                return null;
            }

            _context.TblBrands.Remove(brand);
            DeleteImageFile(brand.ImagePath);
            _context.SaveChanges();
            return brand;
        }

        public List<TblBrand> GetAllBrand()
        {
            var categoryList = _context.TblBrands.Include(x=>x.User).Include(y=>y.User.Role).ToList();

            return categoryList;
        }

        public TblBrand GetBrandById(int id)
        {
            var category = _context.TblBrands.Include(x=>x.User).Include(y => y.User.Role).FirstOrDefault(b => b.Id == id);

            return category;
        }

        public TblBrand UpdateBrand(BrandResponse brandresonse, int id)
        {
            var brand = _context.TblBrands.FirstOrDefault(b => b.Id == id);

            brand.BrandName = brandresonse.BrandName;
            brand.ImagePath = brandresonse.ImagePath;
            brand.UserId = brandresonse.UserId;
            brand.UpdatedAt = brandresonse.UpdatedAt;

            _context.Entry(brand).State = EntityState.Modified;

            _context.SaveChanges();

            return brand;
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
