using Microsoft.Extensions.Logging;
using PTLab2_web.Data.Models;
using PTLab2_web.Data.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace PTLab2_web.Data.Services.Implimentations
{
    public class ShopDbService : IShopDbService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ShopDbService> _logger;

        public ShopDbService(HttpClient httpClient, ILogger<ShopDbService> logger) => 
            (_httpClient, _logger) = (httpClient, logger);

        public async Task<ServiceResponse<Product>> GetProduct(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"/product/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error (GetProduct): {ex}");

                ServiceResponse<Product> _response = new ServiceResponse<Product>();

                _response.Data = null;
                _response.Success = false;
                _response.Message = "Something went wrong while getting product.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

                return _response;
            }
        }

        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("/product");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error (GetProducts): {ex}");

                ServiceResponse<List<Product>> _response = new ServiceResponse<List<Product>>();
                
                _response.Data = new List<Product>();
                _response.Success = false;
                _response.Message = "Something went wrong while getting products.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

                return _response;
            }
        }

        public async Task<ServiceResponse<List<Purchase>>> GetPurchases(int userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ServiceResponse<List<Purchase>>>($"/purchase/get_by_user/{userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error (GetProducts): {ex}");

                ServiceResponse<List<Purchase>> _response = new ServiceResponse<List<Purchase>>();

                _response.Data = new List<Purchase>();
                _response.Success = false;
                _response.Message = "Something went wrong while getting user purchases.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

                return _response;
            }
        }

        public async Task<ServiceResponse<User>> GetUser(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ServiceResponse<User>>($"/user/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error (GetUser): {ex}");

                ServiceResponse<User> _response = new ServiceResponse<User>();

                _response.Data = null;
                _response.Success = false;
                _response.Message = "Something went wrong while getting user.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

                return _response;
            }
        }

        public async Task<ServiceResponse<User>> Login(string email, string password)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            try
            {
                var postResponse = await _httpClient.PostAsJsonAsync($"/auth/login?email={email}&password={password}", options);
                postResponse.EnsureSuccessStatusCode();
                
                ServiceResponse<User> _response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<User>>();

                return _response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error (Login): {ex}");
                ServiceResponse<User> _response = new ServiceResponse<User>();

                _response.Data = null;
                _response.Success = false;
                _response.Message = "Something went wrong while login.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

                return _response;
            }
        }

        public async Task<ServiceResponse<Purchase>> MakePurchase(int userId, int productId, string address)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            try
            {
                var postResponse = await _httpClient.PostAsJsonAsync($"/purchase/make_purchase?userId={userId}&productId={productId}&address={address}", options);
                postResponse.EnsureSuccessStatusCode();

                ServiceResponse<Purchase> _response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<Purchase>>();

                return _response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error (MakePurchases): {ex}");
                ServiceResponse<Purchase> _response = new ServiceResponse<Purchase>();

                _response.Data = null;
                _response.Success = false;
                _response.Message = "Something went wrong while making purchase.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

                return _response;
            }
        }

        public async Task<ServiceResponse<User>> Register(string name, string email, string password)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            try
            {
                var postResponse = await _httpClient.PostAsJsonAsync($"/auth/register?name={name}&email={email}&password={password}", options);
                postResponse.EnsureSuccessStatusCode();

                ServiceResponse<User> _response = await postResponse.Content.ReadFromJsonAsync<ServiceResponse<User>>();

                return _response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error (Register): {ex}");
                ServiceResponse<User> _response = new ServiceResponse<User>();

                _response.Data = null;
                _response.Success = false;
                _response.Message = "Something went wrong while register.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };

                return _response;
            }
        }
    }
}
