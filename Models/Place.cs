using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Enums;

namespace tthk_kinoteater.Models
{
    public class Place : IPlace
    {
        public int Number { get; set; }
        public int Row { get; set; }
        public Hall Hall { get; set; }
        public double TicketAdditionalCost => 0.10 * Row;
        public PlaceStatus IsBusy { get; set; }

        public CheckBox GetCheckBox(Session session)
        {
            var placeCheckBox = new CheckBox
            {
                CheckAlign = ContentAlignment.MiddleCenter
            };
            placeCheckBox = UpdateCheckBox(placeCheckBox);
            return placeCheckBox;
        }

        public CheckBox UpdateCheckBox(CheckBox checkBox)
        {
            if (IsBusy == PlaceStatus.Occupied)
            {
                checkBox.Enabled = false;
                checkBox.Checked = true;
                checkBox.BackColor = Color.Red;
            }
            else if (IsBusy == PlaceStatus.Selected)
            {
                checkBox.Checked = true;
                checkBox.BackColor = Color.Yellow;
            }
            else
            {
                checkBox.Checked = false;
                checkBox.BackColor = Color.Green;
            }

            return checkBox;
        }
    }
}