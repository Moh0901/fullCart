using FullCartAPI.Models;
using FullCartAPI.ViewModels;

namespace FullCartAPI.Repository.Interfaces
{
    public interface IBrandRepository
    {
        List<TblBrand> GetAllBrand();

        TblBrand GetBrandById(int id);

        TblBrand AddBrand(BrandViewModel brand);

        TblBrand UpdateBrand(BrandResponse brand, int id);

        TblBrand DeleteBrand(int id);
    }
}
