using Microsoft.AspNetCore.Components;
using MudBlazor;
using PTLab2_web.Data.Models;

namespace PTLab2_web.Shared
{
    public partial class PurchaseDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public Product product { get; set; } = new Product();
        [Parameter] public User user { get; set; } = null;
        private string address = string.Empty;

        async void Submit()
        {
            await ShopDbService.MakePurchase(user.Id, product.Id, address);
            var response =  await ShopDbService.GetUser(user.Id);
            user = response.Data;
            UserService.SaveUser(user);

            MudDialog.Close(DialogResult.Ok(true));
        }

        void Cancel() => MudDialog.Cancel();

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
    }
}
