using PTLab2_web.Data.Models;

namespace PTLab2_web.Data.Services.Interfaces
{
    public interface IUserService
    {
        Task SaveUser(User user);
        Task DeleteUser();
        Task<User> GetUser();
    }
}
