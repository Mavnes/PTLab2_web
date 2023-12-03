using MudBlazor;
using PTLab2_web.Data.Models;
using System.Text.RegularExpressions;

namespace PTLab2_web.Pages
{
    public partial class Account
    {
        private User? user;
        private IEnumerable<Purchase> purchases = new List<Purchase>();

        private bool isLoading = true;
        private bool isPurchasesLoading = false;
        private bool loginProcessing = false;
        private bool registerProcessing = false;
        private bool logoutProcessing = false;

        private bool showPassword = false;
        InputType passwordInput = InputType.Password;
        string passwordInputIcon = Icons.Material.Filled.VisibilityOff;

        bool success;
        string[] errors = { };

        MudTextField<string> nameField;
        MudTextField<string> passwordField;
        MudTextField<string> emailField;

        MudTextField<string> passwordLogField;
        MudTextField<string> emailLogField;

        void ShowPasswordField()
        {
            if(showPassword)
        {
                showPassword = false;
                passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                passwordInput = InputType.Password;
            }
        else
            {
                showPassword = true;
                passwordInputIcon = Icons.Material.Filled.Visibility;
                passwordInput = InputType.Text;
            }
        }

        private IEnumerable<string> PasswordStrength(string pw)
        {
            if (string.IsNullOrWhiteSpace(pw))
            {
                yield return "Пароль является обязательным полем.";
                yield break;
            }
            if (pw.Length < 8)
                yield return "Пароль должен состоять из минимум 8 знаков.";
            if (!Regex.IsMatch(pw, @"[A-Z]"))
                yield return "Пароль должен содержать минимум 1 заглавную букву.";
            if (!Regex.IsMatch(pw, @"[a-z]"))
                yield return "Пароль должен содержать минимум 1 незаглавную букву.";
            if (!Regex.IsMatch(pw, @"[0-9]"))
                yield return "Пароль должен содержать минимум 1 цифру.";
        }

        private string PasswordMatch(string arg)
        {
            if (passwordField.Value != arg)
                return "Пароли не совпадают.";

            return null;
        }

        private async Task Login()
        {
            loginProcessing = true;

            var password = passwordLogField.Value;
            var email = emailLogField.Value;

            var response = await ShopDbService.Login(email, password);

            if (response.Success)
            {
                user = response.Data;
                await UserService.SaveUser(user);

                Snackbar.Add("Был выполнен вход в аккаунт.", Severity.Success);

                purchases = new List<Purchase>();
                await LoadPurchases();
            }
            else
            {
                Snackbar.Add(response.Message, Severity.Error);
            }

            loginProcessing = false;
        }

        private async Task CreateAccount()
        {
            registerProcessing = true;

            var name = nameField.Value;
            var password = passwordField.Value;
            var email = emailField.Value;

            var response = await ShopDbService.Register(name, email, password);

            if (response.Success)
            {
                user = response.Data;
                await UserService.SaveUser(user);

                Snackbar.Add("Был создан аккаунт.", Severity.Success);

                purchases = new List<Purchase>();
            }
            else
            {
                Snackbar.Add(response.Message, Severity.Error);
            }

            registerProcessing = false;
        }

        private async Task Logout()
        {
            logoutProcessing = true;

            user = null;
            await UserService.DeleteUser();

            Snackbar.Add("Был выполнен выход из аккаунта.", Severity.Success);

            purchases = new List<Purchase>();

            logoutProcessing = false;
        }

        private async Task LoadPurchases()
        {
            isPurchasesLoading = true;

            var response = await ShopDbService.GetPurchases(user.Id);

            if (response is not null)
            {
                purchases = response.Data;
            }

            isPurchasesLoading = false;
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                user = await UserService.GetUser();

                isLoading = false;

                if (user is not null)
                {
                    await LoadPurchases();
                }                

                StateHasChanged();
            }
        }
    }
}
