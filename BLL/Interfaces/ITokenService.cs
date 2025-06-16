using DAL.Models;

namespace BLL.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWTToken(Account account);
    }
}
