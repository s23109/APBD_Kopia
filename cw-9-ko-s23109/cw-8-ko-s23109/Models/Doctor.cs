using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw_8_ko_s23109.Models
{
    public class Doctor
    {

        public int IdDoctor { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public virtual IEnumerable<Prescription> Prescriptions { get; set; }
    }
}
