﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class TicketTag
    {
        public int TicketId { get; set; }
        public TicketModel Ticket { get; set; }
        public int TagId { get; set; }
        public TagModel Tag { get; set; } // Sparas som ett heltal (int) i databasen(enumens value), går sedan att omvandla till det motsvarande namnet
    }
}
