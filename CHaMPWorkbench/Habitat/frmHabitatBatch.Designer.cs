﻿namespace CHaMPWorkbench.Habitat
{
    partial class frmHabitatBatch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.chkFieldSeasons = new System.Windows.Forms.CheckedListBox();
            this.cmsSelectAllNone = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectNoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkWatersheds = new System.Windows.Forms.CheckedListBox();
            this.tabFilter = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkVisitTypes = new System.Windows.Forms.CheckedListBox();
            this.tabSpecies = new System.Windows.Forms.TabPage();
            this.chkSpecies = new System.Windows.Forms.CheckedListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.chkPrimary = new System.Windows.Forms.CheckBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdSelectNone = new System.Windows.Forms.Button();
            this.cmdSelectAll = new System.Windows.Forms.Button();
            this.grdVisits = new System.Windows.Forms.DataGridView();
            this.colSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFieldSeason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWatershed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Visitcol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkModels = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtD50RasterFileName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtD50TopLevel = new System.Windows.Forms.TextBox();
            this.cmdD50TopLevel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMonitoringFolder = new System.Windows.Forms.TextBox();
            this.cmdBrowseMonitoringDataFolder = new System.Windows.Forms.Button();
            this.cmdHabitatModelDB = new System.Windows.Forms.Button();
            this.txtHabitatModelDB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bindingSourceSelectedVisits = new System.Windows.Forms.BindingSource(this.components);
            this.cmdHelp = new System.Windows.Forms.Button();
            this.cmdFilterSelectNone = new System.Windows.Forms.Button();
            this.cmdFilterSelectAll = new System.Windows.Forms.Button();
            this.cmsSelectAllNone.SuspendLayout();
            this.tabFilter.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabSpecies.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisits)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSelectedVisits)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(607, 569);
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
            this.cmdOK.Location = new System.Drawing.Point(526, 569);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // chkFieldSeasons
            // 
            this.chkFieldSeasons.CheckOnClick = true;
            this.chkFieldSeasons.ContextMenuStrip = this.cmsSelectAllNone;
            this.chkFieldSeasons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkFieldSeasons.FormattingEnabled = true;
            this.chkFieldSeasons.Location = new System.Drawing.Point(3, 3);
            this.chkFieldSeasons.Name = "chkFieldSeasons";
            this.chkFieldSeasons.Size = new System.Drawing.Size(626, 157);
            this.chkFieldSeasons.TabIndex = 2;
            this.chkFieldSeasons.SelectedIndexChanged += new System.EventHandler(this.FilterVisits);
            // 
            // cmsSelectAllNone
            // 
            this.cmsSelectAllNone.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.selectNoneToolStripMenuItem});
            this.cmsSelectAllNone.Name = "cmsSelectAllNone";
            this.cmsSelectAllNone.Size = new System.Drawing.Size(138, 48);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllNone);
            // 
            // selectNoneToolStripMenuItem
            // 
            this.selectNoneToolStripMenuItem.Name = "selectNoneToolStripMenuItem";
            this.selectNoneToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.selectNoneToolStripMenuItem.Text = "Select None";
            this.selectNoneToolStripMenuItem.Click += new System.EventHandler(this.SelectAllNone);
            // 
            // chkWatersheds
            // 
            this.chkWatersheds.CheckOnClick = true;
            this.chkWatersheds.ContextMenuStrip = this.cmsSelectAllNone;
            this.chkWatersheds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkWatersheds.FormattingEnabled = true;
            this.chkWatersheds.Location = new System.Drawing.Point(3, 3);
            this.chkWatersheds.Name = "chkWatersheds";
            this.chkWatersheds.Size = new System.Drawing.Size(626, 157);
            this.chkWatersheds.TabIndex = 5;
            this.chkWatersheds.SelectedIndexChanged += new System.EventHandler(this.FilterVisits);
            // 
            // tabFilter
            // 
            this.tabFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFilter.Controls.Add(this.tabPage1);
            this.tabFilter.Controls.Add(this.tabPage2);
            this.tabFilter.Controls.Add(this.tabPage3);
            this.tabFilter.Controls.Add(this.tabSpecies);
            this.tabFilter.Controls.Add(this.tabPage4);
            this.tabFilter.Location = new System.Drawing.Point(6, 29);
            this.tabFilter.Name = "tabFilter";
            this.tabFilter.SelectedIndex = 0;
            this.tabFilter.Size = new System.Drawing.Size(640, 189);
            this.tabFilter.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkFieldSeasons);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(632, 163);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Field Seasons";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkWatersheds);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(632, 163);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Watersheds";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chkVisitTypes);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(632, 163);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Visit Types";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // chkVisitTypes
            // 
            this.chkVisitTypes.CheckOnClick = true;
            this.chkVisitTypes.ContextMenuStrip = this.cmsSelectAllNone;
            this.chkVisitTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkVisitTypes.FormattingEnabled = true;
            this.chkVisitTypes.Location = new System.Drawing.Point(3, 3);
            this.chkVisitTypes.Name = "chkVisitTypes";
            this.chkVisitTypes.Size = new System.Drawing.Size(626, 157);
            this.chkVisitTypes.TabIndex = 0;
            this.chkVisitTypes.SelectedIndexChanged += new System.EventHandler(this.FilterVisits);
            // 
            // tabSpecies
            // 
            this.tabSpecies.Controls.Add(this.chkSpecies);
            this.tabSpecies.Location = new System.Drawing.Point(4, 22);
            this.tabSpecies.Name = "tabSpecies";
            this.tabSpecies.Padding = new System.Windows.Forms.Padding(3);
            this.tabSpecies.Size = new System.Drawing.Size(632, 173);
            this.tabSpecies.TabIndex = 4;
            this.tabSpecies.Text = "Species Present";
            this.tabSpecies.UseVisualStyleBackColor = true;
            // 
            // chkSpecies
            // 
            this.chkSpecies.CheckOnClick = true;
            this.chkSpecies.ContextMenuStrip = this.cmsSelectAllNone;
            this.chkSpecies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSpecies.FormattingEnabled = true;
            this.chkSpecies.Location = new System.Drawing.Point(3, 3);
            this.chkSpecies.Name = "chkSpecies";
            this.chkSpecies.Size = new System.Drawing.Size(626, 167);
            this.chkSpecies.TabIndex = 0;
            this.chkSpecies.SelectedIndexChanged += new System.EventHandler(this.FilterVisits);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.chkPrimary);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(632, 173);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Other Criteria";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // chkPrimary
            // 
            this.chkPrimary.AutoSize = true;
            this.chkPrimary.Location = new System.Drawing.Point(7, 11);
            this.chkPrimary.Name = "chkPrimary";
            this.chkPrimary.Size = new System.Drawing.Size(130, 17);
            this.chkPrimary.TabIndex = 2;
            this.chkPrimary.Text = "Must be a primary visit";
            this.chkPrimary.UseVisualStyleBackColor = true;
            this.chkPrimary.CheckedChanged += new System.EventHandler(this.FilterVisits);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Location = new System.Drawing.Point(12, 12);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(672, 547);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox2);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(664, 521);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Visits";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cmdSelectNone);
            this.groupBox2.Controls.Add(this.cmdSelectAll);
            this.groupBox2.Controls.Add(this.grdVisits);
            this.groupBox2.Location = new System.Drawing.Point(6, 236);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(652, 282);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Selected Visits";
            // 
            // cmdSelectNone
            // 
            this.cmdSelectNone.Location = new System.Drawing.Point(490, 13);
            this.cmdSelectNone.Name = "cmdSelectNone";
            this.cmdSelectNone.Size = new System.Drawing.Size(75, 23);
            this.cmdSelectNone.TabIndex = 0;
            this.cmdSelectNone.Text = "Select None";
            this.cmdSelectNone.UseVisualStyleBackColor = true;
            this.cmdSelectNone.Click += new System.EventHandler(this.cmdSelectNone_Click);
            // 
            // cmdSelectAll
            // 
            this.cmdSelectAll.Location = new System.Drawing.Point(571, 13);
            this.cmdSelectAll.Name = "cmdSelectAll";
            this.cmdSelectAll.Size = new System.Drawing.Size(75, 23);
            this.cmdSelectAll.TabIndex = 1;
            this.cmdSelectAll.Text = "Select All";
            this.cmdSelectAll.UseVisualStyleBackColor = true;
            this.cmdSelectAll.Click += new System.EventHandler(this.cmdSelectAll_Click);
            // 
            // grdVisits
            // 
            this.grdVisits.AllowUserToAddRows = false;
            this.grdVisits.AllowUserToDeleteRows = false;
            this.grdVisits.AllowUserToResizeRows = false;
            this.grdVisits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdVisits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grdVisits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdVisits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelected,
            this.colFieldSeason,
            this.colWatershed,
            this.colSite,
            this.Visitcol});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdVisits.DefaultCellStyle = dataGridViewCellStyle8;
            this.grdVisits.Location = new System.Drawing.Point(6, 42);
            this.grdVisits.MultiSelect = false;
            this.grdVisits.Name = "grdVisits";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdVisits.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.grdVisits.RowHeadersVisible = false;
            this.grdVisits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdVisits.Size = new System.Drawing.Size(640, 237);
            this.grdVisits.TabIndex = 2;
            // 
            // colSelected
            // 
            this.colSelected.DataPropertyName = "Selected";
            this.colSelected.HeaderText = "";
            this.colSelected.Name = "colSelected";
            this.colSelected.Width = 30;
            // 
            // colFieldSeason
            // 
            this.colFieldSeason.DataPropertyName = "FieldSeason";
            this.colFieldSeason.HeaderText = "Season";
            this.colFieldSeason.Name = "colFieldSeason";
            this.colFieldSeason.ReadOnly = true;
            this.colFieldSeason.Width = 50;
            // 
            // colWatershed
            // 
            this.colWatershed.DataPropertyName = "WatershedName";
            this.colWatershed.HeaderText = "Watershed";
            this.colWatershed.Name = "colWatershed";
            this.colWatershed.ReadOnly = true;
            this.colWatershed.Width = 175;
            // 
            // colSite
            // 
            this.colSite.DataPropertyName = "SiteName";
            this.colSite.HeaderText = "Site";
            this.colSite.Name = "colSite";
            this.colSite.ReadOnly = true;
            this.colSite.Width = 200;
            // 
            // Visitcol
            // 
            this.Visitcol.DataPropertyName = "VisitID";
            this.Visitcol.HeaderText = "Visit ID";
            this.Visitcol.Name = "Visitcol";
            this.Visitcol.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmdFilterSelectNone);
            this.groupBox1.Controls.Add(this.cmdFilterSelectAll);
            this.groupBox1.Controls.Add(this.tabFilter);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 224);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visit Filters";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox5);
            this.tabPage6.Controls.Add(this.groupBox4);
            this.tabPage6.Controls.Add(this.groupBox3);
            this.tabPage6.Controls.Add(this.cmdHabitatModelDB);
            this.tabPage6.Controls.Add(this.txtHabitatModelDB);
            this.tabPage6.Controls.Add(this.label3);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(664, 521);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Habitat Model";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.chkModels);
            this.groupBox5.Location = new System.Drawing.Point(10, 43);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(639, 162);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Habitat Models to Configure";
            // 
            // chkModels
            // 
            this.chkModels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkModels.CheckOnClick = true;
            this.chkModels.FormattingEnabled = true;
            this.chkModels.Location = new System.Drawing.Point(6, 19);
            this.chkModels.Name = "chkModels";
            this.chkModels.Size = new System.Drawing.Size(627, 124);
            this.chkModels.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtD50RasterFileName);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.txtD50TopLevel);
            this.groupBox4.Controls.Add(this.cmdD50TopLevel);
            this.groupBox4.Location = new System.Drawing.Point(10, 330);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(639, 143);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "D50 Substrate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "D50 raster file name";
            // 
            // txtD50RasterFileName
            // 
            this.txtD50RasterFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtD50RasterFileName.Location = new System.Drawing.Point(123, 92);
            this.txtD50RasterFileName.Name = "txtD50RasterFileName";
            this.txtD50RasterFileName.Size = new System.Drawing.Size(179, 20);
            this.txtD50RasterFileName.TabIndex = 7;
            this.txtD50RasterFileName.Text = "D50.tif";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Top level folder";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(617, 32);
            this.label5.TabIndex = 0;
            this.label5.Text = "Specify the top level folder that contains the D50 substrate rasters. The folder " +
    "that you specify will have Year\\Watershed\\Site\\VISIT_xxxx\\Topo added to it.";
            // 
            // txtD50TopLevel
            // 
            this.txtD50TopLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtD50TopLevel.Location = new System.Drawing.Point(123, 66);
            this.txtD50TopLevel.Name = "txtD50TopLevel";
            this.txtD50TopLevel.Size = new System.Drawing.Size(482, 20);
            this.txtD50TopLevel.TabIndex = 1;
            // 
            // cmdD50TopLevel
            // 
            this.cmdD50TopLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdD50TopLevel.Image = global::CHaMPWorkbench.Properties.Resources.BrowseFolder;
            this.cmdD50TopLevel.Location = new System.Drawing.Point(610, 65);
            this.cmdD50TopLevel.Name = "cmdD50TopLevel";
            this.cmdD50TopLevel.Size = new System.Drawing.Size(23, 23);
            this.cmdD50TopLevel.TabIndex = 2;
            this.cmdD50TopLevel.UseVisualStyleBackColor = true;
            this.cmdD50TopLevel.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtMonitoringFolder);
            this.groupBox3.Controls.Add(this.cmdBrowseMonitoringDataFolder);
            this.groupBox3.Location = new System.Drawing.Point(10, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(639, 100);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hydraulic Model Results";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(617, 32);
            this.label4.TabIndex = 0;
            this.label4.Text = "Specify the top level folder that contains the hydraulic model results. The folde" +
    "r that you specify will have Year\\Watershed\\Site\\VISIT_xxxx\\Hydro\\HydroModelResu" +
    "lts added to it.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Top level folder";
            // 
            // txtMonitoringFolder
            // 
            this.txtMonitoringFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonitoringFolder.Location = new System.Drawing.Point(123, 66);
            this.txtMonitoringFolder.Name = "txtMonitoringFolder";
            this.txtMonitoringFolder.Size = new System.Drawing.Size(482, 20);
            this.txtMonitoringFolder.TabIndex = 2;
            this.txtMonitoringFolder.TextChanged += new System.EventHandler(this.txtMonitoringFolder_TextChanged);
            // 
            // cmdBrowseMonitoringDataFolder
            // 
            this.cmdBrowseMonitoringDataFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseMonitoringDataFolder.Image = global::CHaMPWorkbench.Properties.Resources.BrowseFolder;
            this.cmdBrowseMonitoringDataFolder.Location = new System.Drawing.Point(610, 65);
            this.cmdBrowseMonitoringDataFolder.Name = "cmdBrowseMonitoringDataFolder";
            this.cmdBrowseMonitoringDataFolder.Size = new System.Drawing.Size(23, 23);
            this.cmdBrowseMonitoringDataFolder.TabIndex = 3;
            this.cmdBrowseMonitoringDataFolder.UseVisualStyleBackColor = true;
            this.cmdBrowseMonitoringDataFolder.Click += new System.EventHandler(this.cmdBrowseMonitoringDataFolder_Click);
            // 
            // cmdHabitatModelDB
            // 
            this.cmdHabitatModelDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHabitatModelDB.Image = global::CHaMPWorkbench.Properties.Resources.BrowseFolder;
            this.cmdHabitatModelDB.Location = new System.Drawing.Point(626, 14);
            this.cmdHabitatModelDB.Name = "cmdHabitatModelDB";
            this.cmdHabitatModelDB.Size = new System.Drawing.Size(23, 23);
            this.cmdHabitatModelDB.TabIndex = 2;
            this.cmdHabitatModelDB.UseVisualStyleBackColor = true;
            this.cmdHabitatModelDB.Click += new System.EventHandler(this.cmdHabitatModelDB_Click);
            // 
            // txtHabitatModelDB
            // 
            this.txtHabitatModelDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHabitatModelDB.Location = new System.Drawing.Point(133, 15);
            this.txtHabitatModelDB.Name = "txtHabitatModelDB";
            this.txtHabitatModelDB.Size = new System.Drawing.Size(487, 20);
            this.txtHabitatModelDB.TabIndex = 1;
            this.txtHabitatModelDB.TextChanged += new System.EventHandler(this.txtHabitatModelDB_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Habitat project file";
            // 
            // cmdHelp
            // 
            this.cmdHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdHelp.Location = new System.Drawing.Point(12, 569);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(75, 23);
            this.cmdHelp.TabIndex = 3;
            this.cmdHelp.Text = "Help";
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // cmdFilterSelectNone
            // 
            this.cmdFilterSelectNone.Location = new System.Drawing.Point(483, 11);
            this.cmdFilterSelectNone.Name = "cmdFilterSelectNone";
            this.cmdFilterSelectNone.Size = new System.Drawing.Size(75, 23);
            this.cmdFilterSelectNone.TabIndex = 3;
            this.cmdFilterSelectNone.Text = "Select None";
            this.cmdFilterSelectNone.UseVisualStyleBackColor = true;
            this.cmdFilterSelectNone.Click += new System.EventHandler(this.cmdFilterSelectNone_Click);
            // 
            // cmdFilterSelectAll
            // 
            this.cmdFilterSelectAll.Location = new System.Drawing.Point(564, 11);
            this.cmdFilterSelectAll.Name = "cmdFilterSelectAll";
            this.cmdFilterSelectAll.Size = new System.Drawing.Size(75, 23);
            this.cmdFilterSelectAll.TabIndex = 4;
            this.cmdFilterSelectAll.Text = "Select All";
            this.cmdFilterSelectAll.UseVisualStyleBackColor = true;
            this.cmdFilterSelectAll.Click += new System.EventHandler(this.cmdFilterSelectAll_Click);
            // 
            // frmHabitatBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 604);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.MinimumSize = new System.Drawing.Size(710, 642);
            this.Name = "frmHabitatBatch";
            this.Text = "Create Habitat Model Batch Run";
            this.Load += new System.EventHandler(this.frmHabitatBatch_Load);
            this.cmsSelectAllNone.ResumeLayout(false);
            this.tabFilter.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabSpecies.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdVisits)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSelectedVisits)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.CheckedListBox chkFieldSeasons;
        private System.Windows.Forms.CheckedListBox chkWatersheds;
        private System.Windows.Forms.TabControl tabFilter;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckedListBox chkVisitTypes;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox chkPrimary;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView grdVisits;
        private System.Windows.Forms.BindingSource bindingSourceSelectedVisits;
        private System.Windows.Forms.Button cmdHabitatModelDB;
        private System.Windows.Forms.TextBox txtHabitatModelDB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdBrowseMonitoringDataFolder;
        private System.Windows.Forms.TextBox txtMonitoringFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdSelectNone;
        private System.Windows.Forms.Button cmdSelectAll;
        private System.Windows.Forms.TabPage tabSpecies;
        private System.Windows.Forms.CheckedListBox chkSpecies;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtD50TopLevel;
        private System.Windows.Forms.Button cmdD50TopLevel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox chkModels;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtD50RasterFileName;
        private System.Windows.Forms.ContextMenuStrip cmsSelectAllNone;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectNoneToolStripMenuItem;
        private System.Windows.Forms.Button cmdHelp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldSeason;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWatershed;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSite;
        private System.Windows.Forms.DataGridViewTextBoxColumn Visitcol;
        private System.Windows.Forms.Button cmdFilterSelectNone;
        private System.Windows.Forms.Button cmdFilterSelectAll;
    }
}