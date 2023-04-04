using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace WebApplication1.Models
{
    public class Animal
    {   
        [Required()]
        public int IdAnimal { get; set; }
        
        [MaxLength(200,ErrorMessage = "Parametr zbyt długi")]
        public string Name { get; set; }
        
        [MaxLength(200,ErrorMessage = "Parametr zbyt długi")]
        public string? Description { get; set; }
        
        [MaxLength(200,ErrorMessage = "Parametr zbyt długi")]
        public string Category { get; set; }
        
        [MaxLength(200,ErrorMessage = "Parametr zbyt długi")] 
        public string Area { get; set; }
        
    }
}