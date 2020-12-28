using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class QueryAbsenParam
    {
        public DateTime? Dari { get; set; }
        public DateTime? Sampai { get; set; }
    }


    public class AbsenReportModel
    {
        public string Name { get; set; }
        public string NIK { get; set; }
        public int Hadir { get; set; }
        public int Alpha { get; set; }
        public int Sakit { get; set; }
        public int Izin { get; set; }
        public int Cuti { get; set; }
    }
}
