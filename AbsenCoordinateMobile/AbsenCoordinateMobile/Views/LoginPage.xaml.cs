using AbsenCoordinateMobile.Helpers;
using AbsenCoordinateMobile.ViewModels;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }

    public class LoginViewModel : BaseViewModel
    {
        #region Constructor
        public LoginViewModel()
        {
            LoginCommand = new Command(LoginAction, CanLogin);
            this.PropertyChanged +=
             (_, __) => LoginCommand.ChangeCanExecute();
        }
        #endregion

        #region Fields
        private string url;
        private string userName;
        private string _password;
        private Command _loginCommand;
        #endregion

        #region Properties
        public string Url
        {
            get
            {
                if (string.IsNullOrEmpty(url))
                    url = Helper.Url;
                return url;
            }
            set
            {
                SetProperty(ref url, value);
                Helper.Url = Url;

            }
        }

        public string UserName
        {
            get { return userName; }
            set
            {
                SetProperty(ref userName, value);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
               
            }

        }

        public Command LoginCommand
        {
            get => _loginCommand;
            set => SetProperty(ref _loginCommand, value);
        }
        #endregion

        #region Methods
        private bool CanLogin(object arg)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                return false;
            return true;
        }

        private async void LoginAction(object obj)
        {
            try
            {
                var user = new UserLogin() { UserName = UserName, Password = Password };
                var result = await DataService.Login(user);
                if (Account.UserIsLogin)
                {
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    throw new SystemException("You Not Have Access !");
                }
            }
            catch (Exception ex)
            {
               await MessageShow.ErrorAsync(ex.Message);
            }
            finally
            {
                Helper.Url = Url;
            }
        }
        #endregion
    }
}