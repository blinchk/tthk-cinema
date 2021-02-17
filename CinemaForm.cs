using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;

namespace tthk_kinoteater
{
    public partial class CinemaForm : Form
    {
        DataHandler dataHandler;
        private Stage stage;

        private Stage Stage
        {
            get => stage;
            set
            {
                stage = value;
                DisplayCurrentStage();
            }
        }

        const Stage DefaultStage = Stage.SessionOverview;
        static Color SpaceGray = Color.FromArgb(52, 61, 70);
        public CinemaForm()
        {
            Stage = DefaultStage;
            BackColor = SpaceGray;
            ForeColor = Color.White;
            DisplayMovies();
            InitializeComponent();
        }

        private void DisplaySessions(DateTime date)
        {
            dataHandler = new DataHandler();
            List<Session> sessions = dataHandler.GetSessions()
                .Where(s => s.StartTime.Date == date.Date)
                .ToList();
        }

        private void InitializeSession(Session session)
        {
            Panel sessionPanel = new Panel();
            Label sessionTimeLabel = new Label()
            {
                Text = session.DurationString + " | " + session.TimeString
            };
            Label sessionMovieTitleLabel = new Label()
            {
                Text = session.Movie.Title
            };
            Label sessionMovieInfoLabel = new Label()
            {
                Text = session.Movie.Director + " | " + session.Movie.Year + " | " + session.DurationString
            };
        }

        private void DisplayMovies()
        {
            Label moviesStageTitleLabel = new Label()
            {
                Text = "Praegu kinos",
                Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
                Location = new Point(50, 0),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Orange
            };
            dataHandler = new DataHandler();
            List<Movie> movies = dataHandler.GetMovies();
            int[] moviesYears = movies.Select(m => m.Year)
                .ToArray();
            string[] moviesDirectors = movies.Select(m => m.Director)
                .Distinct()
                .ToArray();
            movies.ForEach(m => Console.WriteLine(m.Title));
            for (int i = 0; i < movies.Count; i++)
            {
                var movie = movies[i];
                InitializeMovie(movie, i + 1);
            }
            Controls.Add(moviesStageTitleLabel);
        }

        private void InitializeMovie(Movie movie, int index)
        {
            Label movieTitleLabel = new Label()
            {
                Text = movie.Title,
                Width = 300,
                Location = new Point(50, 50 * index),
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
            watchSessionsButton.Click += WatchSessionsButton_Click;
            Controls.AddRange(new Control[] { movieTitleLabel, movieDirectorLabel, movieYearLabel, movieDurationLabel, watchSessionsButton });
        }

        private void WatchSessionsButton_Click(object sender, EventArgs e)
        {
            
        }

        private void DisplayCurrentStage()
        {
            switch (Stage)
            {
                case Stage.SessionOverview:
                    DisplaySessions(DateTime.Now);
                    break;
                case Stage.MovieOverview:
                    DisplayMovies();
                    break;
            }
        }
    }
}
