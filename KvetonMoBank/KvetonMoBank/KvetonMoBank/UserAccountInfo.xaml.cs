using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvetonMoBank.Models;
using KvetonMoBank.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KvetonMoBank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserAccountInfo : ContentPage
    {
        private int ID_Account;
        private bool VIP;
        

        public UserAccountInfo(int ID, bool VIP)
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
                editButton.BackgroundColor = Color.Goldenrod;
            }

            Account account = await App.Database.GetAccountAsync(ID_Account);
            labelUsername.BindingContext = account;
            labelPassword.BindingContext = account;
        }

        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserAccountInfoEdit(ID_Account, VIP));
        }
    }
}