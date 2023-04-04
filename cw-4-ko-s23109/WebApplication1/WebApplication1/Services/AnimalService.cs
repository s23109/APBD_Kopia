using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class AnimalService : IAnimalService
    {

        private readonly IConfiguration _configuration;

        public AnimalService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        

        public List<Animal> GetAnimals(string orderBy = "name")
        {
            var return_list = new List<Animal>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = $"select * from animal order by @1";
                    command.Parameters.AddWithValue("@1", orderBy);
                    connection.Open();

                    var dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        return_list.Add(new Animal
                        {
                            IdAnimal = int.Parse(dataReader["idAnimal"].ToString()),
                            Name = dataReader["name"].ToString(),
                            Description = dataReader["description"].ToString(),
                            Category = dataReader["category"].ToString(),
                            Area = dataReader["area"].ToString()
                        });
                    }
                }
            }

            return return_list;
        }

        public int AddAnimal(Animal animal)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using (var command = new SqlCommand()){
                command.Connection = connection;
                command.CommandText = $"insert into animal (idanimal, name, description, category, area) values (@1, @2, @3, @4, @5)";
                command.Parameters.AddWithValue("@1",animal.IdAnimal);
                command.Parameters.AddWithValue("@2",animal.Name);
                command.Parameters.AddWithValue("@3",animal.Description);
                command.Parameters.AddWithValue("@4",animal.Category);
                command.Parameters.AddWithValue("@5",animal.Area);
                connection.Open();
                return command.ExecuteNonQuery();
            }
            
        }

        public int DeleteAnimal(int idAnimal)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = $"Delete from animal where idanimal = @1";
                command.Parameters.AddWithValue("@1", idAnimal);
                connection.Open();
                return command.ExecuteNonQuery();

            }
        }

        public int PutAnimal(int idAnimal , Animal animal)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText =
                    $"Update Animal set  (idanimal, name, description, category, area) values (@1, @2, @3, @4, @5)";
                command.Parameters.AddWithValue("@1",animal.IdAnimal);
                command.Parameters.AddWithValue("@2",animal.Name);
                command.Parameters.AddWithValue("@3",animal.Description);
                command.Parameters.AddWithValue("@4",animal.Category);
                command.Parameters.AddWithValue("@5",animal.Area);
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}