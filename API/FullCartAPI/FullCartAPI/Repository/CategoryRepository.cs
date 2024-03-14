using FullCartAPI.Models;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace FullCartAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly fullCartDBContext _context;

        public CategoryRepository(fullCartDBContext context)
        {
            _context = context;
        }
        public TblCategory AddCategory(CategoryViewModel category)
        {
            String newFilePath = ImageFileAdd(category.ImagePath).ToString();

            TblCategory newcategory = new TblCategory();

            newcategory.CategoryName = category.CategoryName;
            newcategory.ImagePath = newFilePath;
            newcategory.UserId = category.UserId;
            newcategory.CreatedAt = DateTime.Now;

            _context.TblCategories.Add(newcategory);

            _context.SaveChanges();

            return newcategory;
        }

        public TblCategory DeleteCategory(int id)
        {
            var category = _context.TblCategories.Find(id);

            if(category == null)
            {
                return null;
            }

            _context.TblCategories.Remove(category);
            DeleteImageFile(category.ImagePath);
            _context.SaveChanges();
            return category;
        }

        public List<TblCategory> GetAllCategory()
        {

            var categoryList = _context.TblCategories.Include(x => x.User).Include(y => y.User.Role).ToList();

            return categoryList;
        }

        public TblCategory GetCategoryById(int id)
        {
            var category = _context.TblCategories.Include(x => x.User).Include(y => y.User.Role).FirstOrDefault(b => b.Id == id);
            return category;
        }

        public TblCategory UpdateCategory(CategoryResponse categoryResponse, int id)
        {
            var category = _context.TblCategories.FirstOrDefault(b => b.Id == id);

            category.CategoryName = categoryResponse.CategoryName;
            category.ImagePath = categoryResponse.ImagePath;
            category.UserId = categoryResponse.UserId;
            category.UpdatedAt = categoryResponse.UpdatedAt;
            

            _context.Entry(category).State = EntityState.Modified;
         
            _context.SaveChanges();

            return category;

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
    


