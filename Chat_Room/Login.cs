using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_Room
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string IDnum = textBox_IDnum.Text;
            MainWin mainwin = new MainWin();
            mainwin.userID = IDnum;
            mainwin.Show();

            Hide();
        }

    }
}
