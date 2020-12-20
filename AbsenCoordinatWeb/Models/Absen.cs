using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class Absen
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Masuk { get; set; }
        public DateTime? Pulang { get; set; }
        public int KaryawanId { get; set; }

        public virtual Karyawan Karyawan { get; set; }

    }
}
