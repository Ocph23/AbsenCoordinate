using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class TempatDetail
    {

        public int Id { get; set; }
        public int TempatId { get; set; }
        public int KaryawanId { get; set; }
        public virtual Karyawan Karyawan { get; set; }
        public virtual Tempat Tempat { get; set; }

    }
}
