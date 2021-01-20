using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Utilities;

namespace Keystrokes
{
    public partial class KeystrokesWindow : Form
    {
        private globalKeyboardHook gkh = new globalKeyboardHook();

        System.Windows.Forms.Keys[] keysToAdd = { Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space, Keys.LShiftKey, Keys.LControlKey };

        private System.Drawing.Color color1 = Color.FromArgb(255, 35, 35, 35);
        private System.Drawing.Color color2 = Color.FromArgb(255, 60, 60, 60);

        public KeystrokesWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Keys addKey in keysToAdd)
            {
                gkh.HookedKeys.Add(addKey);
            }
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
        }

        private void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            debugText.Text = e.KeyCode.ToString() + " UP";
            switch (e.KeyCode.ToString())
            {
                case "W":
                    panel1.BackColor = color2;
                    break;
                case "A":
                    panel4.BackColor = color2;
                    break;
                case "S":
                    panel3.BackColor = color2;
                    break;
                case "D":
                    panel2.BackColor = color2;
                    break;
                case "Space":
                    panel5.BackColor = color2;
                    break;
                case "LShiftKey":
                    panel6.BackColor = color2;
                    break;
                case "LControlKey":
                    panel7.BackColor = color2;
                    break;
            }
        }

        private void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            debugText.Text = e.KeyCode.ToString() + " DOWN";
            switch (e.KeyCode.ToString())
            {
                case "W":
                    panel1.BackColor = color1;
                    break;
                case "A":
                    panel4.BackColor = color1;
                    break;
                case "S":
                    panel3.BackColor = color1;
                    break;
                case "D":
                    panel2.BackColor = color1;
                    break;
                case "Space":
                    panel5.BackColor = color1;
                    break;
                case "LShiftKey":
                    panel6.BackColor = color1;
                    break;
                case "LControlKey":
                    panel7.BackColor = color1;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            contextMenuStrip1.Show(ptLowerLeft);
        }

        private void onTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (onTopToolStripMenuItem.Checked)
            {
                onTopToolStripMenuItem.Checked = false;
                this.TopMost = false;
            }
            else
            {
                onTopToolStripMenuItem.Checked = true;
                this.TopMost = true;
            }
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (debugToolStripMenuItem.Checked)
            {
                debugToolStripMenuItem.Checked = false;
                debugText.Visible = false;
            }
            else
            {
                debugToolStripMenuItem.Checked = true;
                debugText.Visible = true;
            }
        }

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (darkModeToolStripMenuItem.Checked)
            {
                darkModeToolStripMenuItem.Checked = false;
                KeystrokesWindow.ActiveForm.BackColor = Color.FromArgb(255, 240, 240, 240);
                panel1.BackColor = Color.FromArgb(255, 190, 190, 190);
                panel2.BackColor = Color.FromArgb(255, 190, 190, 190);
                panel3.BackColor = Color.FromArgb(255, 190, 190, 190);
                panel4.BackColor = Color.FromArgb(255, 190, 190, 190);
                panel5.BackColor = Color.FromArgb(255, 190, 190, 190);

                label1.ForeColor = Color.FromArgb(255, 0, 0, 0);
                label2.ForeColor = Color.FromArgb(255, 0, 0, 0);
                label3.ForeColor = Color.FromArgb(255, 0, 0, 0);
                label4.ForeColor = Color.FromArgb(255, 0, 0, 0);
                label5.ForeColor = Color.FromArgb(255, 0, 0, 0);

                color1 = Color.FromArgb(255, 150, 150, 150);
                color2 = Color.FromArgb(255, 190, 190, 190);
    }
            else
            {
                darkModeToolStripMenuItem.Checked = true;
                KeystrokesWindow.ActiveForm.BackColor = Color.FromArgb(255, 17, 17, 17);
                panel1.BackColor = Color.FromArgb(255, 50, 50, 50);
                panel2.BackColor = Color.FromArgb(255, 50, 50, 50);
                panel3.BackColor = Color.FromArgb(255, 50, 50, 50);
                panel4.BackColor = Color.FromArgb(255, 50, 50, 50);
                panel5.BackColor = Color.FromArgb(255, 50, 50, 50);

                label1.ForeColor = Color.FromArgb(255, 255, 255, 255);
                label2.ForeColor = Color.FromArgb(255, 255, 255, 255);
                label3.ForeColor = Color.FromArgb(255, 255, 255, 255);
                label4.ForeColor = Color.FromArgb(255, 255, 255, 255);
                label5.ForeColor = Color.FromArgb(255, 255, 255, 255);

                color1 = Color.FromArgb(255, 35, 35, 35);
                color2 = Color.FromArgb(255, 60, 60, 60);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Credits credits = new Credits();
            credits.Show();
        }

        private void shiftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (shiftToolStripMenuItem.Checked)
            {
                shiftToolStripMenuItem.Checked = false;
                panel6.Visible = false;
            }
            else
            {
                shiftToolStripMenuItem.Checked = true;
                panel6.Visible = true;
            }
        }

        private void lCTRLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lCTRLToolStripMenuItem.Checked)
            {
                lCTRLToolStripMenuItem.Checked = false;
                panel7.Visible = false;
            }
            else
            {
                lCTRLToolStripMenuItem.Checked = true;
                panel7.Visible = true;
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showTitleBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showTitleBarToolStripMenuItem.Checked)
            {
                showTitleBarToolStripMenuItem.Checked = false;
            KeystrokesWindow.ActiveForm.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                showTitleBarToolStripMenuItem.Checked = true;
            KeystrokesWindow.ActiveForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            }
        }
    }
}