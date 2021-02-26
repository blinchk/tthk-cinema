using System;
using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;
using tthk_kinoteater.Views;

namespace tthk_kinoteater
{
    public sealed partial class CinemaForm : Form
    {
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
            InitializeComponent();
        }

        private void DisplayStageChangeCheckBox(Stage currentStage)
        {
            CheckBox stageCheckBox = new CheckBox()
            {
                Location = new Point(350, 10),
                Size = new Size(100, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            stageCheckBox.Text = currentStage switch
            {
                Stage.SessionOverview => "Kõik filmid",
                Stage.MovieOverview => "Kõik seansid",
                _ => stageCheckBox.Text
            };
            stageCheckBox.CheckedChanged += StageCheckBoxOnCheckedChanged;
            stageCheckBox.Appearance = Appearance.Button;
            Controls.Add(stageCheckBox);
            stageCheckBox.BringToFront();
        }

        private void StageCheckBoxOnCheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox)
            {
                Stage = Stage == Stage.MovieOverview ? Stage.SessionOverview : Stage.MovieOverview;
            }
        }

        private void DisplayCurrentStage()
        {
            Controls.Clear();
            switch (stage)
            {  
                case Stage.SessionOverview:
                    var sessionPage = new SessionPage();
                    Controls.Add(sessionPage);
                    break;
                case Stage.MovieOverview:
                    var moviePage = new MoviePage();
                    Controls.Add(moviePage);
                    break;
            }
            DisplayStageChangeCheckBox(stage);
        }

        public void DisplaySelectedMovie(Movie movie)
        {
            Controls.Clear();
            stage = Stage.MovieOverview;
            var sessionPage = new SessionPage();
            Controls.Add(sessionPage);
            sessionPage.SelectMovie(movie);
            DisplayStageChangeCheckBox(stage);
        }
    }
}
