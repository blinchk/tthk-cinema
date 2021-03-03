using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;
using Message = tthk_kinoteater.Models.Message;

namespace tthk_kinoteater.Views
{
    public partial class ReceptionPage : UserControl
    {
        private const double basicTicketPrice = 4.90;
        private readonly Label emailLabel;
        private readonly TextBox emailTextBox;
        private readonly Label nameLabel;
        private readonly TextBox nameTextBox;
        private readonly string purchaseAmount;
        private readonly Button purchaseButton;
        private readonly List<string> purchaseStrings;
        private readonly List<Place> selectedPlaces;
        private readonly Session session;

        public ReceptionPage(List<Place> selectedPlaces, Session session)
        {
            this.session = session;
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
            purchaseStrings = new List<string>();
            foreach (var place in selectedPlaces)
                purchaseStrings.Add(
                    $"1x Pilet | Rida: {place.Row} | Koht: {place.Number} | {basicTicketPrice + place.TicketAdditionalCost:F}€");

            purchaseAmount = $"Summa: {PurchaseAmount():F}€";
            var purchaseContentLabel = new Label
            {
                Text = string.Join("\n", purchaseStrings.ToArray()) + "\n" + purchaseAmount,
                Location = new Point(50, 50),
                Size = new Size(350, purchaseStrings.Count * 20 + 15),
                TextAlign = ContentAlignment.MiddleCenter
            };
            nameLabel = new Label
            {
                Text = "Nimi",
                Location = new Point(50, 10 + purchaseContentLabel.Height + purchaseContentLabel.Top),
                Size = new Size(50, 30),
                TextAlign = ContentAlignment.TopCenter
            };
            nameTextBox = new TextBox
            {
                Location = new Point(100, 10 + purchaseContentLabel.Height + purchaseContentLabel.Top),
                Size = new Size(150, 30)
            };
            emailLabel = new Label
            {
                Text = "E-post",
                Location = new Point(50, nameLabel.Top + nameLabel.Height),
                Size = new Size(50, 30),
                TextAlign = ContentAlignment.TopCenter
            };
            emailTextBox = new TextBox
            {
                Location = new Point(100, nameLabel.Top + nameLabel.Height),
                Size = new Size(150, 30)
            };
            purchaseButton = new Button
            {
                Text = "Vormista",
                Location = new Point(50, emailLabel.Top + emailLabel.Height),
                Size = new Size(200, 50),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            var backButton = new Button
            {
                Text = "Tagasi",
                Location = new Point(50, purchaseButton.Top + purchaseButton.Height + 5),
                Size = new Size(200, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Orange,
                FlatStyle = FlatStyle.Flat
            };
            backButton.Click += BackButtonOnClick;
            purchaseButton.Click += PurchaseButtonOnClick;
            Controls.Add(receptionPageLabel);
            Controls.Add(purchaseContentLabel);
            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
            Controls.Add(emailLabel);
            Controls.Add(emailTextBox);
            Controls.Add(purchaseButton);
            Controls.Add(backButton);
            InitializeComponent();
        }

        private void BackButtonOnClick(object sender, EventArgs e)
        {
            if (ParentForm is CinemaForm mainForm) mainForm.Stage = Stage.SessionOverview;
        }

        private void PurchaseButtonOnClick(object sender, EventArgs e)
        {
            var name = nameTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(emailTextBox.Text) && !string.IsNullOrEmpty(name) &&
                emailTextBox.Text.Contains("@"))
            {
                var result = SendReceipt();
                if (result)
                {
                    var dataHandler = new DataHandler();
                    foreach (var place in selectedPlaces)
                        dataHandler.AddTicket(new Ticket
                        {
                            Row = place.Row,
                            Number = place.Number
                        }, session);
                    if (ParentForm is CinemaForm mainForm) mainForm.DisplayCongratulations(name);
                }
                else
                {
                    MessageBox.Show("E-mail on vale. Palun proovige uuesti!", "Viga", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("E-mail või nimi pole täidetud. Palun proovige uuesti!", "Viga", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string GenerateTickets()
        {
            var name = nameTextBox.Text.Trim();
            var sessionData = $"{session.Movie.Title} | {session.TimeString} | Saal: {session.Hall.Title}";
            var body = File.ReadAllText("Assets/Receipt.htm");
            var amount = PurchaseAmount().ToString("F");
            var tax = (PurchaseAmount() * 0.2).ToString("F");
            var purchase = string.Join("<br>", purchaseStrings.ToArray());
            var operationTime = DateTime.Now.ToString(@"dd.MM.yyyy H\:mm\:ss");
            body = body.Replace("#Session#", sessionData);
            body = body.Replace("#Amount#", amount);
            body = body.Replace("#Name#", name);
            body = body.Replace("#Tax#", tax);
            body = body.Replace("#Purchase#", purchase);
            body = body.Replace("#OperationTime#", operationTime);
            return body;
        }

        private bool SendReceipt()
        {
            var message = new Message(GenerateTickets())
            {
                Recipients = new List<string> {emailTextBox.Text.Trim()}
            };
            return message.Send();
        }

        private double PurchaseAmount()
        {
            var additionalCosts = selectedPlaces.Sum(p => p.TicketAdditionalCost);
            var basicCosts = basicTicketPrice * selectedPlaces.Count;
            return additionalCosts + basicCosts;
        }
    }
}