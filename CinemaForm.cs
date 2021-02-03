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
            InitializeComponent();
        }
    }
}
