using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvetonMoBank.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KvetonMoBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMainPage : ContentPage
    {
        private Account account;
        private BankAccount bankAccount;
        private Client client;
        private int ID_Account;
        private bool vipClicked = false;

        public UserMainPage(int ID)
        {
            InitializeComponent();
            this.ID_Account = ID;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            account = await App.Database.GetAccountAsync(ID_Account);
            bankAccount = await App.Database.GetBankAccountAsync(account.BankAccountID);
            client = await App.Database.GetClientAsync(bankAccount.ClientID);
            labelName.BindingContext = client;
        }

        private async void LoginInfoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserAccountInfo(ID_Account, vipClicked));
        }

        private async void BankAccountInfoButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserBankAccountInfo(ID_Account, vipClicked));
        }

        private async void SendMoneyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSendMoney(ID_Account, vipClicked));
        }

        private async void VIPButton_Clicked(object sender, EventArgs e)
        {
            if (account.VIP)
            {
                background.BackgroundColor = Color.Gold;
                button1.BackgroundColor = Color.Goldenrod;
                button2.BackgroundColor = Color.Goldenrod;
                button3.BackgroundColor = Color.Goldenrod;
                button4.BackgroundColor = Color.Goldenrod;
                button5.BackgroundColor = Color.Goldenrod;
                vipClicked = true;
            }
            else await DisplayAlert("VIP", "You are not a VIP :(", "OK");
            
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool x = await DisplayAlert("Delete Account", "Are you sure?", "Yes", "No");
            if (x)
            {
                await App.Database.DeleteAccountAsync(account);
                await App.Database.DeleteBankAccountAsync(bankAccount);
                await App.Database.DeleteClientAsync(client);

                await DisplayAlert("Delete Account", "Account has been deleted.", "OK");
                await Navigation.PopAsync();
            }
            
        }
    }
}