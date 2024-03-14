using FullCartAPI.Models;
using FullCartAPI.ViewModels;

namespace FullCartAPI.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        List<TblCategory> GetAllCategory();

        TblCategory GetCategoryById(int id);

        TblCategory AddCategory(CategoryViewModel category);

        TblCategory UpdateCategory(CategoryResponse category, int id);

        TblCategory DeleteCategory(int id);
    }
}
