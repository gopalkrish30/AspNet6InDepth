using configurations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace configurations.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Auth0Details _auth0Details_Monitor;
        private readonly Auth0Details _auth0Details_Snapshot;
        private readonly Auth0Details _auth0Details;

        public ValuesController(IConfiguration configuration, IOptions<Auth0Details> Auth0Details, IOptionsSnapshot<Auth0Details> Auth0Details_Snapshot, IOptionsMonitor<Auth0Details> Auth0Details_Monitor)
        {
            _configuration = configuration;
            _auth0Details_Monitor = Auth0Details_Monitor.CurrentValue;
            _auth0Details_Snapshot = Auth0Details_Snapshot.Value;
            _auth0Details = Auth0Details.Value;
        }


        [HttpGet]
        public IActionResult GetConfig()
        {
            //weekly typed-- always return in string
            string retryCount = _configuration["Auth0:RetryCount"];

            //utility to covert to type
            int retryCount_converted = _configuration.GetValue<int>("Auth0:RetryCount");


            var autho_sec = _configuration.GetSection("Auth0");

            foreach ( var item in autho_sec.GetChildren())
            {
                Console.WriteLine(item.Key + " : " + item.Value);   
            }

            return Ok(new
            {
                retryCount_stronglytyped = retryCount,
                retryCount_converted = retryCount_converted,
                Ioptions_value = _auth0Details.RetryCount,
                IoptionsSnapshot_Value = _auth0Details_Snapshot.RetryCount,
                IoptionsMontior_value = _auth0Details_Monitor.RetryCount,
                autho_sec = new
                {
                    clientid = _configuration.GetSection("Auth0")["ClientId"],
                    clientSecret = _configuration.GetSection("Auth0")["ClientSecret"],
                    RetryCount = _configuration.GetSection("Auth0")["RetryCount"]
                }
            });
        }

    }
}
