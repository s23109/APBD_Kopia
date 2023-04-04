using System;

namespace cw_7_ko_s23109.DTOs
{
    public class ClientTripDto
    {

        public int IdClient { get; set; }

        public int IdTrip { get; set; }

        public DateTime RegisteredAt { get; set; }

        public DateTime? PaymentDate { get; set; }

    }
}
