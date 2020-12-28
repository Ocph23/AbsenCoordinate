using AbsenCoordinateMobile.Models;
using AbsenCoordinateMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbsenCoordinateMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AbsenView : ContentPage
    {
        public AbsenView()
        {
            InitializeComponent();
            BindingContext = new AbsenDataViewModel();
        }
    }

    public class AbsenDataViewModel : BaseViewModel
    {
        private Absen _selectedItem;

        public ObservableCollection<Absen> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Absen> ItemTapped { get; }

        public AbsenDataViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Absen>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Absen>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataService.GetAbsens(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Absen SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewCutiPage));
        }

        async void OnItemSelected(Absen item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CutiDetailPage)}?{nameof(CutiDetailPageViewModel.Model)}={item.Id}");
        }
    }
}