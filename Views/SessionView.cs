using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class SessionView : UserControl
    {
        public SessionView(Session session)
        {
            Controls.AddRange(InitializeControls(session));
            InitializeComponent();
        }

        private Control[] InitializeControls(Session session)
        {
            var movieTitleLabel = new Label
            {
                Text = session.Movie.Title,
                Width = 300,
                Location = new Point(50, 50),
                Height = 20,
                Font = new Font(FontFamily.GenericSansSerif, 10)
            };
            var movieDirectorLabel = new Label
            {
                Text = $"{session.Movie.Director} | {session.Movie.YearString} | {session.Movie.DurationString}",
                Height = 15,
                Width = 200,
                Location = movieTitleLabel.Location
            };
            movieDirectorLabel.Top += 20;
            var movieYearLabel = new Label
            {
                Text = $"Saal: {session.Hall}",
                Height = 15,
                Location = movieDirectorLabel.Location
            };
            movieYearLabel.Top += 15;
            var sessionTimeLabel = new Label
            {
                Text = session.TimeString,
                Height = 15,
                Location = movieTitleLabel.Location
            };
            sessionTimeLabel.Left += 300;
            var buyTicketButton = new Button
            {
                Text = "Osta pilet",
                Location = sessionTimeLabel.Location,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            buyTicketButton.FlatAppearance.BorderSize = 1;
            buyTicketButton.Top += 15;
            buyTicketButton.Click += (sender, args) =>
            {
                if (ParentForm is CinemaForm mainForm) mainForm.DisplayTickets(session);
            };
            return new Control[]
                {movieTitleLabel, movieDirectorLabel, movieYearLabel, sessionTimeLabel, buyTicketButton};
        }
    }
}