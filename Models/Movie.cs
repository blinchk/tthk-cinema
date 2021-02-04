using System;

namespace tthk_kinoteater.Models
{
    class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Year { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
