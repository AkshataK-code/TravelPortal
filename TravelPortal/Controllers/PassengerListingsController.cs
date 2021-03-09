using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPortal.Repository.IRepository;

namespace TravelPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengerListingsController : ControllerBase
    {        
        private readonly IPassengerListingsService _passengerListings;

        public PassengerListingsController(IPassengerListingsService listings)
        {
            _passengerListings = listings;//Initialise listings
        }

        /// <summary>
        /// Get passenger listings
        /// </summary>
        [HttpGet("{numberOfPassengers:int}")]
        public async Task<ActionResult> Get(int numberOfPassengers)
        {
            string passengerListingsJson = "";

            //URL for getting passenger listings
            string path = "https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest";

            string parameters = "";//No parameters

            //Get all the listings from the endpoint
            passengerListingsJson = await _passengerListings.GetAsync(path, parameters);

            if(!String.IsNullOrEmpty(passengerListingsJson))
            {
                //Add the data
                _passengerListings.AddListings(passengerListingsJson);

                //Get the relevant listings which support the number of passengers provided and return the json data
                return Ok(_passengerListings.GetListingsForNumberOfPassengers(numberOfPassengers));
            }
            else
            {
                return NoContent();
            }
            
        }
    }
}
