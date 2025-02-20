using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_ZX
{
    public partial class FormSetting : Form
    {
        string LocationSave = Properties.Settings.Default.SettingLocationSave;
        int modified = 0;
        public FormSetting()
        {
            InitializeComponent();
            tbxLocation.Text = LocationSave;
        }

        private void btn_ProcessData(object sender, EventArgs e)
        {
            if (sender == btnOK) // ตรวจสอบว่าเหตุการณ์เป็นจากปุ่ม OK หรือไม่
            {
                switch (modified)
                {
                    case 1:
                        if (Locationsave())
                        {
                            this.Close();
                        }
                        break;
                    default:
                        this.Close();
                        break;
                }
            }
            else if (sender == btnCancel) // ตรวจสอบว่าเหตุการณ์เป็นจากปุ่ม Cancel หรือไม่
            {
                this.Close();
            }
        }


        private void Modified_Process(object sender, EventArgs e)
        {
            modified = 1;
        }

        private bool Locationsave()
        {
            string text = tbxLocation.Text;

            if (!string.IsNullOrEmpty(text)) // Check if the text is not empty
            {
                try
                {
                    // Check if the directory exists
                    if (System.IO.Directory.Exists(text))
                    {
                        // The text represents a valid directory location
                        // Continue with the next steps here
                        // Save the directory location to Settings
                        Properties.Settings.Default.SettingLocationSave = text;
                        Properties.Settings.Default.Save();
                        return true; // Return true if the directory exists and the location is saved successfully
                    }
                    else
                    {
                        // The text does not represent an existing directory location
                        // Perform further actions here
                        MessageBox.Show("Error: Location not found");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during directory check
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                Properties.Settings.Default.SettingLocationSave = "";
                Properties.Settings.Default.Save();
                return true;
            }

            return false; // Return false if any other error occurs
        }

        private void btnLocationSave_Click(object sender, EventArgs e)
        {
            modified = 1;
            // Open the dialog when the button is clicked
            DialogResult result = this.folderBrowserDialog1.ShowDialog();
            // Check the result
            if (result == DialogResult.OK)
            {
                // Get the selected path
                string selectedPath = this.folderBrowserDialog1.SelectedPath;
                // Do something with the selected path
                // For example, save it to a database, display a message, etc.
                tbxLocation.Text = selectedPath;
                //MessageBox.Show("You selected: " + selectedPath);
            }
        }

        private void FormSetting_Load(object sender, EventArgs e)
        {

        }
    }

}
