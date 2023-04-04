
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;


namespace WebApplication1.Controllers
{
    
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _service;

        public AnimalController(IAnimalService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAnimals(string orderBy = "name")
        {
            var result = _service.GetAnimals(orderBy);
            return Ok(result);  // ??? 
        }

        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            _service.AddAnimal(animal);
            return Created("" , "");
        }

        [HttpPut("{id}")]
        public IActionResult PutAnimal(int idAnimal, Animal animal)
        {
            //put = update existing
            if (idAnimal != animal.IdAnimal)
            {
                return BadRequest();
            }

            if (idAnimal == animal.IdAnimal)
            {
                _service.PutAnimal(idAnimal, animal);
                return Ok("Updateowano");
            }

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int idAnimal)
        {
            _service.DeleteAnimal(idAnimal);
            return Ok();
        }
        


    }
}