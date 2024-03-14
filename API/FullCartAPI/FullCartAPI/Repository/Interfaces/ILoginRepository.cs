using FullCartAPI.Models;
using FullCartAPI.ViewModels;

namespace FullCartAPI.Repository.Interfaces
{
    public interface ILoginRepository
    {
        TblUser Authenticate(LoginViewModel user);
    }
}
