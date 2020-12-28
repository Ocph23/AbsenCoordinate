using AbsenCoordinateMobile.Models;
using AbsenCoordinateMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AbsenCoordinateMobile.Views
{
    public partial class NewCutiPage : ContentPage
    {
        public Item Item { get; set; }

        public NewCutiPage()
        {
            InitializeComponent();
            BindingContext = new NewCutiPageViewModel();
        }
    }
}