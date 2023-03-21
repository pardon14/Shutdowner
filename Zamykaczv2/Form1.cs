using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Zamykaczv2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        public static class ShutdownManager
        {
            public static void ScheduleShutdown(int seconds)
            {
                Process.Start("shutdown", $"/s /t {seconds}");
            }

            public static void CancelShutdown()
            {
                Process.Start("shutdown", "/a");
            }
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {

            DateTime shutdownTime = dateTimePicker1.Value;

            // Ustaw czas wyłączenia systemu
            int shutdownTimeout = (int)(shutdownTime - DateTime.Now).TotalSeconds;
            ShutdownManager.ScheduleShutdown(shutdownTimeout);

            // Wyświetl powiadomienie o czasie wyłączenia systemu
            TimeSpan ts = TimeSpan.FromSeconds(shutdownTimeout);
            string timeToShutdown = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                    ts.Hours, ts.Minutes, ts.Seconds);
            MessageBox.Show($"The system will be shutdown in {timeToShutdown}", "System shutdown", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Anuluj wyłączenie systemu
            ShutdownManager.CancelShutdown();

            // Wyświetl powiadomienie o anulowaniu wyłączenia systemu
            MessageBox.Show("The system shutdown has been canceled.", "Canceling system shutdown", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            // Zamknij aplikację
            Application.Exit();

            Process.GetCurrentProcess().Kill();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMMM yyyy HH:mm:ss";
            dateTimePicker1.ShowUpDown = true;
        }
    }
}