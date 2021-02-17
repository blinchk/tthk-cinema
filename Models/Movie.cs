using System;

namespace tthk_kinoteater.Models
{
    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public string YearString => Year + " a";
        public TimeSpan Duration { get; set; }
        public string DurationString => Duration.TotalMinutes + " min";
    }
}
