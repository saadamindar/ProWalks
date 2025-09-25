using Microsoft.AspNetCore.Identity;

namespace ProWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
