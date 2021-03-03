using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class ReceptionPage : UserControl
    {
        private List<Place> selectedPlaces;
        private Label emailLabel;
        private TextBox emailTextBox;
        
        public ReceptionPage(List<Place> selectedPlaces)
        {
            Size = new Size(1000, 1000);
            this.selectedPlaces = selectedPlaces;
            var receptionPageLabel = new Label
            {
                Text = "Ostu vormistamine",
                Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold),
                Location = new Point(50, 0),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Orange
            };
            List<string> purchaseStrings = new List<string>();
            foreach (var place in selectedPlaces)
            {
                purchaseStrings.Add($"1x Pilet | Rida: {place.Row} | Koht: {place.Number}");
            }
            var purchaseContentLabel = new Label()
            {
                Text = String.Join("\n", purchaseStrings.ToArray()),
                Location = new Point(50, 50),
                Size = new Size(600, 300)
            };
            emailLabel = new Label()
            {
                Text = "E-post"
            };
            emailTextBox = new TextBox();
            Controls.Add(receptionPageLabel);
            Controls.Add(purchaseContentLabel);
            InitializeComponent();
        }
    }
}