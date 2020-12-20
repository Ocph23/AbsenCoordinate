using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class Cuti
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Mulai { get; set; }
        public DateTime? Berakhir { get; set; }
        public DateTime? TanggalPengajuan { get; set; }
        public StatusPengajuanCuti StatusPengajuan { get; set; }
        public int KaryawanId { get; set; }
        public virtual Karyawan Karyawan { get; set; }
        public int DisetuiOlehId { get; set; }
     
        [ForeignKey("DisetuiOlehId")]
        public virtual Karyawan DisetujuiOleh { get; set; }
    }
}
