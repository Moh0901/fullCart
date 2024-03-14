using FullCartAPI.Models;

namespace FullCartAPI.Repository.Interfaces
{
    public interface ITokenHandler
    {
        String CreateToken(TblUser user);
    }
}
