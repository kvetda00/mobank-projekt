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
    public partial class UserSendMoney : ContentPage
    {
        private int ID_Account;
        private bool VIP;
        private Account account;
        private BankAccount bankAccount;
        private List<BankAccount> bankAccounts;
        public UserSendMoney(int ID, bool VIP)
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
                pickerBankAccounts.BackgroundColor = Color.Goldenrod;
                entryAmount.BackgroundColor = Color.Goldenrod;
                sendButton.BackgroundColor = Color.Goldenrod;
            }

            account = await App.Database.GetAccountAsync(ID_Account);
            bankAccount = await App.Database.GetBankAccountAsync(account.BankAccountID);
            bankAccounts = await App.Database.GetBankAccountsAsync();
            
            foreach (var element in bankAccounts)
            {
                if (element != (null) && element.ID != bankAccount.ID)
                {
                   pickerBankAccounts.Items.Add(element.AccountNumber);
                }
            }
        }

        private async void SendMoneyButton_Clicked(object sender, EventArgs e)
        {   
            decimal amount = 0;
            bool success = false;
            if(pickerBankAccounts.SelectedItem == (null))
            {
                await DisplayAlert("Send", "Please choose account number.", "OK");
            }
            else if (Decimal.TryParse(entryAmount.Text, out amount) && amount > 0){
                bool answer = await DisplayAlert("Send", "Are you sure?", "Yes", "No");
                if (answer)
                {
                    if (!(bankAccount.Withdrawal(amount))) await DisplayAlert("Send", "Not enough honey to send this amount of money ;)","OK");
                    else
                    {
                        foreach (var element in bankAccounts)
                        {
                            if (element.AccountNumber.Equals(pickerBankAccounts.SelectedItem))
                            {
                                element.Deposit(amount);
                                await App.Database.SaveBankAccountAsync(element);
                                await App.Database.SaveBankAccountAsync(bankAccount);
                                success = true;
                            }
                        }
                    }
                }
            } else await DisplayAlert("Send", "Invalid type of amount.\nTry again.", "OK");
            if (success)
            {
                await DisplayAlert("Send", "Payment was successful.", "OK");       
            }

            
        }
    }
}