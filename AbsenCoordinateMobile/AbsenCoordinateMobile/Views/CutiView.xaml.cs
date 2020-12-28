using AbsenCoordinateMobile.Models;
using AbsenCoordinateMobile.ViewModels;
using AbsenCoordinateMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbsenCoordinateMobile.Views
{
    public partial class CutiView : ContentPage
    {
        readonly CutiViewModel _viewModel;

        public CutiView()
        {
            InitializeComponent();

            BindingContext = _viewModel = new CutiViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}