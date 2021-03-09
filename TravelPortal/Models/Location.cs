using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPortal.Models
{
    public class Location
    {
        public string city { get; set; } //City corresponding to caller's IP address
    }

    //Configuration for access key
    public class LocationConfiguration
    {
        public string key { get; set; }
    }
}
