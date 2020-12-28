using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinateMobile.Models
{
    public class PersetujuanCuti  
    {
        public int Id { get; set; }
        public int CutiId { get; set; }

        public DateTime? Mulai { get; set; }

        public DateTime? Berakhir { get; set; }

        public DateTime TanggalPersetujuan { get; set; }

        public StatusPengajuanCuti StatusPengajuan { get; set; }


        public string CatatanPersetujuan { get; set; }
        public int KaryawanId { get; set; }
        public virtual Karyawan Karyawan { get; set; }
        public virtual Cuti Permohonan{ get; set; }

    }
}
