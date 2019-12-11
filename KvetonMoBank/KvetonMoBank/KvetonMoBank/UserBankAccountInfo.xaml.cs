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
    public partial class UserBankAccountInfo : ContentPage
    {
        private int ID_Account;
        private bool VIP;
        public UserBankAccountInfo(int ID, bool VIP)
        {
            InitializeComponent();
            this.ID_Account = ID;
            this.VIP = VIP;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (VIP)
            {
                page.BackgroundColor = Color.Gold;
            }

            Account account = await App.Database.GetAccountAsync(ID_Account);
            BankAccount bankAccount = await App.Database.GetBankAccountAsync(account.BankAccountID);
            Client client = await App.Database.GetClientAsync(bankAccount.ClientID);
            labelName.BindingContext = client;
            labelSurname.BindingContext = client;
            labelAccountNumber.BindingContext = bankAccount;
            labelBalance.BindingContext = bankAccount;
            labelCreateDate.BindingContext = account;
            labelBalance.Text += " $";
        }
    }
}