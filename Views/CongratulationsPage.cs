using System;
using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Enums;

namespace tthk_kinoteater.Views
{
    public partial class CongratulationsPage : UserControl
    {
        public CongratulationsPage(string name)
        {
            Size = new Size(500, 1000);
            var congratulationsLabel = new Label
            {
                Text = $"{name}, aitäh ostu eest!\nOlete alati teretulnud!",
                Font = new Font(FontFamily.GenericSansSerif, 16, FontStyle.Regular),
                Location = new Point(50, 100),
                Size = new Size(350, 100),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Orange
            };
            var backButton = new Button
            {
                Text = "Tagasi",
                Location = new Point(50, congratulationsLabel.Top+congratulationsLabel.Height+5),
                Size = new Size(350, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            backButton.Click += BackButtonOnClick;
            Controls.Add(congratulationsLabel);
            Controls.Add(backButton);
            InitializeComponent();
        }

        private void BackButtonOnClick(object sender, EventArgs e)
        {
            if (ParentForm is CinemaForm mainForm) mainForm.Stage = Stage.MovieOverview;
        }
    }
}