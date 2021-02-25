using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class MovieView : UserControl
    {
        public MovieView(Movie movie)
        {
            Control[] controls = InitializeControls(movie);
            Controls.AddRange(controls);
            InitializeComponent();
        }

        private Control[] InitializeControls(Movie movie)
        {
            Label movieTitleLabel = new Label()
            {
                Text = movie.Title,
                Width = 300,
                Location = new Point(50, 50),
                Height = 20,
                Font = new Font(FontFamily.GenericSansSerif, 10)
            };
            Label movieDirectorLabel = new Label()
            {
                Text = movie.Director,
                Height = 15,
                Location = movieTitleLabel.Location
            };
            movieDirectorLabel.Top += 20;
            Label movieYearLabel = new Label()
            {
                Text = movie.YearString,
                Height = 15,
                Location = movieDirectorLabel.Location
            };
            movieYearLabel.Top += 15;
            Label movieDurationLabel = new Label()
            {
                Text = movie.DurationString,
                Height = 15,
                Location = movieTitleLabel.Location
            };
            movieDurationLabel.Left += 300;
            Button watchSessionsButton = new Button()
            {
                Text = "Seansid",
                Location = movieDurationLabel.Location,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            watchSessionsButton.FlatAppearance.BorderSize = 1;
            watchSessionsButton.Top += 15;
            watchSessionsButton.Click += (s, e) =>
            {
                CinemaForm mainForm = ParentForm as CinemaForm;
                if (mainForm != null) mainForm.Stage = Stage.SessionOverview;
            };
            return new Control[]
                {movieTitleLabel, movieDirectorLabel, movieYearLabel, movieDurationLabel, watchSessionsButton};
        }
    }
}