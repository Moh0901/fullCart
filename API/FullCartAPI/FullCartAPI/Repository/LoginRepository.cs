using FullCartAPI.Models;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FullCartAPI.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly fullCartDBContext _context;

        public LoginRepository(fullCartDBContext context)
        {
            _context = context;
        }
        public TblUser Authenticate(LoginViewModel user)
        {
            var loggedUser = _context.Users.FirstOrDefault(
                x => x.Username.ToLower() == user.Username.ToLower() && x.Password == user.Password);

            if (loggedUser == null)
            {
                return null;
            }
            return loggedUser;
        }
    }
}
