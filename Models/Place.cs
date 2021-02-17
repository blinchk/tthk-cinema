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
        public PlaceStatus IsBusy { get; set; }
        public CheckBox GetCheckBox(Session session)
        {
            CheckBox placeCheckBox = new CheckBox();
            if (IsBusy == PlaceStatus.Occupied)
            {
                placeCheckBox.Enabled = false;
                placeCheckBox.Checked = true;
                placeCheckBox.BackColor = Color.Red;
            }
            else if (IsBusy == PlaceStatus.Selected)
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
