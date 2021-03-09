using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TravelPortal.Models
{
    public class PassengerListings
    {
        public string from { get; set; } // From location

        public string to { get; set; }  // To location

        public List<DetailedListings> listings { get; set; } 
    }


    public class DetailedListings
    {
        public string name { get; set; } // name

        public decimal pricePerPassenger { get; set; } //Price Per Passenger

        public VehicleType vehicleType { get; set; } // Vehicle type

    }

    public struct VehicleType
    {
         public string name { get; set; }

         public int maxPassengers { get; set; }    
    }

    public class DetailedListingsWithPrice : DetailedListings
    {
        public decimal totalPrice { get; set; }
        public DetailedListingsWithPrice(DetailedListings details)
        {
            name = details.name;
            pricePerPassenger = details.pricePerPassenger;

            VehicleType typeOfVehicle = new VehicleType();  

            typeOfVehicle.name = details.vehicleType.name;  // Name
            typeOfVehicle.maxPassengers = details.vehicleType.maxPassengers; //Max passengers

            vehicleType = typeOfVehicle; 

        }
        

    }
}
