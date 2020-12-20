using AbsenCoordinateMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AbsenCoordinateMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}