using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class SessionPage : UserControl
    {
        private readonly List<Session> sessions;
        private ComboBox datesComboBox;
        private ComboBox directorsComboBox;
        private SessionList sessionList;

        private ComboBox titlesComboBox;
        private ComboBox yearsComboBox;

        public SessionPage()
        {
            InitializeComponent();
            var dataHandler = new DataHandler();
            sessions = dataHandler.GetSessions();
            LoadHeaders(sessions);
            Size = new Size(500, 1000);
            sessionList = new SessionList(sessions)
            {
                Size = new Size(450, 500),
                Location = new Point(0, 0)
            };
            Controls.Add(sessionList);
        }

        private void LoadHeaders(List<Session> currentSessions)
        {
            var moviesStageTitleLabel = new Label
            {
                Text = "Kinokava",
                Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
                Location = new Point(50, 0),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Orange
            };
            Controls.Add(moviesStageTitleLabel);
            var moviesTitles = GetTitles(currentSessions);
            titlesComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesTitles,
                Left = 50,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 125
            };
            var moviesDirectors = GetDirectors(currentSessions);
            directorsComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesDirectors,
                Left = titlesComboBox.Left + 125,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 125
            };
            var moviesYears = GetYears(currentSessions);
            yearsComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesYears,
                Left = directorsComboBox.Left + 125,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 50
            };
            var sessionsDates = GetDates(currentSessions);
            datesComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = sessionsDates,
                Left = yearsComboBox.Left + 50,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 100
            };
            Controls.Add(titlesComboBox);
            Controls.Add(directorsComboBox);
            Controls.Add(yearsComboBox);
            Controls.Add(datesComboBox);
            titlesComboBox.SelectedIndexChanged += FilterComboBoxOnSelectedIndexChanged;
            yearsComboBox.SelectedIndexChanged += FilterComboBoxOnSelectedIndexChanged;
            directorsComboBox.SelectedIndexChanged += FilterComboBoxOnSelectedIndexChanged;
            datesComboBox.SelectedIndexChanged += FilterComboBoxOnSelectedIndexChanged;
        }

        private void FilterComboBoxOnSelectedIndexChanged(object sender, EventArgs e)
        {
            var title = titlesComboBox.SelectedValue.ToString();
            var year = yearsComboBox.SelectedValue.ToString();
            var director = directorsComboBox.SelectedValue.ToString();
            var date = datesComboBox.SelectedValue.ToString();
            var selectedSessions = sessions;
            if (!string.IsNullOrEmpty(title))
                selectedSessions = selectedSessions.Where(s => s.Movie.Title == title).ToList();

            if (!string.IsNullOrEmpty(year))
                selectedSessions = selectedSessions.Where(s => s.Movie.Year == Convert.ToInt32(year)).ToList();

            if (!string.IsNullOrEmpty(director))
                selectedSessions = selectedSessions.Where(s => s.Movie.Director == director).ToList();
            if (!string.IsNullOrEmpty(date))
                selectedSessions = selectedSessions.Where(s => s.StartTime.Date == Convert.ToDateTime(date).Date)
                    .ToList();
            Controls.Remove(sessionList);
            sessionList = new SessionList(selectedSessions)
            {
                Size = new Size(450, 500),
                Location = new Point(0, 0)
            };
            Controls.Add(sessionList);
        }

        private string[] GetDirectors(List<Session> sessionsToFilter)
        {
            return sessionsToFilter.Select(s => s.Movie.Director)
                .Append("")
                .OrderBy(d => d)
                .Distinct()
                .ToArray();
        }

        private string[] GetYears(List<Session> sessionsToFilter)
        {
            return sessionsToFilter.Select(s => s.Movie.Year.ToString())
                .Append("")
                .OrderBy(y => y)
                .Distinct()
                .ToArray();
        }

        private string[] GetTitles(List<Session> sessionsToFilter)
        {
            return sessionsToFilter.Select(s => s.Movie.Title)
                .Append("")
                .OrderBy(m => m)
                .Distinct()
                .ToArray();
        }

        private string[] GetDates(List<Session> sessionsToFilter)
        {
            return sessionsToFilter.Select(s => s.StartTime.Date.ToString("d.MM.yyyy"))
                .Append("")
                .OrderBy(s => s)
                .Distinct()
                .ToArray();
        }

        internal void SelectMovie(Movie movie)
        {
            titlesComboBox.SelectedItem = movie.Title;
        }
    }
}