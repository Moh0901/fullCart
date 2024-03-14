using FullCartAPI.Models;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FullCartAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly fullCartDBContext _context;

        public UserRepository(fullCartDBContext context)
        {
            _context = context;
        }
        public TblUser AddNewUsers(UserViewModel userViewModel)
        {
            TblUser user = new TblUser();

            user.Name = userViewModel.Name;
            user.Username = userViewModel.Username;
            user.Email = userViewModel.Email;
            user.Password = userViewModel.Password;
            user.RoleId = userViewModel.RoleId;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public List<TblUser> GetAllUsers()
        {
           // var usersList = _context.Users.ToList();
            var usersList = _context.Users.Include(x=>x.Role).ToList();

            return usersList;
        }

        public TblUser GetUsersById(int id)
        {
           // var user = _context.Users.Find(id);
            var user = _context.Users.Include(x => x.Role).FirstOrDefault(y => y.Id == id);

            return user;
        }
    }
}
