using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace QuickReplicate
{
    public partial class Form1 : Form
    {
        //This is open folder dialog and
        //Dialog result instance to accept the result if it is not null
        private FolderBrowserDialog dialog;
        private DialogResult result;

        //string arrays to store list of files extracted from specified directories
        string[] sourceFiles;
        string[] destinationFiles;

        //The list stores every step the user perform during the execution of the program
        //The steps will later be stored in a log file.
        List<string> logs = new List<string>();

        //Message to be displayed after every operation
        string message = null;

        public Form1()
        {
            InitializeComponent();
            //Instantiates the DialogResult
            dialog = new FolderBrowserDialog();
        }

        /// <summary>
        /// Calls the ReplicateFile method if the ValidateInput method returns true. Otherwise displays an error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplicateButton_Click(object sender, EventArgs e)
        {
            if (Validateinput())
            {
                ReplicateFiles();
                MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                ErrorLabel.Text = "All fields are required";
                ErrorLabel.Visible = true;
            }
        }

        /// <summary>
        /// Assign the selected directory to the source Texbox if the Dialog result value != null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceButton_Click(object sender, EventArgs e)
        {
            result = dialog.ShowDialog();
            if (result.Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                SourceTextBox.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Assign the selected directory to the destination Texbox if the Dialog result value != null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestinationButton_Click(object sender, EventArgs e)
        {
            result = dialog.ShowDialog();
            if (result.Equals(DialogResult.OK) && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                DestinationTextBox.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Calls the ViewLog log method to open the log file in notepad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewLogButton_Click(object sender, EventArgs e)
        {
            ViewLogfile();
        }

        /// <summary>
        /// Calls the ClearLog method and displays message to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            ClearLogFile();
            MessageBox.Show("Log file has been successfully cleared", "Cleared Log", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// This method verifies if the user has specified directories before proceeding
        /// </summary>
        /// <returns></returns>
        private bool Validateinput()
        {
            if (SourceTextBox.Text.Length > 0 && DestinationTextBox.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///     The ReplicationFiles() performs all the operations according to the requirements specified
        /// </summary>
        private void ReplicateFiles()
        {
            try
            {
                logs.Add("The following process was performed:");
                logs.Add("****************************************************************************************");

                if (SubDirectoryCheckBox.Checked)
                {
                    //Calling the IncludeSubDirectories() if the user checked the "Include subforlders" checkbox
                    IncludeSubDirectories();
                }
                else
                {
                    //The opposite of the above called method
                    ExcludeSubDirectories();
                }

                //Compares the files for similarity.
                if (sourceFiles.SequenceEqual(destinationFiles))
                {
                    // Returned if no files are the similar and none was copied or removed
                    message = "File contents are similar, no changes made.";
                }
                else
                {
                    //Deletes the files in the destination directory if "Mirror Destination" is checked. Otherwise pass
                    if (MirrorDestinationCheckBox.Checked)
                    {
                        if (destinationFiles.Length > 0)
                        {
                            logs.Add("\nList of files found and deleted in the destination path(s)");
                            logs.Add("****************************************************************************************");
                            foreach (var item in destinationFiles)
                            {
                                string file = Path.GetFileName(item);
                                File.Delete(item);

                                //Track all the deleted files
                                logs.Add(file);
                            }
                        }
                    }

                    logs.Add("\nList of files copied into the destination path");
                    logs.Add("****************************************************************************************");

                    //initializing progressBar properties
                    progressBar.Minimum = 1;
                    progressBar.Maximum = sourceFiles.Length;
                    progressBar.Value = 1;
                    progressBar.Step = 1;

                    //Gets all files in all detected directories and
                    //Copy files from source to destination directory. and progress the Progress bar to the next step
                    foreach (var item in sourceFiles)
                    {
                        string file = Path.GetFileName(item);

                        if(destinationFiles.Length > 0)
                        {
                            for (int i = 0; i < destinationFiles.Length; i++)
                            {
                                destinationFiles[i] = Path.Combine(DestinationTextBox.Text, file);
                                File.Copy(item, destinationFiles[i], true);
                            }
                        }
                        else
                        {
                            string destFile = Path.Combine(DestinationTextBox.Text, file);
                            File.Copy(item, destFile, true);
                        }

                        //Tract all the copied files
                        logs.Add(file);
                        progressBar.PerformStep();
                        Thread.Sleep(1000);
                    }

                    logs.Add("************************************End Of Process**************************************\n");

                    //Pass the List<string> log to the following method to create and write log fole.
                    WriteLogFile(logs);
                    WriteXML();

                    //Returned if the entire operation was successfull
                    message = "Files copied to destination folder";
                }
            }
            catch (Exception exeption)
            {
                MessageBox.Show($"Process stopped working: {exeption.Message}", "Process stopped", MessageBoxButtons.OK, MessageBoxIcon.Error);
                message = "Please restart the application.";
            }
        }

        /// <summary>
        /// This method gets all the sub directories in the specified directories
        /// </summary>
        private void IncludeSubDirectories()
        {
            sourceFiles = Directory.GetFiles(SourceTextBox.Text, "*.*", SearchOption.AllDirectories);
            //Checks if the destination directory exists, otherwise the directory is created
            if (!File.Exists(DestinationTextBox.Text))
            {
                var directory = Directory.CreateDirectory(DestinationTextBox.Text);
                destinationFiles = Directory.GetFiles(directory.ToString(), "*.*", SearchOption.AllDirectories);
                logs.Add($"The directory {directory.ToString()} was created");
                logs.Add($"Creation time: {directory.CreationTime}");
            }
            else
            {
                destinationFiles = Directory.GetFiles(DestinationTextBox.Text, "*.*", SearchOption.AllDirectories);
            }
        }

        /// <summary>
        /// This method only get top level directories present in the specified directories.
        /// Subdirectories are ignored.
        /// </summary>
        private void ExcludeSubDirectories()
        {
            sourceFiles = Directory.GetFiles(SourceTextBox.Text, "*.*");

            //Checks if the destination directory exists, otherwise the directory is created
            if (!File.Exists(DestinationTextBox.Text))
            {
                var directory = Directory.CreateDirectory(DestinationTextBox.Text);
                destinationFiles = Directory.GetFiles(directory.ToString(), "*.*");
                logs.Add($"The directory {directory.ToString()} was created");
                logs.Add($"Creation time: {directory.CreationTime}");
            }
            else
            {
                destinationFiles = Directory.GetFiles(DestinationTextBox.Text, "*.*");
            }
        }

        /// <summary>
        /// This method creates and writes all the functions performed by the user.
        /// </summary>
        /// <param name="logFile"></param>
        private void WriteLogFile(List<string> logFile)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter($@"{SourceTextBox.Text}\log.txt"))
                {
                    foreach (string sentence in logFile)
                    {
                        writer.WriteLine(sentence);
                    }
                    File.Encrypt($@"{SourceTextBox.Text}\log.txt");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error: {exception.Message}", "Process stopped", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This method creates xml file and store user options
        /// </summary>
        public void WriteXML()
        {
            try
            {
                using (XmlTextWriter writer = new XmlTextWriter($@"{SourceTextBox.Text}\UserOptions.xml", Encoding.UTF8))
                {
                    //Creating an xml file
                    writer.WriteStartDocument(true);
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;

                    writer.WriteStartElement("Directories");
                    //Creating source directory node
                    writer.WriteStartElement("Source");
                    writer.WriteString(SourceTextBox.Text);
                    writer.WriteEndElement();

                    //Creating destination directory node
                    writer.WriteStartElement("Destination");
                    writer.WriteString(DestinationTextBox.Text);
                    writer.WriteEndElement();

                    //Creating sub directory node
                    writer.WriteStartElement("Sub_directory");
                    writer.WriteValue(SubDirectoryCheckBox.Checked);
                    writer.WriteEndElement();

                    //Creating mirror destination node
                    writer.WriteStartElement("Mirror_destination");
                    writer.WriteValue(MirrorDestinationCheckBox.Checked);
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    //Close the document
                    writer.WriteEndDocument();

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error: {exception.Message}", "Process stopped", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// This method reads the xml file and load its data when the form loads
        /// </summary>
        public void ReadXML()
        {
            string xmlfile = "";
            //Getting the array of all directories
            //Please note: the app start searching in the downloads folder since starting in C:\ until Q513307 will generated an exception
            //if access to the is restricted by the administator
            //Replace the path with a relevant path according to your machine
            string[] files = Directory.GetFiles(@"C:\Users\Q513307\Downloads", "*", SearchOption.AllDirectories);

            foreach (var item in files)
            {
                try
                {
                    //Getting and assigning individual files
                    string file = Path.GetFileName(item);
                    if (file == "UserOptions.xml")
                    {
                        xmlfile = file;

                        using (XmlReader reader = XmlReader.Create(item))
                        {
                            while (reader.Read())
                            {
                                if (reader.IsStartElement())
                                {

                                    //Read and assign user options to the app fields
                                    switch (reader.Name.ToString())
                                    {
                                        case "Source":
                                            SourceTextBox.Text = reader.ReadElementContentAsString();
                                            break;
                                        case "Destination":
                                            DestinationTextBox.Text = reader.ReadElementContentAsString();
                                            break;
                                        case "Sub_directory":
                                            SubDirectoryCheckBox.Checked = reader.ReadElementContentAsBoolean();
                                            break;
                                        case "Mirror_destination":
                                            MirrorDestinationCheckBox.Checked = reader.ReadElementContentAsBoolean();
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Error: {exception.Message}", "Process stopped", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// This method opens the logfile in a notepad app
        /// </summary>
        private void ViewLogfile()
        {
            try
            {
                Process.Start("notepad.exe", SourceTextBox.Text + @"\log.txt");
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error: {exception.Message}", "Process stopped", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// This method clears the log file.
        /// </summary>
        private void ClearLogFile()
        {
            try
            {
                File.WriteAllText($@"{SourceTextBox.Text}\log.txt", string.Empty);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error: {exception.Message}", "Process stopped", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadXML();
        }
    }
}
