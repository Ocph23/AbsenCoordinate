using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class EnumCollection
    {
    }

    public enum StatusPengajuanCuti
    {
        None=-1, Baru, Disetujui, Ditolak=-2
    }

    public enum AbsenStatus
    {
        Hadir, Alpha, Sakit, Izin, Cuti
    }
}
