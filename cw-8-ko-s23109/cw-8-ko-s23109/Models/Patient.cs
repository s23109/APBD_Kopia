using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw_8_ko_s23109.Models
{
    public class Patient
    {
        public int IdPatient { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        public virtual IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
