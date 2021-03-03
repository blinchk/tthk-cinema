using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class MovieList : UserControl
    {
        public MovieList(List<Movie> movies)
        {
            DisplayMovies(movies);
            InitializeComponent();
        }

        private void DisplayMovies(List<Movie> movies)
        {
            for (var i = 0; i < movies.Count; i++)
            {
                var movie = movies[i];
                var movieView = new MovieView(movie)
                {
                    Size = new Size(450, 100),
                    Location = new Point(0, 65 + 65 * i)
                };
                Controls.Add(movieView);
            }
        }
    }
}