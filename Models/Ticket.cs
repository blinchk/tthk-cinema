using System;

namespace tthk_kinoteater.Models
{
    class Ticket
    {
        public int Id { get; set; }
        public Place Place { get; set; }
        public Hall Hall => Place.Hall;
        public Session Session { get; set; }
        public DateTime StartTime => Session.StartTime;
        public DateTime EndTime => Session.EndTime;
    }
}
