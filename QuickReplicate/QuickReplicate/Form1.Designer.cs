﻿
namespace QuickReplicate
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SubDirectoryCheckBox = new System.Windows.Forms.CheckBox();
            this.MirrorDestinationCheckBox = new System.Windows.Forms.CheckBox();
            this.ViewLogButton = new System.Windows.Forms.Button();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.ReplicateButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.SourceTextBox = new System.Windows.Forms.TextBox();
            this.DestinationTextBox = new System.Windows.Forms.TextBox();
            this.SourceButton = new System.Windows.Forms.Button();
            this.DestinationButton = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination";
            // 
            // SubDirectoryCheckBox
            // 
            this.SubDirectoryCheckBox.AutoSize = true;
            this.SubDirectoryCheckBox.Checked = true;
            this.SubDirectoryCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SubDirectoryCheckBox.Location = new System.Drawing.Point(78, 182);
            this.SubDirectoryCheckBox.Name = "SubDirectoryCheckBox";
            this.SubDirectoryCheckBox.Size = new System.Drawing.Size(147, 19);
            this.SubDirectoryCheckBox.TabIndex = 3;
            this.SubDirectoryCheckBox.Text = "Include Sub Directories";
            this.SubDirectoryCheckBox.UseVisualStyleBackColor = true;
            // 
            // MirrorDestinationCheckBox
            // 
            this.MirrorDestinationCheckBox.AutoSize = true;
            this.MirrorDestinationCheckBox.Location = new System.Drawing.Point(557, 182);
            this.MirrorDestinationCheckBox.Name = "MirrorDestinationCheckBox";
            this.MirrorDestinationCheckBox.Size = new System.Drawing.Size(175, 19);
            this.MirrorDestinationCheckBox.TabIndex = 4;
            this.MirrorDestinationCheckBox.Text = "Mirror Destination to Source";
            this.MirrorDestinationCheckBox.UseVisualStyleBackColor = true;
            // 
            // ViewLogButton
            // 
            this.ViewLogButton.Location = new System.Drawing.Point(78, 226);
            this.ViewLogButton.Name = "ViewLogButton";
            this.ViewLogButton.Size = new System.Drawing.Size(75, 23);
            this.ViewLogButton.TabIndex = 5;
            this.ViewLogButton.Text = "View Log";
            this.ViewLogButton.UseVisualStyleBackColor = true;
            this.ViewLogButton.Click += new System.EventHandler(this.ViewLogButton_Click);
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.Location = new System.Drawing.Point(174, 225);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(75, 23);
            this.ClearLogButton.TabIndex = 6;
            this.ClearLogButton.Text = "Clear log";
            this.ClearLogButton.UseVisualStyleBackColor = true;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // ReplicateButton
            // 
            this.ReplicateButton.Location = new System.Drawing.Point(557, 225);
            this.ReplicateButton.Name = "ReplicateButton";
            this.ReplicateButton.Size = new System.Drawing.Size(75, 23);
            this.ReplicateButton.TabIndex = 7;
            this.ReplicateButton.Text = "Replicate";
            this.ReplicateButton.UseVisualStyleBackColor = true;
            this.ReplicateButton.Click += new System.EventHandler(this.ReplicateButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(668, 225);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(93, 23);
            this.StopButton.TabIndex = 8;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // SourceTextBox
            // 
            this.SourceTextBox.Enabled = false;
            this.SourceTextBox.Location = new System.Drawing.Point(97, 69);
            this.SourceTextBox.Name = "SourceTextBox";
            this.SourceTextBox.Size = new System.Drawing.Size(535, 23);
            this.SourceTextBox.TabIndex = 9;
            // 
            // DestinationTextBox
            // 
            this.DestinationTextBox.Location = new System.Drawing.Point(97, 128);
            this.DestinationTextBox.Name = "DestinationTextBox";
            this.DestinationTextBox.Size = new System.Drawing.Size(535, 23);
            this.DestinationTextBox.TabIndex = 10;
            // 
            // SourceButton
            // 
            this.SourceButton.Location = new System.Drawing.Point(668, 69);
            this.SourceButton.Name = "SourceButton";
            this.SourceButton.Size = new System.Drawing.Size(93, 23);
            this.SourceButton.TabIndex = 11;
            this.SourceButton.Text = "Load Directory";
            this.SourceButton.UseVisualStyleBackColor = true;
            this.SourceButton.Click += new System.EventHandler(this.SourceButton_Click);
            // 
            // DestinationButton
            // 
            this.DestinationButton.Location = new System.Drawing.Point(668, 127);
            this.DestinationButton.Name = "DestinationButton";
            this.DestinationButton.Size = new System.Drawing.Size(93, 23);
            this.DestinationButton.TabIndex = 12;
            this.DestinationButton.Text = "Load Directory";
            this.DestinationButton.UseVisualStyleBackColor = true;
            this.DestinationButton.Click += new System.EventHandler(this.DestinationButton_Click);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(97, 275);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(32, 15);
            this.ErrorLabel.TabIndex = 13;
            this.ErrorLabel.Text = "Error";
            this.ErrorLabel.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 12);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(749, 35);
            this.progressBar.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 317);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.DestinationButton);
            this.Controls.Add(this.SourceButton);
            this.Controls.Add(this.DestinationTextBox);
            this.Controls.Add(this.SourceTextBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.ReplicateButton);
            this.Controls.Add(this.ClearLogButton);
            this.Controls.Add(this.ViewLogButton);
            this.Controls.Add(this.MirrorDestinationCheckBox);
            this.Controls.Add(this.SubDirectoryCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Replicate";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox SubDirectoryCheckBox;
        private System.Windows.Forms.CheckBox MirrorDestinationCheckBox;
        private System.Windows.Forms.Button ViewLogButton;
        private System.Windows.Forms.Button ClearLogButton;
        private System.Windows.Forms.Button ReplicateButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox SourceTextBox;
        private System.Windows.Forms.TextBox DestinationTextBox;
        private System.Windows.Forms.Button SourceButton;
        private System.Windows.Forms.Button DestinationButton;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

