using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace yimmenu_injector_headless
{
    public partial class startgta : Form
    {
        private Process p = new Process();
        private bool exitRequested = false;

        public startgta()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            p.StartInfo.FileName = "C:\\Program Files (x86)\\Steam\\steam.exe";
            p.StartInfo.Arguments = "steam://rungameid/271590 -silent -minimized";
            p.EnableRaisingEvents = true;
            p.Start();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("C:\\Program Files\\Epic Games\\GTA V\\GTA5.exe");
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
