using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater
{
    public partial class CinemaForm : Form
    {
        public CinemaForm()
        {
            DataHandler dataHandler = new DataHandler();
            Panel moviePanel = new Panel();
            Label movieTitleLabel = new Label();
            Label movieDescriptionLabel = new Label();
            Label movieDurationLabel = new Label();
            Label movieStartTimeLabel = new Label();
            Label movieEndTimeLabel = new Label();
            InitializeComponent();
        }
    }
}
