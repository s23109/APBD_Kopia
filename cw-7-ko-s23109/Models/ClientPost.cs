using System;
using System.ComponentModel.DataAnnotations;

namespace cw_7_ko_s23109.Models
{
    public class ClientPost
    {
       // FROM BODY
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Pesel { get; set; }
        public int IdTrip { get; set; }
        public string TripName { get; set; }
        public DateTime? PaymentDate { get; set; }


    }
}
