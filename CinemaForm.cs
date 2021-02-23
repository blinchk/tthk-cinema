﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;
using tthk_kinoteater.Views;

namespace tthk_kinoteater
{
    public partial class CinemaForm : Form
    {
        private DataHandler dataHandler;
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
            MoviePage moviePage = new MoviePage()
            {
                Size = new Size(500, 1000)
            };
            InitializeComponent();
            
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

        private void DisplayCurrentStage()
        {
            Controls.Clear();
            switch (stage)
            {  
                case Stage.SessionOverview:
                    Controls.Add(new SessionPage());
                    break;
                case Stage.MovieOverview:
                    Controls.Add(new MoviePage());
                    break;
            }
        }
    }
}
