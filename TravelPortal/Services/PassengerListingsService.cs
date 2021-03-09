using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TravelPortal.Models;
using TravelPortal.Repository.IRepository;

namespace TravelPortal.Repository.IRepository
{
    public class PassengerListingsService : Service<string>, IPassengerListingsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private PassengerListings tripListings;

        public PassengerListingsService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public bool AddListings(string json)
        {
            bool result = false;

            //Add data
            tripListings = JsonConvert.DeserializeObject<PassengerListings>(json);
            result = true;

            return result;
        }

        /*
         * Function to return the listings which support the given number of passengers 
         */
        public string GetListingsForNumberOfPassengers(int numberOfPassengers)
        {
            if (numberOfPassengers <= 0)
            {
                //Number of passengers should be greater than zero
                return ("The number of passengers should be greater than zero");
            }
            else
            {
                List<DetailedListingsWithPrice> resultListings = new List<DetailedListingsWithPrice>();

                //Parse through all the listings
                foreach (DetailedListings vehicleDetails in tripListings.listings)
                {
                    //Filter the listings which support the given number of passengers 
                    if (vehicleDetails.vehicleType.maxPassengers >= numberOfPassengers)
                    {
                        //Calculate the total price for the given list of passenegers
                        decimal calculatedPrice = numberOfPassengers * vehicleDetails.pricePerPassenger;

                        
                        DetailedListingsWithPrice listingWithPrice = new DetailedListingsWithPrice(vehicleDetails);
                        listingWithPrice.totalPrice = calculatedPrice;

                        //Generate the output list with total price
                        resultListings.Add(listingWithPrice);
                    }
                }

                if (resultListings.Count > 0)
                {
                    //Sort the list based on total price in ascending order
                    List<DetailedListingsWithPrice> SortedList = resultListings.OrderBy(o => o.totalPrice).ToList();

                    //Convert object to json
                    var listingsJson = JValue.Parse(JsonConvert.SerializeObject(SortedList)).ToString(Formatting.Indented);

                    //Return the json string
                    return listingsJson.ToString();
                }
                else
                {
                    //Number of passengers should be greater than zero
                    return ("No vehicle is available for this number of passengers");
                }
            }

        }
    }
}
