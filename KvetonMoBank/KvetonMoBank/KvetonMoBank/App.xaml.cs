using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KvetonMoBank.Data;
using KvetonMoBank.Models;
using System.IO;

namespace KvetonMoBank
{
    public partial class App : Application
    {
        static MobankDatabase database;

        public static MobankDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MobankDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Mobank.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            
            var page = new MainPage();
            MainPage = new NavigationPage(page);
            NavigationPage.SetHasNavigationBar(page, false);
        }

        //protected override async void OnStart()
        //{
        //    // Handle when your app starts
        //    Account a = new Account();
        //    BankAccount b = new BankAccount();
        //    Client c = new Client();
        //    a.Username = "test";
        //    a.Password = "test";
        //    c.Name = "David";
        //    c.Surname = "Květoň";
        //    await App.Database.SaveClientAsync(c);
        //    b.ClientID = c.ID;
        //    await App.Database.SaveBankAccountAsync(b);
        //    a.BankAccountID = b.ID;
        //    await App.Database.SaveAccountAsync(a);

        //}

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
