using AbsenCoordinateMobile.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AbsenCoordinateMobile.ViewModels
{
    public class CutiDetailPageViewModel : BaseViewModel
    {
        private PersetujuanCuti model;

        public CutiDetailPageViewModel(PersetujuanCuti persetujuan)
        {
            Title = "Persetujuan";
           Model = persetujuan;
            if (Model.StatusPengajuan == StatusPengajuanCuti.Disetujui)
                ShowTanggal = true;
        }

        public PersetujuanCuti Model
        {
            get
            {
                return model;
            }
            set
            {
                SetProperty(ref model , value);
            }
        }

        private bool showTanggal;

        public bool ShowTanggal
        {
            get { return showTanggal; }
            set { SetProperty(ref showTanggal ,  value); }
        }

    }
}
