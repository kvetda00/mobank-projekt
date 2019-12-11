using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KvetonMoBank.Data;
using KvetonMoBank.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KvetonMoBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registration : ContentPage
    {
        private List<Account> accounts;
        public Registration()
        {
            InitializeComponent();
        }
        public async void CreateAccbtn_OnClicked(object sender, EventArgs e)
        {
            if (username.Text == (null) || password.Text == (null) || name.Text == (null) || surname.Text == (null))
            {
                await DisplayAlert("Registration", "Creating account has failed.\nTry again.", "OK");
            }
            else if (password.Text.Equals(passwordAgain.Text) && password.Text.Length >= 8)
            {
                var client = new Client();
                var account = new Account();
                var bankAccount = new BankAccount();
                client.Name = name.Text;
                client.Surname = surname.Text;
                account.Username = username.Text;
                account.Password = password.Text;
                if (name.Text.Equals("Petr") && surname.Text.Equals("Janiš")) account.VIP = true;
                await App.Database.SaveClientAsync(client);
                bankAccount.ClientID = client.ID;
                await App.Database.SaveBankAccountAsync(bankAccount);
                account.BankAccountID = bankAccount.ID;
                await App.Database.SaveAccountAsync(account);
                
                
                await DisplayAlert("Registration", "Account has been created. For starters we are giving you 100$. Enjoy :-)", "OK");
                await Navigation.PopToRootAsync();
                
            } 
            else await DisplayAlert("Registration", "Creating account has failed.\nTry again.", "OK");
        }
            
          
        private void password_Unfocused(object sender, FocusEventArgs e)
        {
            if (password.Text != (null) && password.Text.Length < 8) { DisplayAlert("Warning", "Invalid size of password. Should be at least 8 characters.", "OK"); }
        }

        private void passwordAgain_Unfocused(object sender, FocusEventArgs e)
        {
            if (passwordAgain.Text != (null) && (!(password.Text.Equals(passwordAgain.Text)))) { DisplayAlert("Warning", "Not equal to previous password.", "OK"); }
        }

        private async void username_Unfocused(object sender, FocusEventArgs e)
        {
            accounts = await App.Database.GetAccountsAsync();
            foreach(var element in accounts)
            {
                if (username.Text != (null) && username.Text.Equals(element.Username))
                {
                    await DisplayAlert("Warning", "This username already exists.\nTry something else.", "OK");
                }
            }
        }
    }
}