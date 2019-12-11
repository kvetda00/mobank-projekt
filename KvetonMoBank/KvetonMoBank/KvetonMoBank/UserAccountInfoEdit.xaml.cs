using KvetonMoBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KvetonMoBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAccountInfoEdit : ContentPage
    {
        private int ID_Account;
        private bool VIP;
        public UserAccountInfoEdit(int ID, bool VIP)
        {
            InitializeComponent();
            this.ID_Account = ID;
            this.VIP = VIP;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (VIP)
            {
                page.BackgroundColor = Color.Gold;
                newUsername.BackgroundColor = Color.Goldenrod;
                newPassword.BackgroundColor = Color.Goldenrod;
                newPasswordAgain.BackgroundColor = Color.Goldenrod;
                updateButton.BackgroundColor = Color.Goldenrod;
            }
        }

        public async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            if (newUsername.Text == (null) || newPassword.Text == (null) || newPasswordAgain.Text == (null))
            {
                await DisplayAlert("Edit Account", "Please fill up all entries.", "OK");
            }
            else if (newPassword.Text.Equals(newPasswordAgain.Text) && newPassword.Text.Length >= 8)
            {
                Account account = await App.Database.GetAccountAsync(ID_Account);
                account.EditAccount(newUsername.Text, newPassword.Text);
                await App.Database.SaveAccountAsync(account);
                await DisplayAlert("Edit Account", "Account has been edited.\nLogin up!", "OK");

                await Navigation.PopToRootAsync();
            }
            else await DisplayAlert("Edit Account", "Password must be at least 8 characters long.", "OK");
        }

        private async void newUsername_Unfocused(object sender, FocusEventArgs e)
        {
            List<Account> accounts = await App.Database.GetAccountsAsync();
            foreach (var element in accounts)
            {
                if (newUsername.Text != (null) && newUsername.Text.Equals(element.Username))
                {
                    await DisplayAlert("Warning", "This username already exists.\nTry something else.", "OK");
                }
            }
        }

        private async void newPasswordAgain_Unfocused(object sender, FocusEventArgs e)
        {
            if (newPasswordAgain.Text != (null) && (!(newPassword.Text.Equals(newPasswordAgain.Text))))
            {
                await DisplayAlert("Warning", "Not equal to previous password.\nTry again.", "OK");
            }
        }
    }
}