using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class TicketPage : UserControl
    {
        public TicketPage(Session session)
        {
            Size = new Size(500, 1000);
            InitializeComponent();
        }
    }
}