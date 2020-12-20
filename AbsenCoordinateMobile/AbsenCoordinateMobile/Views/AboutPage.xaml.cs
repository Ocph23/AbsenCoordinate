using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace AbsenCoordinateMobile.Views
{
    public partial class AboutPage : ContentPage
    {
        private CancellationTokenSource cts;

        public AboutPage()
        {
            InitializeComponent();


            LoadData();

           
        }

        private async Task LoadData()
        {

            await Task.Delay(100);
            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token); 
            if (location!=null)
            {
                var newMap = new MapSpan(new Position(location.Latitude, location.Longitude), 0.01, 0.01);

                var wiguna = new Position(-2.612755707747568, 140.6794002673815);
                Distance distance4 = Distance.BetweenPositions(new Position(location.Latitude,location.Longitude), wiguna);
                map.Pins.Add(new Pin
                {
                    Label = $"Distance {distance4.Meters} M",
                    Address = "WIGUNA ",
                    Type = PinType.Place,
                    Position = wiguna
                });

                map.MoveToRegion(newMap);

            }

        }
    }
}