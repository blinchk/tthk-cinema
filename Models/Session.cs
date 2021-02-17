using System;

namespace tthk_kinoteater.Models
{
    public class Session
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime + Movie.Duration;
        public int Duration => Convert.ToInt32(Movie.Duration.TotalMinutes);
        public string TimeString => $"{StartTime.TimeOfDay}-{EndTime.TimeOfDay}";
        public string DurationString => Duration + " min";
        public Hall Hall { get; set; }
        public int NumberOfTickets => Convert.ToInt32(Hall.NumberOfPlaces);
    }
}
