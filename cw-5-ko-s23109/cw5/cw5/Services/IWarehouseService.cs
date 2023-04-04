using System;
using System.Threading.Tasks;

namespace cw5.Services
{
    public interface IWarehouseService
    {
        // 1 
        public Task<bool> DoesProductExist(int productId);
        public Task<bool> DoesWarehouseExist(int warehouseId);
        // /1
        
        //2
        public Task<int> GetTheValidOrderId(int warehouseId, int productId, int amount, DateTime createdAt);
        
        // /2
        
        //3
        public Task<bool> IsOrderCompleted(int orderId);
        
                
        // / 3
        public Task<int> CompeleteTheOrder(int orderId, int warehouseId, int productId, int amount, DateTime createdAt);
        public Task<double> GetTheProductPrice(int productId);
        
        
    }
}