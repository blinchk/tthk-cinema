using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class TicketPage : UserControl
    {
        private readonly List<Place> selectedPlaces;
        private Session session;

        public TicketPage(Session session)
        {
            this.session = session;
            selectedPlaces = new List<Place>();
            var amountLabel = new Label
            {
                Text = $"Summa: {PurchaseAmount():F}\nPiletid: {TicketsAmount}",
                Size = new Size(160, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(400, 300)
            };
            var backButton = new Button
            {
                Text = "Tagasi",
                Location = new Point(400, 350),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            backButton.Click += BackButtonOnClick;
            var purchaseButton = new Button
            {
                Text = "Osta",
                Location = new Point(480, 350),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            purchaseButton.Click += PurchaseButtonOnClick;
            Controls.Add(amountLabel);
            Size = new Size(1000, 1000);
            var dataHandler = new DataHandler();
            var buyedTickets = dataHandler.GetTickets(session);
            InitializeComponent();
            double rowsAverage = Convert.ToInt32(session.Hall.NumberOfPlaces) / 12 / 2;
            foreach (var place in session.Hall.Places)
            {
                if (buyedTickets.Any(t => t.Row == place.Row && t.Number == place.Number))
                {
                    place.IsBusy = PlaceStatus.Occupied;
                }
                var placeCheckBox = place.GetCheckBox(session);
                if (place.IsBusy == PlaceStatus.Free && Math.Abs(rowsAverage-place.Row+1) < 1)
                {
                    placeCheckBox.BackColor = Color.Orange;
                }
                placeCheckBox.Size = new Size(30, 30);
                placeCheckBox.Location = new Point(30 * place.Number, 30 * place.Row);
                placeCheckBox.CheckedChanged += (sender, args) =>
                {
                    switch (place.IsBusy)
                    {
                        case PlaceStatus.Free:
                            place.IsBusy = PlaceStatus.Selected;
                            placeCheckBox = place.UpdateCheckBox(placeCheckBox);
                            selectedPlaces.Add(place);
                            break;
                        case PlaceStatus.Selected:
                            place.IsBusy = PlaceStatus.Free;
                            placeCheckBox = place.UpdateCheckBox(placeCheckBox);
                            if (Math.Abs(rowsAverage-place.Row+1) < 1)
                            {
                                placeCheckBox.BackColor = Color.Orange;
                            }
                            if (selectedPlaces.Contains(place)) selectedPlaces.Remove(place);
                            break;
                    }

                    amountLabel.Text = $"Summa: {PurchaseAmount():F}€\nPiletid: {TicketsAmount}";
                };
                Controls.Add(placeCheckBox);
                Controls.Add(backButton);
                Controls.Add(purchaseButton);
            }
        }

        private int TicketsAmount => selectedPlaces.Count;

        private void PurchaseButtonOnClick(object sender, EventArgs e)
        {
            if (ParentForm is CinemaForm mainForm && selectedPlaces.Count > 0) mainForm.DisplayReception(selectedPlaces, session);
        }

        private void BackButtonOnClick(object sender, EventArgs e)
        {
            if (ParentForm is CinemaForm mainForm) mainForm.Stage = Stage.SessionOverview;
        }

        private double PurchaseAmount()
        {
            const double basicTicketPrice = 4.90;
            var additionalCosts = selectedPlaces.Sum(p => p.TicketAdditionalCost);
            var basicCosts = basicTicketPrice * selectedPlaces.Count;
            return additionalCosts + basicCosts;
        }
    }
}