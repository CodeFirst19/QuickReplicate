using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QuickReplicate
{
    public partial class Form1 : Form
    {
        Replicate replicate;
        

        public string MyProperty { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void ReplicateButton_Click(object sender, EventArgs e)
        {
            replicate = new Replicate(SourceTextBox.Text, DestinationTextBox.Text, SubDirectoryCheckBox.Checked, MirrorDestinationCheckBox.Checked);
            MessageBox.Show($"{replicate.Message}", $"{replicate.Message}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SourceButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                SourceTextBox.Text = dialog.FileName;
            }
        }

        private void DestinationButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                DestinationTextBox.Text = dialog.FileName;
            }
        }
    }
}
