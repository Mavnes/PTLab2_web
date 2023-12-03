using Microsoft.AspNetCore.Components;
using MudBlazor;
using PTLab2_web.Data.Models;
using PTLab2_web.Shared;

namespace PTLab2_web.Pages
{
    public partial class Store
    {
        private User? user;

        private IEnumerable<Product> products = new List<Product>();
        private bool isLoading = true;

        private void RowClickEvent(TableRowClickEventArgs<Product> tableRowClickEventArgs)
        {
            var product = tableRowClickEventArgs.Item;

            if (product is null) 
            {
                return;
            }

            if (user is null)
            {
                Snackbar.Add("Вы должны быть авторизированы, чтобы совершать покупки.", Severity.Warning);
                NavigationManager.NavigateTo("/account");

                return;
            }

            NavigationManager.NavigateTo($"/purchase/{product.Id}");
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

                if (user is not null) 
                {
                    var userResponse = await ShopDbService.GetUser(user.Id);

                    if (userResponse.Success)
                    {
                        user = userResponse.Data;
                    }
                }

                var response = await ShopDbService.GetProducts();

                if (response is not null)
                {
                    products = response.Data;
                }

                isLoading = false;

                StateHasChanged();
            }
        }
    }
}
