using AbsenCoordinateMobile.Models;
using AbsenCoordinateMobile.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AbsenCoordinateMobile.ViewModels
{
    public class CutiViewModel : BaseViewModel
    {
        private Cuti _selectedItem;

        public ObservableCollection<Cuti> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Cuti> ItemTapped { get; }

        public CutiViewModel()
        {
            Title = "PENGAJUAN CUTI";
            Items = new ObservableCollection<Cuti>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Cuti>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataService.GetCuties(true);
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

        public Cuti SelectedItem
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

        async void OnItemSelected(Cuti item)
        {
            if (item == null || item.Persetujuan==null)
                return;

            var page = new CutiDetailPage() { BindingContext = new CutiDetailPageViewModel(item.Persetujuan) };
            await Shell.Current.Navigation.PushAsync(page);
        }
    }
}