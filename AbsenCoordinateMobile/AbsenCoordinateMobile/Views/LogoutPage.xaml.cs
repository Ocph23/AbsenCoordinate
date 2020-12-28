using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbsenCoordinateMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();
        }

        private async void logoutClick(object sender, EventArgs e)
        {
            await Account.SetProfile(null);
            await Account.SetUser(null);
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}