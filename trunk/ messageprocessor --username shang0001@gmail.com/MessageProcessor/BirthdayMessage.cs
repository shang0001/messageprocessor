using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProcessor
{
    public class BirthdayMessage : GenericMessage
    {
        public DateTime BirthDate { get; set; }
        public string gift { get; set; }
    }
}
