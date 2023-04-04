using System;
using System.Collections.Generic;

namespace cw_8_ko_s23109.Models
{
    public class Prescription
    {
        public int IdPrescription { get; set; }

        public DateTime Date { get; set; }

        public DateTime Duedate { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual IEnumerable<Prescription_Medicament> Prescription_Medicament { get; set; }
    }
}
