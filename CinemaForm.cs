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

        const Stage DefaultStage = Stage.MovieOverview;
        static Color SpaceGray = Color.FromArgb(52, 61, 70);
        public CinemaForm()
        {
            Stage = DefaultStage;
            BackColor = SpaceGray;
            ForeColor = Color.White;
            DisplayCurrentStage();
            InitializeComponent();
        }

        private void DisplaySessions(DateTime date)
        {
            dataHandler = new DataHandler();
            List<Session> sessions = dataHandler.GetSessions()
                .Where(s => s.StartTime.Date == date.Date)
                .ToList();
        }
        
        private void DisplaySessions(Movie movie)
        {
            dataHandler = new DataHandler();
            List<Session> sessions = dataHandler.GetSessions()
                .Where(s => s.Movie == movie)
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

        ComboBox yearsComboBox;

        private List<Movie> LoadMovies()
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
            Controls.Add(moviesStageTitleLabel);
            dataHandler = new DataHandler();
            List<Movie> movies = dataHandler.GetMovies();
            string[] moviesDirectors = GetMoviesDirectors(movies);
            ComboBox directorsComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesDirectors,
                Left = 50,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 125
            };
            string[] moviesYears = GetMoviesYears(movies);
            yearsComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesYears,
                Left = directorsComboBox.Left + 125,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 50
            };
            yearsComboBox.SelectedValueChanged += YearsComboBox_SelectedValueChanged;
            Controls.AddRange(new Control[] { yearsComboBox, directorsComboBox });
            return movies;
        }

        private void DisplayMovies()
        {
            var movies = LoadMovies();
            for (int i = 0; i < movies.Count; i++)
            {
                var movie = movies[i];
                InitializeMovie(movie, i + 1);
            }
        }

        private void DisplayMovies(int year)
        {
            var movies = LoadMovies().Where(m => m.Year == year).ToList();
            for (int i = 0; i < movies.Count; i++)
            {
                var movie = movies[i];
                InitializeMovie(movie, i + 1);
            }
        }

        private void DisplayMovies(string director)
        {
            var movies = LoadMovies().Where(m => m.Director == director).ToList();
            for (int i = 0; i < movies.Count; i++)
            {
                var movie = movies[i];
                InitializeMovie(movie, i + 1);
            }
        }

        private void YearsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string yearString = yearsComboBox.SelectedItem.ToString();
            if (!String.IsNullOrEmpty(yearString))
            {
                int year = Convert.ToInt32(yearString);
                DisplayMovies(year);
            }
            else
            {
                DisplayMovies();
            }
        }

        private string[] GetMoviesDirectors(List<Movie> movies)
        {
            return movies.Select(m => m.Director)
                .Append("")
                .OrderBy(d => d)
                .Distinct()
                .ToArray();
        }
        private string[] GetMoviesYears(List<Movie> movies)
        {
            return movies.Select(m => m.Year.ToString())
                .Append("")
                .OrderBy(y => y)
                .Distinct()
                .ToArray();
        }

        private void InitializeMovie(Movie movie, int index)
        {
            Label movieTitleLabel = new Label()
            {
                Text = movie.Title,
                Width = 300,
                Location = new Point(50, 50 + 50 * index),
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
                Controls.Clear();
                DisplaySessions(movie);
            };
            Controls.AddRange(new Control[] { movieTitleLabel, movieDirectorLabel, movieYearLabel, movieDurationLabel, watchSessionsButton });
        }

        private void DisplayCurrentStage()
        {
            Controls.Clear();
            switch (stage)
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
