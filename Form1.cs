using System;
using System.Drawing;
using System.Windows.Forms;

namespace Keystrokes
{
    public partial class KeystrokesWindow : Form
    {
        private Color Color1 = Color.FromArgb(255, 35, 35, 35);
        private Color Color2 = Color.FromArgb(255, 60, 60, 60);
        private readonly GlobalKeyboardHook Gkh = new GlobalKeyboardHook();

        private readonly Keys[] KeysToAdd = {Keys.W, Keys.A, Keys.S, Keys.D, Keys.Space, Keys.LShiftKey, Keys.LControlKey};

        public KeystrokesWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var AddKey in KeysToAdd)
                Gkh.HookedKeys.Add(AddKey);
            Gkh.KeyDown += gkh_KeyDown;
            Gkh.KeyUp += gkh_KeyUp;
        }

        private void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            debugText.Text = e.KeyCode + @" UP";
            switch (e.KeyCode.ToString())
            {
                case "W":
                    panel1.BackColor = Color2;
                    break;
                case "A":
                    panel4.BackColor = Color2;
                    break;
                case "S":
                    panel3.BackColor = Color2;
                    break;
                case "D":
                    panel2.BackColor = Color2;
                    break;
                case "Space":
                    panel5.BackColor = Color2;
                    break;
                case "LShiftKey":
                    panel6.BackColor = Color2;
                    break;
                case "LControlKey":
                    panel7.BackColor = Color2;
                    break;
            }
        }

        private void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            debugText.Text = e.KeyCode + @" DOWN";
            switch (e.KeyCode.ToString())
            {
                case "W":
                    panel1.BackColor = Color1;
                    break;
                case "A":
                    panel4.BackColor = Color1;
                    break;
                case "S":
                    panel3.BackColor = Color1;
                    break;
                case "D":
                    panel2.BackColor = Color1;
                    break;
                case "Space":
                    panel5.BackColor = Color1;
                    break;
                case "LShiftKey":
                    panel6.BackColor = Color1;
                    break;
                case "LControlKey":
                    panel7.BackColor = Color1;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var BtnSender = (Button) sender;
            var PtLowerLeft = new Point(0, BtnSender.Height);
            PtLowerLeft = BtnSender.PointToScreen(PtLowerLeft);
            contextMenuStrip1.Show(PtLowerLeft);
        }

        private void onTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (onTopToolStripMenuItem.Checked)
            {
                onTopToolStripMenuItem.Checked = false;
                TopMost = false;
            }
            else
            {
                onTopToolStripMenuItem.Checked = true;
                TopMost = true;
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
                if (ActiveForm != null)
                    ActiveForm.BackColor = Color.FromArgb(255, 240, 240, 240);
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

                Color1 = Color.FromArgb(255, 150, 150, 150);
                Color2 = Color.FromArgb(255, 190, 190, 190);
            }
            else
            {
                darkModeToolStripMenuItem.Checked = true;
                if (ActiveForm != null)
                    ActiveForm.BackColor = Color.FromArgb(255, 17, 17, 17);
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

                Color1 = Color.FromArgb(255, 35, 35, 35);
                Color2 = Color.FromArgb(255, 60, 60, 60);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var Credits = new Credits();
            Credits.Show();
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
                if (ActiveForm != null)
                    ActiveForm.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                showTitleBarToolStripMenuItem.Checked = true;
                if (ActiveForm != null)
                    ActiveForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            }
        }
    }
}