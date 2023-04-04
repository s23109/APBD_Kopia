using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using cw5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace cw5.Services
{
    
   
    public class ProcedureController : IProcedureController
    {

        private readonly IConfiguration _configuration;

        public ProcedureController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> AddProductWareHouse(WarehousePost warehousePost)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                await connection.OpenAsync();

                SqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "AddProductToWarehouse";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdProduct", warehousePost.IdProduct);
                command.Parameters.AddWithValue("@IdWarehouse", warehousePost.IdWarehouse);
                command.Parameters.AddWithValue("@Amount", warehousePost.Amount);
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                int primaryKey = await command.ExecuteNonQueryAsync();

                return primaryKey;

            }
        }
    }
}