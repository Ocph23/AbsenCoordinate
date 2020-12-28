using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class Karyawan
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "NIK Tidak Boleh Kosong")]
        public string NIK { get; set; }

        [Required(ErrorMessage ="Nama Tidak Boleh Kosong")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Unit Kerja Tidak Boleh Kosong")]
        public string UnitKerja { get; set; }

        [Required(ErrorMessage = "Lokasi Tidak Boleh Kosong")]
        public string LokasiKerja { get; set; }

        [Required(ErrorMessage = "Jabatan Tidak Boleh Kosong")]
        public string Jabatan { get; set; }

        [Required(ErrorMessage = "Alamat Tidak Boleh Kosong")]
        public string Alamat { get; set; }

        [Required(ErrorMessage = "Email Tidak Boleh Kosong")]
        public string Email { get;  set; }

        public string UserId { get; set; }

        [NotMapped]
        public bool IsHRD { get; set; }
    }
}
