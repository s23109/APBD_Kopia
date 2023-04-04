using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IAnimalService
    {
        public List<Animal> GetAnimals(string orderBy);

        public int AddAnimal(Animal animal);

        public int DeleteAnimal(int idAnimal);

        public int PutAnimal(int idAnimal , Animal animal);

    }
}