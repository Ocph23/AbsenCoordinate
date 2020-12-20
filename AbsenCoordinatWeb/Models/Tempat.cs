using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class Tempat
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public virtual ICollection<TempatDetail> Karyawans { get; set; }
    }
}
