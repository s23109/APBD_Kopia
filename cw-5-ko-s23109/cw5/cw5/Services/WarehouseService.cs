using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace cw5.Services
{
    public class WarehouseService : IWarehouseService
    {

        private readonly IConfiguration _configuration;

        public WarehouseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<bool> DoesProductExist(int productId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using(var command = new SqlCommand()){

                command.Connection = connection;
                command.CommandText = "select IdProduct from Product where IdProduct = @1";
                command.Parameters.AddWithValue("@1", productId);

                await connection.OpenAsync();

                var found_products = double.Parse((await command.ExecuteScalarAsync()).ToString());

                return found_products == 0;
            }
        }

        public async Task<bool> DoesWarehouseExist(int warehouseId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using(var command = new SqlCommand()){

                command.Connection = connection;
                command.CommandText = "select IdWarehouse from Warehouse where IdWarehouse = @1";
                command.Parameters.AddWithValue("@1", warehouseId);

                await connection.OpenAsync();

                var found_warehouses = double.Parse((await command.ExecuteScalarAsync()).ToString());

                return found_warehouses == 0;
            }
        }
        

        public async Task<double> GetTheProductPrice(int productId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using(var command = new SqlCommand()){

                command.Connection = connection;
                command.CommandText = "select Price from Product where IdProduct = @1";
                command.Parameters.AddWithValue("@1", productId);

                await connection.OpenAsync();

                var productPrice = double.Parse((await command.ExecuteScalarAsync()).ToString());

                return productPrice;
            }
        }

        public async Task<bool> IsOrderCompleted(int orderId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using(var command = new SqlCommand()){

                command.Connection = connection;
                command.CommandText = "select IsNull(FulfilledAt,'') from Order where IdOrder = @1";
                command.Parameters.AddWithValue("@1", orderId);

                await connection.OpenAsync();

                var end_date = (await command.ExecuteScalarAsync()).ToString();

                return end_date.Length>0;
            }
        }
        public async Task<int> GetTheValidOrderId(int warehouseId, int productId, int amount, DateTime createdAt)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using(var command = new SqlCommand()){
                
                command.Connection = connection;
                command.CommandText = "select Top 1 IdOrder from Product_Warehouse where IdWarehouse = @1 AND IdProduct = @2 AND Amount= @3 where CreatedAt < @4 ";
                command.Parameters.AddWithValue("@1", warehouseId);
                command.Parameters.AddWithValue("@2", productId);
                command.Parameters.AddWithValue("@3", amount);
                command.Parameters.AddWithValue("@4", createdAt);
                await connection.OpenAsync();
                
                var productPrice = await command.ExecuteReaderAsync();

                if (!productPrice.HasRows)
                {
                    return -1;
                }
                
                return Int32.Parse(productPrice["IdOrder"].ToString());
                
            }
        }

        public async Task<int> CompeleteTheOrder(int orderId, int warehouseId, int productId, int amount, DateTime createdAt)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                using (var command = new SqlCommand())
                {

                    await connection.OpenAsync();
                    var transaction = await connection.BeginTransactionAsync() as SqlTransaction;

                    try
                    { 
                    command.Connection = connection;
                    command.Transaction = transaction;
                    
                    
                    //sprawdź czy nie ma już wiersza z danym idorder
                    command.CommandText = "select 1 from Product_Warehouse  where IdOrder = @1";
                    command.Parameters.AddWithValue("@1", orderId);
        
                    var dataReader = await command.ExecuteReaderAsync();

                    
                    if (dataReader.HasRows)
                    {
                        throw new Exception("Błędne IdOrder");
                    }
                    command.Parameters.Clear();

                    command.CommandText = "UPDATE [ORDER] Set FulfilledAt = @1 where IdOrder = @2";
                    command.Parameters.AddWithValue("@1", DateTime.Now);
                    command.Parameters.AddWithValue("@2", orderId);

                    int updated = await command.ExecuteNonQueryAsync();

                    if (updated < 1)
                    {
                        throw new Exception("Błąd podczas aktualizacji, nie zaaktualizowano tabeli ORDER");
                    }

                    command.Parameters.Clear();
                    command.CommandText = "Insert Into Product_Warehouse (IdWarehouse,IdProduct,IdOrder,Amount,Price,CreatedAt) "+
                                          "values (@IdWarehouse,@IdProduct,@IdOrder,@Amount,@Price,@CreatedAt) ";
                    
                    command.Parameters.AddWithValue("@IdWarehouse", warehouseId);
                    command.Parameters.AddWithValue("@IdProduct", productId);
                    command.Parameters.AddWithValue("@IdOrder", orderId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Price", amount*(await GetTheProductPrice(productId)));
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    

                    int rowsInserted = await command.ExecuteNonQueryAsync();

                    if (rowsInserted < 1)
                    {
                        throw new Exception("Błąd przy wstawianiu do Product_Warehouse, nie wstawiono danych");
                    }
                    
                    command.Parameters.Clear();

                    command.CommandText = "Select IdProductWarehouse From Product_Warehouse where IdOrder = @IdOrder";
                    command.Parameters.AddWithValue("@IdOrder", orderId);

                    int id = await command.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();

                    return id;



                    }
                    catch (Exception e)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception(e.ToString());
                    }
                    
                    


                }
            }

            
        }
        
    }
}