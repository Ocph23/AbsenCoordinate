using AbsenCoordinateMobile.Models;
using AbsenCoordinateMobile.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AbsenCoordinateMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AbsenPage : ContentPage
    {
        private AbsenViewModel vm;

        public AbsenPage()
        {
            InitializeComponent();
            BindingContext = vm = new AbsenViewModel(this.map);

        }
    }

    public class AbsenViewModel : BaseViewModel
    {
        public Command ViewCommand { get; }
        public Command AbsenCommand { get; }
        public TempatDetail SelectedTempat { get; private set; }

        private Xamarin.Forms.Maps.Map map;
        private CancellationTokenSource cts;
        private IEnumerable<TempatDetail> tempats;

        public AbsenViewModel(Xamarin.Forms.Maps.Map mapView)
        {
            ViewCommand = new Command(() => { Shell.Current.Navigation.PushAsync(new AbsenView()); });
            AbsenCommand = new Command(AbsenAction, CanAAbsen);
            map = mapView;
            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
            Load();
        }

        private bool CanAAbsen(object arg)
        {
            if(SelectedTempat!=null && SelectedTempat.Distance.Meters<=10)
            {
                return true;
            }

            return false;
        }

        private async void AbsenAction(object obj)
        {
            try
            {
                var result = await DataService.DoAbsen();
            }
            catch (Exception ex)
            {
                    await  Helpers.MessageShow.ErrorAsync(ex.Message);
            }
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            if (SelectedTempat != null  && e.Position.Accuracy > 50)
            {

                var m = map.Pins.Where(x => x.AutomationId == SelectedTempat.TempatId.ToString()).FirstOrDefault();
                if(m!=null)
                {
                    Distance distance4 = Distance.BetweenPositions(new Position(e.Position.Latitude, e.Position.Longitude), m.Position);
                    SelectedTempat.Distance = distance4;
                }
                AbsenCommand.ChangeCanExecute();
            }
            else
            {
                Status = "Anda Belum Memilik Tempat";
            }

        }

        private async void Load()
        {
            tempats = await DataService.GetTempatAbsen();
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);
            if (location != null)
            {
                var newMap = new MapSpan(new Position(location.Latitude, location.Longitude), 0.01, 0.01);

                foreach (var item in tempats)
                {
                    var position = new Position(item.Tempat.Latitude, item.Tempat.Longitude);
                    Distance distance4 = Distance.BetweenPositions(new Position(location.Latitude, location.Longitude), position);
                    item.Distance = distance4;
                    Pin pin = new Pin
                    {
                        Label = $"Distance {distance4.Meters} M",
                        Address = $"{item.Tempat.Nama}, {item.Tempat.Address}",
                        Type = PinType.Place,
                        AutomationId = item.TempatId.ToString(),
                        Position = position
                    };

                    pin.Clicked += Pin_Clicked;
                    map.Pins.Add(pin);
                }

                if (tempats != null && tempats.Count() >= 0)
                {
                    var item = tempats.OrderBy(x => x.Distance.Meters).FirstOrDefault();
                    newMap = new MapSpan(new Position(item.Tempat.Latitude, item.Tempat.Longitude), 0.01, 0.01);
                }

                map.MoveToRegion(newMap);

                if (!CrossGeolocator.Current.IsListening)
                   await CurrentPositon.StartListening();
            }
        }

        private void Pin_Clicked(object sender, EventArgs e)
        {
            var pin = (Pin)sender;
            SelectedTempat = tempats.Where(x => x.TempatId.ToString() == pin.AutomationId).FirstOrDefault();
            if(SelectedTempat!=null)
            {
                Status = String.Empty;
                AbsenCommand.ChangeCanExecute();
            }
        }


        private string status;
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status , value); }
        }

    }
}