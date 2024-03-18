using FullCartAPI.Models;
using FullCartAPI.ViewModels;

namespace FullCartAPI.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        List<TblInventory> GetAllProducts();

        TblInventory GetProductById(int id);

        TblInventory AddProduct(InventoryViewModel inventory);

        TblInventory UpdateProduct(InventoryResponse inventory, int id);

        TblInventory DeleteProduct(int id);

       // List<TblInventory> SearchProducts( SearchViewModel searchViewModel);
    }
}
