using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Enums;

namespace tthk_kinoteater.Models
{
    class Place
    {
        public int Number { get; set; }
        public int Row { get; set; }
        public Hall Hall { get; set; }
        public CheckBox GetCheckBox(Session session)
        {
            CheckBox placeCheckBox = new CheckBox();
            PlaceStatus placeStatus = new PlaceStatus();
            if (placeStatus == PlaceStatus.Occupied)
            {
                placeCheckBox.Enabled = false;
                placeCheckBox.Checked = true;
                placeCheckBox.BackColor = Color.Red;
            }
            else if (placeStatus == PlaceStatus.Selected)
            {
                placeCheckBox.Checked = true;
                placeCheckBox.BackColor = Color.Yellow;
            }
            else
            {
                placeCheckBox.Checked = false;
                placeCheckBox.BackColor = Color.Green;
            }
            return placeCheckBox;
        }
    }
}
