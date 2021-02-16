using System;

namespace tthk_kinoteater.Models
{
    class Session
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime + Movie.Duration;
        public int Duration => Movie.Duration.Minutes;
        public string TimeString => $"{StartTime.TimeOfDay}-{EndTime.TimeOfDay}";
        public string DurationString => Duration + " min";
        public Hall Hall { get; set; }
        public int NumberOfTickets => Convert.ToInt32(Hall.NumberOfPlaces);
    }
}
