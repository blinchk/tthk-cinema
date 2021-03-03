using System;

namespace tthk_kinoteater.Models
{
    class Ticket
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Row { get; set; }
        public Hall Hall => Session.Hall;
        public Session Session { get; set; }
        public DateTime StartTime => Session.StartTime;
        public DateTime EndTime => Session.EndTime;
    }
}
