using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class MoviePage : UserControl
    {
        private DataHandler dataHandler;
        private ComboBox yearsComboBox;
        private ComboBox directorsComboBox;
        private MovieList movieList;
        private List<Movie> movies;
        
        public MoviePage()
        {
            InitializeComponent();
            dataHandler = new DataHandler();
            movies = dataHandler.GetMovies();
            LoadHeaders(movies);
            movieList = new MovieList(movies)
            {
                Size = new Size(450, 500),
                Location = new Point(0, 0)
            };
            Controls.Add(movieList);
        }
        
        private void LoadHeaders(List<Movie> moviesToDistinct)
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
            string[] moviesDirectors = GetDirectors(moviesToDistinct);
            directorsComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesDirectors,
                Left = 50,
                Top = moviesStageTitleLabel.Top + 50,
                Width = 125
            };
            string[] moviesYears = GetYears(moviesToDistinct);
            yearsComboBox = new ComboBox()
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = moviesYears,
                Left = directorsComboBox.Left + 125,
                Top = moviesStageTitleLabel.Top + 50,
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
            var selectedMovies = movies;
            if (!String.IsNullOrEmpty(year))
            {
                selectedMovies = selectedMovies.Where(m => m.Year == Convert.ToInt32(year)).ToList();
            }

            if (!String.IsNullOrEmpty(director))
            {
                selectedMovies = selectedMovies.Where(m => m.Director == director).ToList();
            }
            Controls.Remove(movieList);
            movieList = new MovieList(selectedMovies)
            {
                Size = new Size(450, 500),
                Location = new Point(0, 0)
            };;
            Controls.Add(movieList);
        }

        private string[] GetDirectors(List<Movie> moviesToFilter)
        {
            return moviesToFilter.Select(m => m.Director)
                .Append("")
                .OrderBy(d => d)
                .Distinct()
                .ToArray();
        }
        
        private string[] GetYears(List<Movie> moviesToFilter)
        {
            return moviesToFilter.Select(m => m.Year.ToString())
                .Append("")
                .OrderBy(y => y)
                .Distinct()
                .ToArray();
        }
    }
}