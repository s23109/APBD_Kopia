using cw_7_ko_s23109.Models;
using cw_7_ko_s23109.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace cw_7_ko_s23109.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _serivce;

        public TripsController (ITripsService service)
        {
            _serivce = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            return Ok(await _serivce.GetTrips());
        }


        [Route("{idTrip}/clients")]
        [HttpPost]

        public async Task<IActionResult> AddClient([FromBody]ClientPost client, [FromRoute] int idTrip)
        {
            // po co id trip w adresie jak jest w ciele metody
            try
            {
                await _serivce.AddClient(client);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


    }
}
