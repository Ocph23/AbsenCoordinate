using AbsenCoordinateMobile.ViewModels;
using System;

namespace AbsenCoordinateMobile.Models
{
    public class Cuti :BaseNotify
    {
        private int _id;
        private DateTime? _mulai;
        private DateTime? _berakhir;
        private DateTime? _tglPengajuan;
        private string _alasan;
        private int _karyawanid;
        private Karyawan _karyawan;
        private PersetujuanCuti _persetujuan;

        public int Id { get =>  _id; set => SetProperty(ref _id, value); }

        public DateTime? Mulai { get =>  _mulai; set => SetProperty(ref _mulai, value); }

        public DateTime? Berakhir { get => _berakhir; set => SetProperty(ref _berakhir, value); }

        public DateTime? TanggalPengajuan { get =>  _tglPengajuan; set => SetProperty(ref _tglPengajuan, value); }

        public string AlasanCuti { get =>  _alasan; set => SetProperty(ref _alasan, value); }

        public int KaryawanId { get =>  _karyawanid; set => SetProperty(ref _karyawanid, value); }

        public virtual Karyawan Karyawan { get =>  _karyawan; set => SetProperty(ref _karyawan, value); }

        public virtual PersetujuanCuti Persetujuan { get =>  _persetujuan; set => SetProperty(ref _persetujuan, value); }

        public StatusPengajuanCuti Status
        {
            get
            {
                if (Persetujuan == null)
                    return StatusPengajuanCuti.Baru;
                return Persetujuan.StatusPengajuan;
            }
        }
    }
}
