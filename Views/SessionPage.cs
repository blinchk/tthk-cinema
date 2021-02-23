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
        private DataHandler dataHandler;
        private SessionList sessionList;
        private List<Session> sessions;
        
        public SessionPage()
        {
            InitializeComponent();
            dataHandler = new DataHandler();
            sessions = dataHandler.GetSessions();
            LoadHeaders(sessions);
            sessionList = new SessionList(sessions)
            {
                Size = new Size(450, 500),
                Location = new Point(0, 0)
            };
            Controls.Add(sessionList);
        }

        public SessionPage(Movie movie)
        {
            InitializeComponent();
            dataHandler = new DataHandler();
            sessions = dataHandler.GetSessions();
            LoadHeaders(sessions);
            titlesComboBox.SelectedItem = movie;
            sessionList = new SessionList(sessions)
            {
                Size = new Size(450, 500),
                Location = new Point(0, 0)
            };
            Controls.Add(sessionList);
        }
        
        public SessionPage(DateTime date)
        {
            InitializeComponent();
            dataHandler = new DataHandler();
            sessions = dataHandler.GetSessions();
            LoadHeaders(sessions);
            datesComboBox.SelectedItem = date.Date;
            sessionList = new SessionList(sessions)
            {
                Size = new Size(450, 500),
                Location = new Point(0, 0)
            };
            Controls.Add(sessionList);
        }
        
        private ComboBox titlesComboBox;
        private ComboBox yearsComboBox;
        private ComboBox directorsComboBox;
        private ComboBox datesComboBox;
        
        private void LoadHeaders(List<Session> sessions)
        {
            Label moviesStageTitleLabel = new Label()
            {
                Text = "Kinokava",
                Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
                Location = new Point(50, 0),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Orange
            };
            Controls.Add(moviesStageTitleLabel);
            string[] moviesTitles = GetTitles(sessions);
            titlesComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesTitles,
                Left = 50,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 125
            };
            string[] moviesDirectors = GetDirectors(sessions);
            directorsComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesDirectors,
                Left = titlesComboBox.Left + 125,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 125
            };
            string[] moviesYears = GetYears(sessions);
            yearsComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesYears,
                Left = directorsComboBox.Left + 125,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 50
            };
            string[] sessionsDates = GetDates(sessions);
            datesComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = sessionsDates,
                Left = directorsComboBox.Left + 125,
                Top = yearsComboBox.Top + 50,
                Width = 50
            };
            Controls.Add(yearsComboBox);
            Controls.Add(directorsComboBox);
            yearsComboBox.SelectedIndexChanged += FilterComboBoxOnSelectedValueChanged;
            directorsComboBox.SelectedIndexChanged += FilterComboBoxOnSelectedValueChanged;
        }

        private void FilterComboBoxOnSelectedValueChanged(object sender, EventArgs e)
        {
            var year = yearsComboBox.SelectedValue.ToString();
            var director = directorsComboBox.SelectedValue.ToString();
            var selectedSessions = sessions;
            if (!String.IsNullOrEmpty(year))
            {
                selectedSessions = selectedSessions.Where(s => s.Movie.Year == Convert.ToInt32(year)).ToList();
            }

            if (!String.IsNullOrEmpty(director))
            {
                selectedSessions = selectedSessions.Where(s => s.Movie.Director == director).ToList();
            }
            Controls.Remove(sessionList);
            sessionList = new SessionList(selectedSessions)
            {
                Size = new Size(450, 500),
                Location = new Point(0, 0)
            };;
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
            return sessionsToFilter.Select(s => s.StartTime.Date.ToString())
                .Append("")
                .OrderBy(s => s)
                .Distinct()
                .ToArray();
        }
    }
}