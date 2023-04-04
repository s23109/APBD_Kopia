using cw_7_ko_s23109.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw_7_ko_s23109.Controllers
{

    [Route("api/clients")]
    [ApiController]

    public class ClientsController : ControllerBase
    {

        private readonly IClientsService _service;

        public ClientsController(IClientsService service)
        {
            _service = service;
        }

        [HttpDelete("{idClient}")]
        public async Task<IActionResult> DeleteClient ([FromRoute] int idClient)
        {

            try
            {
                await _service.DeleteClient(idClient);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }


    }
}
