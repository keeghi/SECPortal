using SecPortal.Webapp.CQRS.Infrastructures;
using System.Threading.Tasks;

namespace SecPortal.Webapp.Authorizations
{
    public interface IAuthorization
    {
        Task<IBaseResponse> Login(string username, string password);
    }
}
