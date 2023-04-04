using cw_8_ko_s23109.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace cw_8_ko_s23109.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class PrescriptionController : ControllerBase
    {

        private readonly IPrescriptionService _service;

        public PrescriptionController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> getPrescription ([FromBody]int idPrescription)
        {

            try
            {
                return  Ok( await _service.GetPrescription(idPrescription));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }




    }
}
