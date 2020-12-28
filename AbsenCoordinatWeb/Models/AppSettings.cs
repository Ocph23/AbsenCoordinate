using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{


    public class AppSettings
    {
        public string Secret { get; set; }
        public string PasswordHashKey { get; set; }
       public AbsenSetting AbsenSetting { get; set; }
    }


    public class AbsenSetting
    {
        public TimeSpan Masuk { get; set; }
        public TimeSpan Pulang { get; set; }
        public TimeSpan KonpensasiMasuk{ get; set; }
        public TimeSpan KonpensasiPulang { get; set; }
        public bool Konpensasi { get; set; }
    }
}
