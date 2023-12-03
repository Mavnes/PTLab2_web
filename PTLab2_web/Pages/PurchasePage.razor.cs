using Microsoft.AspNetCore.Components;
using MudBlazor;
using PTLab2_web.Data.Models;
using PTLab2_web.Data.Services.Implimentations;

namespace PTLab2_web.Pages
{
    public partial class PurchasePage
    {
        [Parameter] public int productId { get; set; }

        private User? user = null;
        private Product? product = null;
        private string address = string.Empty;

        private bool isLoading = true;
        private bool purchaseProcessing = false;

        private async Task Submit()
        {
            purchaseProcessing = true;

            var response = await ShopDbService.MakePurchase(user.Id, product.Id, address);

            if (response.Success)
            {
                Snackbar.Add($"Покупка успешно завершена: {product.Name}.", Severity.Success);
                await UpdateUser();
            }
            else
            {
                Snackbar.Add(response.Message, Severity.Error);
            }

            purchaseProcessing = false;

            NavigationManager.NavigateTo($"/");
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo($"/");
        }

        private bool DisableMakePurchaseButton()
        {
            if (purchaseProcessing)
                return true;

            if (string.IsNullOrEmpty(address))
                return true;

            return false;
        }

        private string GetSalePrice(float price)
        {
            if (user is null)
            {
                return $"{price} ₽";
            }

            if (user.Sale <= 0)
            {
                return $"{price} ₽";
            }

            price = (price / 100) * (100 - user.Sale);

            return $"{price} ₽";
        }

        private string GetBasePrice(float price)
        {
            if (user is null)
            {
                return string.Empty;
            }

            if (user.Sale <= 0)
            {
                return string.Empty;
            }

            return $"{price}";
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                user = await UserService.GetUser();

                var response = await ShopDbService.GetProduct(productId);

                if (response is not null)
                {
                    product = response.Data;
                }

                isLoading = false;

                StateHasChanged();
            }
        }

        private async Task<bool> UpdateUser()
        {
            var response = await ShopDbService.GetUser(user.Id);

            if (response.Success)
            {
                user = response.Data;
                return UserService.SaveUser(user).IsCompletedSuccessfully;
            }

            Snackbar.Add("Ошибка при обновлении данных пользователя.", Severity.Error);
            return false;
        }
    }
}
