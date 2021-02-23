using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class SessionView : UserControl
    {
        public SessionView(Session session)
        {
            InitializeComponent();
        }
        
        private Control[] InitializeControls(Session session)
        {
            Label movieTitleLabel = new Label()
            {
                Text = session.Movie.Title,
                Width = 300,
                Location = new Point(50, 50),
                Height = 20,
                Font = new Font(FontFamily.GenericSansSerif, 10)
            };
            Label movieDirectorLabel = new Label()
            {
                Text = session.Movie.Director,
                Height = 15,
                Location = movieTitleLabel.Location
            };
            movieDirectorLabel.Top += 20;
            Label movieYearLabel = new Label()
            {
                Text = session.Movie.YearString,
                Height = 15,
                Location = movieDirectorLabel.Location
            };
            movieYearLabel.Top += 15;
            Label movieDurationLabel = new Label()
            {
                Text = session.Movie.DurationString,
                Height = 15,
                Location = movieTitleLabel.Location
            };
            movieDurationLabel.Left += 300;
            Button buyTicketButton = new Button()
            {
                Text = "Osta pilet",
                Location = movieDurationLabel.Location,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            buyTicketButton.FlatAppearance.BorderSize = 1;
            buyTicketButton.Top += 15;
            buyTicketButton.Click += (s, e) =>
            {
                
            };
            return new Control[]
                {movieTitleLabel, movieDirectorLabel, movieYearLabel, movieDurationLabel, buyTicketButton};
        }
    }
}