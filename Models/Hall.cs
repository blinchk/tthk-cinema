using tthk_kinoteater.Enums;

namespace tthk_kinoteater.Models
{
    class Hall
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HallSize NumberOfPlaces { get; set; }
    }
}
