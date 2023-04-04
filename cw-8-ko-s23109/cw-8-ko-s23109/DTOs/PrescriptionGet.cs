using cw_8_ko_s23109.Models;
using System;
using System.Collections.Generic;

namespace cw_8_ko_s23109.DTOs
{
    public class PrescriptionGet
    {

        public int IdPrescription { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }  

        public List<Medicament> Medicaments { get; set; }

    }
}
