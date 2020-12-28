using AbsenCoordinateMobile.Services;
using AbsenCoordinateMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbsenCoordinateMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<DataServices>();
            DependencyService.Register<MyCurrentPosition>();
            Load();
        }

        private  void Load()
        {

            MainPage = new Views.LoginPage();

            if (Account.UserIsLogin)
            {
                    MainPage = new AppShell();
            }
            else
            {
                MainPage = new Views.LoginPage();
            }
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
