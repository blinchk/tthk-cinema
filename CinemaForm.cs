using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;

namespace tthk_kinoteater
{
    public partial class CinemaForm : Form
    {
        DataHandler dataHandler;
        private Stage stage;
        const Stage defaultStage = Stage.SessionOverview;
        public CinemaForm()
        {
            stage = defaultStage;
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
            dataHandler = new DataHandler();
            List<Movie> movies = dataHandler.GetMovies();
            int[] moviesYears = movies.Select(m => m.Year)
                .ToArray();
            string[] moviesDirectors = movies.Select(m => m.Director)
                .Distinct()
                .ToArray();
            movies.ForEach(m => Console.WriteLine(m.Title));
        }

        private void DisplayCurrentStage()
        {
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
