using PTLab2_web.Data.Models;

namespace PTLab2_web.Data.Services.Interfaces
{
    public interface IShopDbService
    {
        Task<ServiceResponse<User>> Register(string name, string email, string password);
        Task<ServiceResponse<User>> Login(string email, string password);
        Task<ServiceResponse<User>> GetUser(int id);
        Task<ServiceResponse<List<Product>>> GetProducts();
        Task<ServiceResponse<Product>> GetProduct(int id);
        Task<ServiceResponse<List<Purchase>>> GetPurchases(int userId);
        Task<ServiceResponse<Purchase>> MakePurchase(int userId, int productId, string address);
    }
}
