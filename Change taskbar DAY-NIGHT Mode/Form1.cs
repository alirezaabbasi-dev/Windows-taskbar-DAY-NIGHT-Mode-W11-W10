using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;



namespace Change_taskbar_DAY_NIGHT_Mode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            MinimizeBox = false;
            Text = "";
        }

        private void chck1_CheckedChanged(object sender, EventArgs e)
        {

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);
            if (chck1.Checked == true)
            {
                //Dark mode
                key.SetValue("AppsUseLightTheme", "0");
                key.SetValue("SystemUsesLightTheme", "0");
                chck1.Text = "Dark Mode has on";
            }
            else
            {
                //light mode
                key.SetValue("AppsUseLightTheme", "1");
                key.SetValue("SystemUsesLightTheme", "1");
                chck1.Text = "Light Mode has on";
            }
            key.Close();
            RestartFileExplorer();
        }
        private void RestartFileExplorer()
        {
            try
            {
                // Find the process for File Explorer and close it.
                foreach (var process in Process.GetProcessesByName("explorer"))
                {
                    process.Kill();
                }
                // Restart File Explorer.
                Process.Start("explorer.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Computer\HKEY_LOCAL_MACHINE\
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\Setup", true);
            key.CreateSubKey("MoSetup");
            RegistryKey key1 = Registry.LocalMachine.OpenSubKey(@"HKEY_LOCAL_MACHINE\SYSTEM\Setup\MoSetup", true);
            key1.SetValue("AllowUpgradesWithUnsupportedTPMOrCPU", "1", RegistryValueKind.DWord);
            key1.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);
            if (chck1.Checked == true)
            {
                //show accent color on start and taskbar
                key.SetValue("ColorPrevalence", "1");
            
            }
            else
            {
                key.SetValue("ColorPrevalence", "0");
            }
            RestartFileExplorer();
        }
    }
}


