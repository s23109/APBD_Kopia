using System;
using System.ComponentModel.DataAnnotations;

namespace cw5.Models
{
    public class WarehousePost
    {
        [Required(ErrorMessage ="Id produktu jest wymagane")]
        public int IdProduct { get; set; }
        [Required(ErrorMessage ="Id magazynu jest wymagane")]
        public int IdWarehouse { get; set; }
        [Required(ErrorMessage ="Ilość jest wymagana")]
        [Range(1, int.MaxValue,ErrorMessage="Ilość musi być dodatnia i minimum 1")]
        public int Amount { get; set; }
        [Required(ErrorMessage ="Data jest wymagana")]
        public DateTime CreatedAt { get; set; }
    }
}