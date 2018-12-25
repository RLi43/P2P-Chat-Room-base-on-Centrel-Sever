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
    public partial class MainWin : Form
    {
        public MainWin()
        {
            InitializeComponent();

            TreeNode node = new TreeNode();
            node.Text = userID;
            node.Name = userID;
            treeView_Frds.Nodes.Add(node);
        }

        public string userID ;

        bool find_text_empty = true;

        private void textBox_find_Click(object sender, EventArgs e)
        {
            if (find_text_empty)
            {
                textBox_find.ForeColor = Color.Black;
                textBox_find.Text = "";
            }
        }

        private void textBox_find_Leave(object sender, EventArgs e)
        {
            if (textBox_find.Text == "")
            {
                find_text_empty = true;
                textBox_find.ForeColor = Color.Gray;
                textBox_find.Text = "学号/群号";
            }
            else find_text_empty = false;
        }
    }
}
