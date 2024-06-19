using TransportCompany.Models;

namespace TransportCompany.Interface
{
    public interface IUserService
    {
        public Task<string> CreateTokenJWT(User user);
    }
}
