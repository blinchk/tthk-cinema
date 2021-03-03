using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using tthk_kinoteater.Models;

namespace tthk_kinoteater.Views
{
    public partial class SessionList : UserControl
    {
        public SessionList(List<Session> sessions)
        {
            DisplaySessions(sessions);
            InitializeComponent();
        }

        private void DisplaySessions(List<Session> sessions)
        {
            for (var i = 0; i < sessions.Count; i++)
            {
                var session = sessions[i];
                var sessionView = new SessionView(session)
                {
                    Size = new Size(450, 100),
                    Location = new Point(0, 65 + 65 * i)
                };
                Controls.Add(sessionView);
            }
        }
    }
}