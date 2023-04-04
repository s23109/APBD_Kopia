using System.Threading.Tasks;
using cw5.Models;

namespace cw5.Services
{
    public interface IProcedureController
    {
        Task<int> AddProductWareHouse(WarehousePost warehousePost);
    }
}