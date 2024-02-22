using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class TicketApiModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubmittedBy { get; set; } // Användarnamn eller e-post
        public bool IsResolved { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
