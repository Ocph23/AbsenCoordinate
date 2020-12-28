using AbsenCoordinateMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AbsenCoordinateMobile.ViewModels
{
    public class NewCutiPageViewModel : BaseViewModel
    {
        private Cuti model;

        public NewCutiPageViewModel()
        {
            Model = new Cuti() { Mulai=DateTime.Now, Berakhir=DateTime.Now, TanggalPengajuan=DateTime.Now };
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            Model.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            MessageLine = string.Empty;
            if (String.IsNullOrWhiteSpace(Model.AlasanCuti)
                 || Model.Mulai == null || Model.Berakhir == null)
            {
                MessageLine = "Lengkapi Data !";
                return false;
            }

            if(DateTime.Now.AddDays(7) > Model.Mulai)
            {
                MessageLine = "Pengajuan Cuti Paling Lambat 7 Hari Sebelum Mulai Cuti";
                return false;
            }

            if(Model.Mulai >= Model.Berakhir)
            {
                MessageLine = "Mulai Cuti Harus Lebih Kecil Dari Berakhir Cuti";
                return false;
            }

            return true;

        }


        private string messageLine;

        public string MessageLine
        {
            get { return messageLine; }
            set {SetProperty(ref messageLine , value); }
        }




        public Cuti Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

       

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            try
            {
                var profile = await Account.GetProfile();
                Model.KaryawanId = profile.Id;
                Model.TanggalPengajuan = DateTime.Now;
                await DataService.AddCutiAsync(Model);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
              await  Helpers.MessageShow.ErrorAsync(ex.Message);
            }
        }
    }
}
