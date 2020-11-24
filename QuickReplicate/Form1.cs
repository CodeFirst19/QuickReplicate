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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.Threading;

namespace QuickReplicate
{
    public partial class Form1 : Form
    {

        private FolderBrowserDialog dialog;
        private DialogResult result;

        string message = null;

        public Form1()
        {
            InitializeComponent();
            dialog = new FolderBrowserDialog();
            
        }

        private void ReplicateButton_Click(object sender, EventArgs e)
        {
            if (Validateinput())
            {
                Initializeprocess();
            }
            else
            {
                ErrorLabel.Text = "All fields are required";
                ErrorLabel.Visible = true;
            }
        }

        private void SourceButton_Click(object sender, EventArgs e)
        {
            result = dialog.ShowDialog();
            if (result.Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                SourceTextBox.Text = dialog.SelectedPath;
            }
        }

        private void DestinationButton_Click(object sender, EventArgs e)
        {
            result = dialog.ShowDialog();
            if (result.Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                DestinationTextBox.Text = dialog.SelectedPath;
            }
        }

        private void ViewLogButton_Click(object sender, EventArgs e)
        {
            ViewLogfile();
        }

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            ClearLogFile();
            MessageBox.Show("Log file has been successfully cleared", "Cleared Log", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool Validateinput() 
        { 
            if(SourceTextBox.Text.Length>0 && DestinationTextBox.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ReplicateFiles()
        {
            string[] sourceFiles = Directory.GetFiles(SourceTextBox.Text, "*.*");
            string[] destinationFiles = Directory.GetFiles(DestinationTextBox.Text, "*.*");

            List<string> logs = new List<string>();
            logs.Add("Process performed:");

            if (!File.Exists(DestinationTextBox.Text))
            {
                var directory = Directory.CreateDirectory(SourceTextBox.Text);
                logs.Add($"The directory {SourceTextBox.Text} was created. Creation time: {directory.CreationTime}\n");
            }

            if (sourceFiles.SequenceEqual(destinationFiles))
            {
                message = "File contents are similar, no changes made.";
            }
            else
            {
                if (MirrorDestinationCheckBox.Checked)
                {
                    logs.Add("The following files in the desination directory were deleted\n");

                    foreach (var item in destinationFiles)
                    {
                        string file = Path.GetFileName(item);
                        File.Delete(item);
                        logs.Add(file);
                    }
                }

                logs.Add($"\nThe following files from the source directory were copied into the destination folder\n");
                
                progressBar.Minimum = 1;
                progressBar.Maximum = sourceFiles.Length;
                progressBar.Value = 1;
                progressBar.Step = 1;

                foreach (var item in sourceFiles)
                {
                    string file = Path.GetFileName(item);

                    for (int i = 0; i < destinationFiles.Length; i++)
                    {
                        destinationFiles[i] = Path.Combine(DestinationTextBox.Text, file);
                        File.Copy(item, destinationFiles[i], true);  
                    }
                    logs.Add(file);

                    progressBar.PerformStep();
                    Thread.Sleep(1000);
                }

                WrireLogFile(logs);
                message = "Files copied to destination folder";
            }
        }

        private void IncludeSubDirectories() { }


        private void Initializeprocess()
        {
            if (SubDirectoryCheckBox.Checked)
            {
                IncludeSubDirectories();
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ReplicateFiles();
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void WrireLogFile(List<string> logFile)
        {
            using (StreamWriter writer = new StreamWriter($@"{SourceTextBox.Text}\log.txt"))
            {
                foreach (string sentence in logFile)
                {
                    writer.WriteLine(sentence);
                }
            }
        }

        private void ViewLogfile()
        {
            Process.Start("notepad.exe", SourceTextBox.Text + @"\log.txt");
        }

        private void ClearLogFile()
        {
            File.WriteAllText($@"{SourceTextBox.Text}\log.txt", string.Empty);
        }

    }
}
