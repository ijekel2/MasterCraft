using MasterCraft.Client.Models;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUser> Login(AuthenticationRequest pAuthenticationRequest);
        Task Logout();
    }
}