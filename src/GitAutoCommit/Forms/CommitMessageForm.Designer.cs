using System.Linq;
using System;

namespace GitAutoCommit.Forms
{
	partial class CommitMessageForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.commitMessageTextBox = new System.Windows.Forms.TextBox();
			this.prependAutoCommitCheckbox = new System.Windows.Forms.CheckBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(170, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter your commit message below.";
			// 
			// commitMessageTextBox
			// 
			this.commitMessageTextBox.AcceptsReturn = true;
			this.commitMessageTextBox.AcceptsTab = true;
			this.commitMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.commitMessageTextBox.Location = new System.Drawing.Point(23, 43);
			this.commitMessageTextBox.Multiline = true;
			this.commitMessageTextBox.Name = "commitMessageTextBox";
			this.commitMessageTextBox.Size = new System.Drawing.Size(601, 398);
			this.commitMessageTextBox.TabIndex = 1;
			// 
			// prependAutoCommitCheckbox
			// 
			this.prependAutoCommitCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.prependAutoCommitCheckbox.AutoSize = true;
			this.prependAutoCommitCheckbox.Checked = true;
			this.prependAutoCommitCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.prependAutoCommitCheckbox.Location = new System.Drawing.Point(23, 447);
			this.prependAutoCommitCheckbox.Name = "prependAutoCommitCheckbox";
			this.prependAutoCommitCheckbox.Size = new System.Drawing.Size(145, 17);
			this.prependAutoCommitCheckbox.TabIndex = 2;
			this.prependAutoCommitCheckbox.Text = "Prepend AUTO COMMIT";
			this.prependAutoCommitCheckbox.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(468, 461);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 25);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(549, 461);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 25);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// CommitMessageForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(647, 507);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.prependAutoCommitCheckbox);
			this.Controls.Add(this.commitMessageTextBox);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CommitMessageForm";
			this.Padding = new System.Windows.Forms.Padding(20);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Git auto commit: Set commit message";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox commitMessageTextBox;
		private System.Windows.Forms.CheckBox prependAutoCommitCheckbox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}