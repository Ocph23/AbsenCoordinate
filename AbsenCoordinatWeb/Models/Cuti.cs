using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class Cuti  : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime? Mulai { get; set; }

        [Required]
        public DateTime? Berakhir { get; set; }

        [Required]
        public DateTime? TanggalPengajuan { get; set; }
       

        [Required]
        public string AlasanCuti { get; set; }

        public int KaryawanId { get; set; }
        public virtual Karyawan Karyawan { get; set; }
        public virtual PersetujuanCuti Persetujuan { get; set; }


        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Berakhir <= Mulai)
            {
                yield return new ValidationResult(
                    errorMessage: "Tanggal Mulai Cuti Harus Lebih Kecil",
                    memberNames: new[] { "Berakhir" }
               );
            }

            if(Mulai < TanggalPengajuan.Value.AddDays(7))
            {
                yield return new ValidationResult(
                   errorMessage: "Tanggal Pengajuan Mulai Cuti Paling Lambat 7 Hari",
                   memberNames: new[] { "Mulai" }
              );
            }
        }
    }
}
