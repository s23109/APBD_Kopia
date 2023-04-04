using System;
using System.Threading.Tasks;
using cw5.Models;
using cw5.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw5.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehousesController(IWarehouseService service)
        {
            _service = service;
        }


        [HttpPost]

        public async Task<IActionResult> AddProductToWarehouse(WarehousePost product)
        {
            
            
            if (!await _service.DoesProductExist(product.IdProduct))
            {
                return NotFound("Nie ma takiego produktu");
            }

            if (!await _service.DoesWarehouseExist(product.IdWarehouse))
            {
                return NotFound("Nie ma takiego magazynu");
            }
            // / 1

            int id_order = await _service.GetTheValidOrderId(product.IdWarehouse, product.IdProduct, product.Amount, product.CreatedAt);

            if (id_order == -1)
            {
                return NotFound("Zamówienie nie istnieje w order");
            }
            
            // /2 
            
            if (await _service.IsOrderCompleted(id_order))
            {
                return BadRequest("Zamówienie zostało już wykonane");
            }

            int primaryKey;

            try
            {
                primaryKey = await _service.CompeleteTheOrder(id_order,product.IdWarehouse, product.IdProduct,product.Amount,product.CreatedAt);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(primaryKey);
        }
        
        
    }
}