using System.Collections.Generic;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class ReceptionPage : UserControl
    {
        private List<Place> busyPlaces;
        
        public ReceptionPage(List<Place> busyPlaces)
        {
            this.busyPlaces = busyPlaces;
            InitializeComponent();
        }
    }
}