﻿using System;
using System.Collections.Generic;

namespace cw_7_ko_s23109.DTOs
{
    public class TripDto
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int MaxPeople{ get; set; }

        public List<Country> Countries { get; set; }

        public List<Client> Clients { get; set; }


    }
}
