﻿namespace CHaMPWorkbench.Data
{
    partial class frmUserFeedback
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
            this.ucUserFeedback1 = new CHaMPWorkbench.Data.ucUserFeedback();
            this.SuspendLayout();
            // 
            // ucUserFeedback1
            // 
            this.ucUserFeedback1.DBCon = null;
            this.ucUserFeedback1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserFeedback1.Location = new System.Drawing.Point(0, 0);
            this.ucUserFeedback1.LogID = 0;
            this.ucUserFeedback1.MinimumSize = new System.Drawing.Size(295, 300);
            this.ucUserFeedback1.Name = "ucUserFeedback1";
            this.ucUserFeedback1.Size = new System.Drawing.Size(298, 406);
            this.ucUserFeedback1.TabIndex = 0;
            // 
            // frmUserFeedback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 406);
            this.Controls.Add(this.ucUserFeedback1);
            this.MinimumSize = new System.Drawing.Size(295, 300);
            this.Name = "frmUserFeedback";
            this.Text = "User Feedback";
            this.ResumeLayout(false);

        }

        #endregion

        private ucUserFeedback ucUserFeedback1;
    }
}