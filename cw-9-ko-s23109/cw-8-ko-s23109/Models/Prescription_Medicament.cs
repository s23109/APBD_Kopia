using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw_8_ko_s23109.Models
{
    public class Prescription_Medicament
    {

        public int IdMedicament { get; set; }

        public int IdPrescription { get; set; }

        public int Dose { get; set; }

        [MaxLength(100)]
        public string Details { get; set; }

        public virtual Prescription Prescription { get; set; }

        public virtual Medicament Medicament { get; set; }
    }
}
