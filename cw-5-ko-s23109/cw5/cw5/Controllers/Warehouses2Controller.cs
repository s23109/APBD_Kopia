using System.Threading.Tasks;
using cw5.Models;
using cw5.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw5.Controllers
{
     [ApiController]
    [Route("api/warehouses2")]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IProcedureController _controller;

        public Warehouses2Controller(IProcedureController controller)
        {
            _controller = controller;
        }

        [HttpPost]

        public async Task<IActionResult> AddProductToWarehouse(WarehousePost product)
        {
            return Ok(await _controller.AddProductWareHouse(product));
        }



    }
}