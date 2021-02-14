using System;
using System.Windows.Forms;

namespace Keystrokes
{
    public partial class Credits : Form
    {
        public Credits()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://coolthing.connorcode.com/");
            MessageBox.Show(@"ヾ(≧▽≦*)", @"Gottem");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Basicprogrammer10");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://connorcode.com");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
