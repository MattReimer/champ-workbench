﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace CHaMPWorkbench.Data
{
    public partial class frmCustomVisit : Form
    {
        // Names of the fields in the survey GDB channel units feature class
        private const string SurveyGDB_ChannelUnitsTableName = "Channel_Units";
        private const string SurveyGDB_UnitNumberField = "Unit_Number";
        private const string SurveyGDB_SegmentField = "Segment_Number";
        private const string SurveyGDB_Tier1Field = "Tier1";
        private const string SurveyGDB_Tier2Field = "Tier2";

        // Used if the survey GDB channel units are missing tier information and the
        // user chooses to populate with random tier types
        private Random RandomNumber;

        public string DBCon { get; internal set; }

        // This is the list of channel units bound to the data grid view.
        private BindingList<ChannelUnit> bsChannelUnits;

        // Make the visit ID publicly available to the parent form so that
        // it can select it in the main grid after the insert.
        public int VisitID { get { return (int)valVisitID.Value; } }

        public frmCustomVisit(string sDBCon)
        {
            InitializeComponent();
            DBCon = sDBCon;
            RandomNumber = new Random(DateTime.Now.Second);

            bsChannelUnits = new BindingList<ChannelUnit>();
        }

        private void frmCustomVisit_Load(object sender, EventArgs e)
        {
            naru.db.sqlite.NamedObject.LoadComboWithListItems(ref cboProtocol, DBCon, "SELECT ItemID, Title FROM LookupListItems WHERE ListID = 8 ORDER BY Title");
            naru.db.sqlite.NamedObject.LoadComboWithListItems(ref cboWatershed, DBCon, "SELECT WatershedID, WatershedName FROM CHaMP_Watersheds ORDER BY WatershedName");
            LoadPrograms();

            if (DateTime.Now.Year >= valFieldSeason.Minimum && DateTime.Now.Year <= valFieldSeason.Maximum)
                valFieldSeason.Value = DateTime.Now.Year;

            // Set the visit ID to the next largest available visit ID
            // Check the Visit ID doesn't already exist
            using (SQLiteConnection dbCon = new SQLiteConnection(DBCon))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand("SELECT Max(VisitID) FROM CHaMP_Visits", dbCon);
                object obj = dbCom.ExecuteScalar();
                if (obj != null && obj is Int32)
                {
                    int nMaxVisitID = (int)obj;
                    valVisitID.Value = Math.Max(valVisitID.Minimum, nMaxVisitID + 1);
                }
            }

            // Populate the data grid tier columns with the tier names
            LoadDataGridComboBox(grdChannelUnits.Columns["colTier1"], "Tier1");
            LoadDataGridComboBox(grdChannelUnits.Columns["colTier2"], "Tier2");

            grdChannelUnits.DataSource = bsChannelUnits;
        }

        private void LoadDataGridComboBox(DataGridViewColumn theCol, string sWorkbenchColName)
        {
            DataGridViewComboBoxColumn cboCol = (DataGridViewComboBoxColumn)theCol;

            using (SQLiteConnection dbCon = new SQLiteConnection(DBCon))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand(string.Format("SELECT {0} FROM CHAMP_ChannelUnits GROUP BY {0}", sWorkbenchColName), dbCon);
                SQLiteDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                    cboCol.Items.Add(dbRead[0]);
            }
        }

        private void cboWatershed_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            cboSite.Items.Clear();
            if (cboWatershed.SelectedItem is naru.db.NamedObject)
            {
                using (SQLiteConnection dbCon = new SQLiteConnection(DBCon))
                {
                    dbCon.Open();
                    SQLiteCommand dbCom = new SQLiteCommand("SELECT SiteID, SiteName FROM CHaMP_Sites WHERE WatershedID = @WatershedID ORDER BY SiteName", dbCon);
                    dbCom.Parameters.AddWithValue("@WatershedID", ((naru.db.NamedObject)cboWatershed.SelectedItem).ID);
                    SQLiteDataReader dbRead = dbCom.ExecuteReader();
                    while (dbRead.Read())
                        cboSite.Items.Add(new naru.db.NamedObject(dbRead.GetInt64(dbRead.GetOrdinal("SiteID")), dbRead.GetString(dbRead.GetOrdinal("SiteName"))));
                }
            }

            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        private bool ValidateForm()
        {
            // Check the Visit ID doesn't already exist
            using (SQLiteConnection dbCon = new SQLiteConnection(DBCon))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand("SELECT VisitID FROM CHaMP_Visits WHERE VisitID = @VisitID", dbCon);
                dbCom.Parameters.AddWithValue("@VisitID", (int)valVisitID.Value);
                object obj = dbCom.ExecuteScalar();
                if (obj != null && obj is Int64)
                {
                    MessageBox.Show(string.Format("A visit already exists with the visit ID {0}", valVisitID.Value), CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(cboWatershed.Text))
            {
                MessageBox.Show("The custom visit must be associated with a watershed. Either select an existing watershed or enter a placeholder name if you do not have one.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrEmpty(cboSite.Text))
            {
                MessageBox.Show("The custom visit must be associated with a site. Either select an existing site or enter a placeholder name if you do not have one.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cboProtocol.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a protocol for the custom visit.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (cboProgram.SelectedIndex < 0)
            {
                MessageBox.Show("You must select a program for the custom visit.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (bsChannelUnits.Count < 1)
            {
                switch (MessageBox.Show("If you proceed and create this visit without any channel units then it will not be possible to use this visit with the RBT or batch substrate builder. Do you want to proceed and create this visit without channel units?", "No Channel Units Defined", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.No:
                        return false;
                }
            }

            return true;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            using (SQLiteConnection dbCon = new SQLiteConnection(DBCon))
            {
                dbCon.Open();
                SQLiteTransaction dbTrans = dbCon.BeginTransaction();

                try
                {
                    long nWatershedID = 0;
                    if (cboWatershed.SelectedItem is naru.db.NamedObject)
                        nWatershedID = ((naru.db.NamedObject)cboWatershed.SelectedItem).ID;
                    else
                    {
                        // Watershed ID is not auto-increment.
                        SQLiteCommand dbCom = new SQLiteCommand("INSERT INTO CHaMP_Watersheds (WatershedName) VALUES (@WatershedName)", dbCon, dbTrans);
                        dbCom.Parameters.AddWithValue("@WatershedName", cboWatershed.Text);
                        if (dbCom.ExecuteNonQuery() != 1)
                            throw new Exception("Failed to create new watershed");

                        dbCom = new SQLiteCommand("SELECT last_insert_rowid()", dbTrans.Connection, dbTrans);
                        nWatershedID = (long)dbCom.ExecuteScalar();
                    }

                    long nSiteID = 0;
                    if (cboSite.SelectedItem is naru.db.NamedObject)
                        nSiteID = ((naru.db.NamedObject)cboSite.SelectedItem).ID;
                    else
                    {
                        SQLiteCommand dbCom = new SQLiteCommand("INSERT INTO CHaMP_Sites (SiteName, WatershedID) VALUES (@SiteName, @WatershedID)", dbCon, dbTrans);
                        dbCom.Parameters.AddWithValue("@SiteName", cboSite.Text);
                        dbCom.Parameters.AddWithValue("@WatershedID", nWatershedID);
                        if (dbCom.ExecuteNonQuery() != 1)
                            throw new Exception("Failed to create new site");

                        dbCom = new SQLiteCommand("SELECT last_insert_rowid()", dbTrans.Connection, dbTrans);
                        nSiteID = (long)dbCom.ExecuteScalar();
                    }

                    SQLiteCommand comVisit = new SQLiteCommand("INSERT INTO CHaMP_Visits (VisitID, SiteID, VisitYear, ProtocolID, Organization, Remarks, ProgramID) VALUES (@VisitID, @SiteID, @VisitYear, @ProtocolID, @Organization, @Remarks, @ProgramID)", dbCon, dbTrans);
                    comVisit.Parameters.AddWithValue("@VisitID", (long)valVisitID.Value);
                    comVisit.Parameters.AddWithValue("@SiteID", nSiteID);
                    comVisit.Parameters.AddWithValue("@VisitYear", (long)valFieldSeason.Value);
                    comVisit.Parameters.AddWithValue("@ProtocolID", ((naru.db.NamedObject)cboProtocol.SelectedItem).ID);
                    comVisit.Parameters.AddWithValue("@ProgramID", ((naru.db.NamedObject)cboProgram.SelectedItem).ID);
                    SQLiteParameter pOrganization = comVisit.Parameters.Add("@Organization", DbType.String);
                    if (string.IsNullOrEmpty(txtOrganization.Text))
                        pOrganization.Value = DBNull.Value;
                    else
                        pOrganization.Value = txtOrganization.Text;

                    SQLiteParameter pRemarks = comVisit.Parameters.Add("@Remarks", DbType.String);
                    if (string.IsNullOrEmpty(txtNotes.Text))
                        pRemarks.Value = DBNull.Value;
                    else
                        pRemarks.Value = txtNotes.Text;

                    if (comVisit.ExecuteNonQuery() != 1)
                        throw new Exception("Failed to insert custom visit.");

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // Channel units

                    foreach (ChannelUnit ch in bsChannelUnits)
                    {
                        SQLiteCommand comUnit = new SQLiteCommand("INSERT INTO CHaMP_ChannelUnits (VisitID, SegmentNumber, ChannelUnitNumber, Tier1, Tier2) VALUES (@VisitID, @SegmentNumber, @ChannelUnitNumber, @Tier1, @Tier2)", dbCon, dbTrans);
                        comUnit.Parameters.AddWithValue("@VisitID", valVisitID.Value);
                        comUnit.Parameters.AddWithValue("@SegmentNumber", ch.SegmentNumber);
                        comUnit.Parameters.AddWithValue("@ChannelUnitNumber", ch.UnitNumber);
                        comUnit.Parameters.AddWithValue("@Tier1", ch.Tier1);
                        comUnit.Parameters.AddWithValue("@Tier2", ch.Tier2);
                        if (comUnit.ExecuteNonQuery() != 1)
                            throw new Exception(string.Format("Error inserting new channel unit {0}", ch.UnitNumber));
                    }

                    dbTrans.Commit();
                    MessageBox.Show(string.Format("Custom visit {0} inserted successfully with {1} channel units.", valVisitID.Value, bsChannelUnits.Count), CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                    Classes.ExceptionHandling.NARException.HandleException(ex);
                }
            }
        }

        private class ChannelUnit
        {
            public long UnitNumber { get; set; }
            public long SegmentNumber { get; set; }
            public string Tier1 { get; set; }
            public string Tier2 { get; set; }

            public ChannelUnit()
            {
                UnitNumber = 1;
                SegmentNumber = 1;
            }

            public ChannelUnit(long nUnitNumber, long nSegmentNumber, string sTier1, string sTier2)
            {
                UnitNumber = nUnitNumber;
                SegmentNumber = nSegmentNumber;
                Tier1 = sTier1;
                Tier2 = sTier2;
            }
        }

        private void grdChannelUnits_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!ValidateChannelUnitNumber(e.RowIndex))
            {
                e.Cancel = true;
                return;
            }

            if (!ValidateSegmentUnitNumber(e.RowIndex))
            {
                e.Cancel = true;
                return;
            }

            string[] sTiers = { "1", "2" };
            foreach (string sTier in sTiers)
            {
                object obj = grdChannelUnits.Rows[e.RowIndex].Cells[string.Format("colTier{0}", sTier)].Value;

                if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                {
                    MessageBox.Show(string.Format("You must provide a tier {0} classification for the channel unit.", sTier), CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private bool ValidateChannelUnitNumber(int nRowIndex)
        {
            object obj = grdChannelUnits.Rows[nRowIndex].Cells["colUnitNumber"].Value;

            if (obj == null)
                return false;

            int nNumber = 0;
            if (int.TryParse(obj.ToString(), out nNumber))
            {
                if (nNumber < 1)
                {
                    MessageBox.Show("The channel unit number must be positive.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    // Check unique
                    for (int i = 0; i < grdChannelUnits.Rows.Count; i++)
                    {
                        if (grdChannelUnits.Rows[i].DataBoundItem is ChannelUnit && i != nRowIndex)
                        {
                            if (((ChannelUnit)grdChannelUnits.Rows[i].DataBoundItem).UnitNumber == nNumber)
                            {
                                MessageBox.Show(string.Format("There is already a channel unit number {0}. Each channel unit must have a unique number.", nNumber), CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("You must provide a positive integer channel unit number.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private bool ValidateSegmentUnitNumber(int nRowIndex)
        {
            int nNumber = 0;
            if (int.TryParse(grdChannelUnits.Rows[nRowIndex].Cells["colSegmentNumber"].Value.ToString(), out nNumber))
            {
                if (nNumber < 1)
                {
                    MessageBox.Show("The segment number must be positive.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("You must provide a positive integer segment unit number.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            frmSelectVisitID frm = new frmSelectVisitID(DBCon);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (bsChannelUnits.Count > 0)
                {
                    switch (MessageBox.Show("Do you want to clear the existing list of channel units and reload them from the selected survey geodatabase?", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        case System.Windows.Forms.DialogResult.No:
                            return;

                        case System.Windows.Forms.DialogResult.Cancel:
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            return;
                    }
                }

                // Clear any existing channel units
                bsChannelUnits.Clear();

                using (SQLiteConnection dbCon = new SQLiteConnection(DBCon))
                {
                    dbCon.Open();

                    SQLiteCommand dbCom = new SQLiteCommand("SELECT ChannelUnitNumber, SegmentNumber, Tier1, Tier2 FROM CHAMP_ChannelUnits WHERE VisitID = @VisitID ORDER BY ChannelUnitNumber", dbCon);
                    dbCom.Parameters.AddWithValue("@VisitID", frm.SelectedVisitID);
                    SQLiteDataReader dbRead = dbCom.ExecuteReader();
                    while (dbRead.Read())
                    {
                        bsChannelUnits.Add(new ChannelUnit(
                            dbRead.GetInt64(dbRead.GetOrdinal("ChannelUnitNumber"))
                            , dbRead.GetInt64(dbRead.GetOrdinal("SegmentNumber"))
                            , dbRead.GetString(dbRead.GetOrdinal("Tier1"))
                            , dbRead.GetString(dbRead.GetOrdinal("Tier2"))
                            ));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not yet implemented.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            CHaMPWorkbench.OnlineHelp.FormHelp(this.Name);
        }

        private void LoadPrograms()
        {
            cboProgram.DataSource = new naru.ui.SortableBindingList<CHaMPData.Program>(CHaMPData.Program.Load(naru.db.sqlite.DBCon.ConnectionString).Values.ToList<CHaMPData.Program>());
            cboProgram.DisplayMember = "Name";
            cboProgram.ValueMember = "ID";

            // Default selection to first program that doesn't have API call. i.e. the custom visits program
            for (int i =0; i < cboProgram.Items.Count;i++)
            {
                if (string.IsNullOrEmpty(((CHaMPData.Program) cboProgram.Items[i]).API))
                {
                    cboProgram.SelectedIndex = i;
                    break;
                }
            }
        }
    }
}
