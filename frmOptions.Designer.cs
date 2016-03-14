﻿namespace CHaMPWorkbench
{
    partial class frmOptions
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOptions = new System.Windows.Forms.TextBox();
            this.cmdBrowseRBT = new System.Windows.Forms.Button();
            this.dlgBrowseExecutable = new System.Windows.Forms.OpenFileDialog();
            this.tTip = new System.Windows.Forms.ToolTip(this.components);
            this.cmdBrowseMonitoring = new System.Windows.Forms.Button();
            this.txtMonitoring = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdBrowseOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.cmdBrowseTemp = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.valGoogleMapZoom = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cmdTestAWS = new System.Windows.Forms.Button();
            this.txtStreamName = new System.Windows.Forms.TextBox();
            this.lblStreamName = new System.Windows.Forms.Label();
            this.chkAWSLoggingEnabled = new System.Windows.Forms.CheckBox();
            this.cmdBrowseGUT = new System.Windows.Forms.Button();
            this.txtGUT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.valGoogleMapZoom)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(781, 282);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(700, 282);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RBT console";
            // 
            // txtOptions
            // 
            this.txtOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOptions.Location = new System.Drawing.Point(148, 20);
            this.txtOptions.Name = "txtOptions";
            this.txtOptions.Size = new System.Drawing.Size(601, 20);
            this.txtOptions.TabIndex = 1;
            // 
            // cmdBrowseRBT
            // 
            this.cmdBrowseRBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseRBT.Location = new System.Drawing.Point(755, 19);
            this.cmdBrowseRBT.Name = "cmdBrowseRBT";
            this.cmdBrowseRBT.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseRBT.TabIndex = 2;
            this.cmdBrowseRBT.Text = "Browse";
            this.cmdBrowseRBT.UseVisualStyleBackColor = true;
            this.cmdBrowseRBT.Click += new System.EventHandler(this.cmdBrowseRBT_Click);
            // 
            // dlgBrowseExecutable
            // 
            this.dlgBrowseExecutable.Filter = "Executable Files (*.exe)|*.exe";
            // 
            // cmdBrowseMonitoring
            // 
            this.cmdBrowseMonitoring.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseMonitoring.Location = new System.Drawing.Point(755, 19);
            this.cmdBrowseMonitoring.Name = "cmdBrowseMonitoring";
            this.cmdBrowseMonitoring.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseMonitoring.TabIndex = 2;
            this.cmdBrowseMonitoring.Text = "Browse";
            this.cmdBrowseMonitoring.UseVisualStyleBackColor = true;
            this.cmdBrowseMonitoring.Click += new System.EventHandler(this.cmdBrowseMonitoring_Click);
            // 
            // txtMonitoring
            // 
            this.txtMonitoring.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonitoring.Location = new System.Drawing.Point(140, 20);
            this.txtMonitoring.Name = "txtMonitoring";
            this.txtMonitoring.Size = new System.Drawing.Size(609, 20);
            this.txtMonitoring.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Monitoring data";
            // 
            // cmdBrowseOutput
            // 
            this.cmdBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseOutput.Location = new System.Drawing.Point(755, 50);
            this.cmdBrowseOutput.Name = "cmdBrowseOutput";
            this.cmdBrowseOutput.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseOutput.TabIndex = 5;
            this.cmdBrowseOutput.Text = "Browse";
            this.cmdBrowseOutput.UseVisualStyleBackColor = true;
            this.cmdBrowseOutput.Click += new System.EventHandler(this.cmdBrowseOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(140, 51);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(609, 20);
            this.txtOutput.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Input output foler";
            // 
            // txtTemp
            // 
            this.txtTemp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemp.Location = new System.Drawing.Point(140, 82);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.Size = new System.Drawing.Size(609, 20);
            this.txtTemp.TabIndex = 6;
            // 
            // cmdBrowseTemp
            // 
            this.cmdBrowseTemp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseTemp.Location = new System.Drawing.Point(755, 81);
            this.cmdBrowseTemp.Name = "cmdBrowseTemp";
            this.cmdBrowseTemp.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseTemp.TabIndex = 7;
            this.cmdBrowseTemp.Text = "Browse";
            this.cmdBrowseTemp.UseVisualStyleBackColor = true;
            this.cmdBrowseTemp.Click += new System.EventHandler(this.cmdBrowseTemp_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Temp workspace";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Google map default zoom";
            // 
            // valGoogleMapZoom
            // 
            this.valGoogleMapZoom.Location = new System.Drawing.Point(157, 19);
            this.valGoogleMapZoom.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.valGoogleMapZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.valGoogleMapZoom.Name = "valGoogleMapZoom";
            this.valGoogleMapZoom.Size = new System.Drawing.Size(63, 20);
            this.valGoogleMapZoom.TabIndex = 1;
            this.valGoogleMapZoom.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(844, 264);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.valGoogleMapZoom);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(836, 238);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtTemp);
            this.tabPage2.Controls.Add(this.cmdBrowseTemp);
            this.tabPage2.Controls.Add(this.cmdBrowseMonitoring);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtOutput);
            this.tabPage2.Controls.Add(this.txtMonitoring);
            this.tabPage2.Controls.Add(this.cmdBrowseOutput);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(836, 238);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Folders";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cmdBrowseGUT);
            this.tabPage3.Controls.Add(this.txtGUT);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.cmdBrowseRBT);
            this.tabPage3.Controls.Add(this.txtOptions);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(836, 238);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Models";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cmdTestAWS);
            this.tabPage4.Controls.Add(this.txtStreamName);
            this.tabPage4.Controls.Add(this.lblStreamName);
            this.tabPage4.Controls.Add(this.chkAWSLoggingEnabled);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(836, 238);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Error Logging";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cmdTestAWS
            // 
            this.cmdTestAWS.Location = new System.Drawing.Point(20, 200);
            this.cmdTestAWS.Name = "cmdTestAWS";
            this.cmdTestAWS.Size = new System.Drawing.Size(156, 23);
            this.cmdTestAWS.TabIndex = 4;
            this.cmdTestAWS.Text = "Test AWS Message Log";
            this.cmdTestAWS.UseVisualStyleBackColor = true;
            this.cmdTestAWS.Visible = false;
            this.cmdTestAWS.Click += new System.EventHandler(this.cmdTestAWS_Click);
            // 
            // txtStreamName
            // 
            this.txtStreamName.Location = new System.Drawing.Point(128, 40);
            this.txtStreamName.Name = "txtStreamName";
            this.txtStreamName.ReadOnly = true;
            this.txtStreamName.Size = new System.Drawing.Size(407, 20);
            this.txtStreamName.TabIndex = 2;
            // 
            // lblStreamName
            // 
            this.lblStreamName.AutoSize = true;
            this.lblStreamName.Location = new System.Drawing.Point(46, 44);
            this.lblStreamName.Name = "lblStreamName";
            this.lblStreamName.Size = new System.Drawing.Size(77, 13);
            this.lblStreamName.TabIndex = 1;
            this.lblStreamName.Text = "Installation key";
            // 
            // chkAWSLoggingEnabled
            // 
            this.chkAWSLoggingEnabled.AutoSize = true;
            this.chkAWSLoggingEnabled.Location = new System.Drawing.Point(20, 16);
            this.chkAWSLoggingEnabled.Name = "chkAWSLoggingEnabled";
            this.chkAWSLoggingEnabled.Size = new System.Drawing.Size(261, 17);
            this.chkAWSLoggingEnabled.TabIndex = 0;
            this.chkAWSLoggingEnabled.Text = "Share status and error information with developers";
            this.chkAWSLoggingEnabled.UseVisualStyleBackColor = true;
            this.chkAWSLoggingEnabled.CheckedChanged += new System.EventHandler(this.chkAWSLoggingEnabled_CheckedChanged);
            // 
            // cmdBrowseGUT
            // 
            this.cmdBrowseGUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseGUT.Location = new System.Drawing.Point(755, 49);
            this.cmdBrowseGUT.Name = "cmdBrowseGUT";
            this.cmdBrowseGUT.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowseGUT.TabIndex = 5;
            this.cmdBrowseGUT.Text = "Browse";
            this.cmdBrowseGUT.UseVisualStyleBackColor = true;
            this.cmdBrowseGUT.Click += new System.EventHandler(this.cmdBrowseGUT_Click);
            // 
            // txtGUT
            // 
            this.txtGUT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGUT.Location = new System.Drawing.Point(148, 50);
            this.txtGUT.Name = "txtGUT";
            this.txtGUT.Size = new System.Drawing.Size(601, 20);
            this.txtGUT.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Geomorphic unit tool (GUT)";
            // 
            // frmOptions
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(868, 317);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(416, 300);
            this.Name = "frmOptions";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.valGoogleMapZoom)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOptions;
        private System.Windows.Forms.Button cmdBrowseRBT;
        private System.Windows.Forms.OpenFileDialog dlgBrowseExecutable;
        private System.Windows.Forms.ToolTip tTip;
        private System.Windows.Forms.Button cmdBrowseMonitoring;
        private System.Windows.Forms.TextBox txtMonitoring;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdBrowseOutput;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.Button cmdBrowseTemp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown valGoogleMapZoom;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtStreamName;
        private System.Windows.Forms.Label lblStreamName;
        private System.Windows.Forms.CheckBox chkAWSLoggingEnabled;
        private System.Windows.Forms.Button cmdTestAWS;
        private System.Windows.Forms.Button cmdBrowseGUT;
        private System.Windows.Forms.TextBox txtGUT;
        private System.Windows.Forms.Label label2;
    }
}