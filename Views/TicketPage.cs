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
        private List<Place> selectedPlaces;
        public TicketPage(Session session)
        {
            selectedPlaces = new List<Place>();
            Label amountLabel = new Label()
            {
                Text = $"Summa: {PurchaseAmount():F}",
                Location = new Point(400, 300)
            };
            Controls.Add(amountLabel);
            Size = new Size(1000, 1000);
            InitializeComponent();
            foreach (var place in session.Hall.Places)
            {
                var placeCheckBox = place.GetCheckBox(session);
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
                        {
                            place.IsBusy = PlaceStatus.Free;
                            placeCheckBox = place.UpdateCheckBox(placeCheckBox);
                            if (selectedPlaces.Contains(place)) selectedPlaces.Remove(place);
                            break;
                        }
                    }

                    amountLabel.Text = $"Summa: {PurchaseAmount():F}";
                };
                Controls.Add(placeCheckBox);
            }
        }

        private double PurchaseAmount()
        {
            const double basicTicketPrice = 4.90;
            double additionalCosts = selectedPlaces.Sum(p => p.TicketAdditionalCost);
            double basicCosts = basicTicketPrice * selectedPlaces.Count;
            return additionalCosts + basicCosts;
        }
    }
}