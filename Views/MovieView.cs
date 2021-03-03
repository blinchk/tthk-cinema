using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class MovieView : UserControl
    {
        public MovieView(Movie movie)
        {
            var controls = InitializeControls(movie);
            Controls.AddRange(controls);
            InitializeComponent();
        }

        private Control[] InitializeControls(Movie movie)
        {
            var movieTitleLabel = new Label
            {
                Text = movie.Title,
                Width = 300,
                Location = new Point(50, 50),
                Height = 20,
                Font = new Font(FontFamily.GenericSansSerif, 10)
            };
            var movieDirectorLabel = new Label
            {
                Text = movie.Director,
                Height = 15,
                Location = movieTitleLabel.Location
            };
            movieDirectorLabel.Top += 20;
            var movieYearLabel = new Label
            {
                Text = movie.YearString,
                Height = 15,
                Location = movieDirectorLabel.Location
            };
            movieYearLabel.Top += 15;
            var movieDurationLabel = new Label
            {
                Text = movie.DurationString,
                Height = 15,
                Location = movieTitleLabel.Location
            };
            movieDurationLabel.Left += 300;
            var watchSessionsButton = new Button
            {
                Text = "Seansid",
                Location = movieDurationLabel.Location,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            watchSessionsButton.FlatAppearance.BorderSize = 1;
            watchSessionsButton.Top += 15;
            watchSessionsButton.Click += (sender, args) =>
            {
                var mainForm = ParentForm as CinemaForm;
                if (mainForm != null) mainForm.DisplaySelectedMovie(movie);
            };
            return new Control[]
                {movieTitleLabel, movieDirectorLabel, movieYearLabel, movieDurationLabel, watchSessionsButton};
        }
    }
}