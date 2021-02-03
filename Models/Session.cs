using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tthk_kinoteater.Models
{
    class Session
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime + Movie.Duration;
        public int Duration => Movie.Duration.Minutes;
        public string DurationString => Duration + " min";
        public Hall Hall { get; set; }
        public int NumberOfTickets => Hall.NumberOfPlaces;
    }
}
