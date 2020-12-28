using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinateMobile.Models
{
    public class Karyawan
    {
        public int Id { get; set; }

        public string NIK { get; set; }

        public string Name { get; set; }

        public string UnitKerja { get; set; }

        public string LokasiKerja { get; set; }

        public string Jabatan { get; set; }

        public string Alamat { get; set; }

        public string Email { get;  set; }

        public string UserId { get; set; }

        public bool IsHRD { get; set; }
    }
}
