using GoogleMapsComponents.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Models
{
    public class TempatView : Tempat
    {
        public TempatView() { }

        public TempatView(Tempat tempat)
        {
            this.Nama = tempat.Nama;
            this.Address = tempat.Address;
            this.Id = tempat.Id;
            this.Latitude = tempat.Latitude;
            this.Longitude = tempat.Longitude;
        }

        public Marker Marker { get; set; }

        public string Location
        {
            get
            {
                return $"{Latitude}, {Longitude}";
            }
        }
    }
}
