using System;
using Microsoft.AspNetCore.Mvc;
using TravelPortal.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TravelPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
      
        /// <summary>
        /// Get details of candidates
        /// </summary>
        [HttpGet]
        public ActionResult<string> Get()
        {
            string candidateDetailsJson = "";
            Candidate candidateDetails = new Candidate
            {
                Name = "test",
                phone = "test"
            };

            //Convert object to json
            candidateDetailsJson = JValue.Parse(JsonConvert.SerializeObject(candidateDetails)).ToString(Formatting.Indented);

            //Return the candidate details
            return Ok(candidateDetailsJson);
        }

        
    }
}
