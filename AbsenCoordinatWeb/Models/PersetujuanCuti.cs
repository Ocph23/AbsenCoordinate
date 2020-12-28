using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class PersetujuanCuti  
    {
        [Key]
        public int Id { get; set; }
        public int CutiId { get; set; }

        [Required]
        public DateTime? Mulai { get; set; }

        [Required]
        public DateTime? Berakhir { get; set; }

        [Required]
        public DateTime TanggalPersetujuan { get; set; }

        public StatusPengajuanCuti StatusPengajuan { get; set; }


        [Required]
        public string CatatanPersetujuan { get; set; }
        public int KaryawanId { get; set; }
        public virtual Karyawan Karyawan { get; set; }
        public virtual Cuti Permohonan{ get; set; }

    }
}
