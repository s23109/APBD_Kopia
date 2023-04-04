

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw_8_ko_s23109.Models
{
    public class Medicament
    {
        public int IdMedicament { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        public virtual IEnumerable<Prescription_Medicament> Prescription_Medicament { get; set; }
    }
}
