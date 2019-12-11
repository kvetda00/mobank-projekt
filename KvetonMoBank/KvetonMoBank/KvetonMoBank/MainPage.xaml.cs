using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using KvetonMoBank.Data;
using KvetonMoBank.Models;
using System.IO;

namespace KvetonMoBank
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void Loginbtn_OnClicked(object sender, EventArgs e)
        {
            if (username.Text == (null) || password.Text == (null))
            {
                await DisplayAlert("Login", "Login failed, try again.", "OK");
            }
            else
            {
                bool success = false;
                List<Account> accounts = await App.Database.GetAccountsAsync();

                foreach (var element in accounts)
                {
                    if (element.Username.Equals(username.Text) && element.Password.Equals(password.Text))
                    {
                        await Navigation.PushAsync(new UserMainPage(element.ID));
                        success = true;
                        break;
                    }
                }

                if (!success) await DisplayAlert("Login", "Login failed, try again.", "OK");
            }
            

        }

        public void SignUpbtn_OnClicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new Registration());

            
        }

    }
}
