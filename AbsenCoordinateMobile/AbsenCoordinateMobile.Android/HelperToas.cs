using System.Threading.Tasks;
using AbsenCoordinateMobile.Droid;
using AbsenCoordinateMobile.Helpers;
using Android.App;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(HelperToas))]
namespace AbsenCoordinateMobile.Droid
{
    public class HelperToas : IToas
    {
        public Task ShowLong(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
             return Task.CompletedTask;
        }

        public Task ShowShort(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
            return Task.CompletedTask;
        }
    }
}