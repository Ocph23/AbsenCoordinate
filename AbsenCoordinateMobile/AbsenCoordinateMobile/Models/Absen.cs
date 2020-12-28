using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinateMobile.Models
{
    public class Absen
    {
        public int Id { get; set; }
        public DateTime? Masuk { get; set; }
        public DateTime? Pulang { get; set; }
        public int KaryawanId { get; set; }

        public virtual Karyawan Karyawan { get; set; }

    }
}
