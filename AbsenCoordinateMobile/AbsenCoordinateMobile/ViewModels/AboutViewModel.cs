using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AbsenCoordinateMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
           // StartListening();
        }

        public ICommand OpenWebCommand { get; }

        CancellationTokenSource cts;

        async Task StartListening()
        {

            if (CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);
            CrossGeolocator.Current.PositionChanged += PositionChanged;
            CrossGeolocator.Current.PositionError += PositionError;
        }

        private void PositionChanged(object sender, PositionEventArgs e)
        {
            var position = e.Position;
            var output = "Full: Lat: " + position.Latitude + " Long: " + position.Longitude;
            output += "\n" + $"Time: {position.Timestamp}";
            output += "\n" + $"Heading: {position.Heading}";
            output += "\n" + $"Speed: {position.Speed}";
            output += "\n" + $"Accuracy: {position.Accuracy}";
            output += "\n" + $"Altitude: {position.Altitude}";
            output += "\n" + $"Altitude Accuracy: {position.AltitudeAccuracy}";
            CurrentLocation = output;
            Debug.WriteLine(output);
        }

        private void PositionError(object sender, PositionErrorEventArgs e)
        {
            ErrorMessage = e.Error.ToString();
        }

        async Task StopListening()
        {
            if (!CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StopListeningAsync();

            CrossGeolocator.Current.PositionChanged -= PositionChanged;
            CrossGeolocator.Current.PositionError -= PositionError;
        }


        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await GetCurrentLocation(e.Position);               
            });
        }

        async Task GetCurrentLocation(Plugin.Geolocator.Abstractions.Position position)
        {
            try
            {
              //  var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                if (position != null)
                {
                    Location sanFrancisco = new Location(-2.612755707747568, 140.6794002673815);
                    double km = Location.CalculateDistance(new Location(position.Latitude,position.Longitude), sanFrancisco, DistanceUnits.Kilometers);
                    CurrentLocation = $"Latitude: {position.Latitude}, Longitude: {position.Longitude}, Altitude: {position.Altitude}, Distance : {km*1000} M";
                }

               await Task.CompletedTask;
            }
            catch (FeatureNotSupportedException ex)
            {
                // Handle not supported on device exception
                Console.WriteLine(ex.Message);
            }
            catch (FeatureNotEnabledException ex)
            {
                // Handle not enabled on device exception
                Console.WriteLine(ex.Message);
            }
            catch (PermissionException ex)
            {
                // Handle permission exception
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine(ex.Message);
            }
        }

     

        private string _current;
        private string _error;

        public string CurrentLocation
        {
            get { return _current; }
            set
            {
               SetProperty(ref _current , value);
            }
        }


        public string ErrorMessage
        {
            get { return _error; }
            set
            {
                SetProperty(ref _error, value);
            }
        }
    }
}