using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tthk_kinoteater.Models
{
    class Ticket
    {
        public int Id { get; set; }
        public Place Place { get; set; }
        public Hall Hall => Place.Hall;
        public Session Session { get; set; }
        public DateTime Time => Session.Time;
    }
}
