using Microsoft.AspNetCore.Identity;

namespace weblearn.Repository
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
