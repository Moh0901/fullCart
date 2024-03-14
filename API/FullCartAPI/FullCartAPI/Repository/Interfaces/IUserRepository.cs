using FullCartAPI.Models;

namespace FullCartAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<TblUser> GetAllUsers();

        TblUser GetUsersById(int id);

        TblUser AddNewUsers(ViewModels.UserViewModel userViewModel);

    }
}
