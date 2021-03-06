﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace CHaMPWorkbench.GUT
{
    public partial class frmGUTInputXMLBuilder : Form
    {
        private string m_sDBCon;
        private Dictionary<long, string> m_dVisits;

        public frmGUTInputXMLBuilder(string sDBCon, Dictionary<long, string> dVisits)
        {
            InitializeComponent();
            m_sDBCon = sDBCon;
            m_dVisits = dVisits;
        }

        private void frmGUTInputXMLBuilder_Load(object sender, EventArgs e)
        {
            txtBatch.Text = "Batch " + DateTime.Now.ToString("yyy_MM_dd");
#if DEBUG
            txtBatch.Text = txtBatch.Text + "_debug";
            chkClearOtherBatches.Checked = true;
#endif

            if (!string.IsNullOrEmpty(CHaMPWorkbench.Properties.Settings.Default.MonitoringDataFolder) && System.IO.Directory.Exists(CHaMPWorkbench.Properties.Settings.Default.MonitoringDataFolder))
                txtMonitoringDataFolder.Text = CHaMPWorkbench.Properties.Settings.Default.MonitoringDataFolder;

            if (!string.IsNullOrEmpty(CHaMPWorkbench.Properties.Settings.Default.InputOutputFolder) && System.IO.Directory.Exists(CHaMPWorkbench.Properties.Settings.Default.InputOutputFolder))
                txtOutputFolder.Text = CHaMPWorkbench.Properties.Settings.Default.InputOutputFolder;

            try
            {
                Classes.InputFileBuilder_Helper.RefreshVisitPaths(m_sDBCon, ref m_dVisits, ref lstVisits);
            }
            catch (Exception ex)
            {
                Classes.ExceptionHandling.NARException.HandleException(ex);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }

            Classes.ModelInputFiles.GUTInputProperties gutProperties = new Classes.ModelInputFiles.GUTInputProperties(val_low_slope.Value, val_high_slope.Value,
                val_low_cm_slope.Value, val_high_cm_slope.Value, val_fw_reliedf.Value, val_low_hadbf.Value, val_up_hadbf.Value, val_low_bf_distance.Value, val_high_bf_distance.Value,
                valLowRelief.Value, valHighRelief.Value);

            Classes.ModelInputFiles.GUT_BatchInputFileBuilder gutBuilder = new Classes.ModelInputFiles.GUT_BatchInputFileBuilder(m_sDBCon, txtBatch.Text, chkClearOtherBatches.Checked,
                txtMonitoringDataFolder.Text, txtOutputFolder.Text, ref m_dVisits, txtInputFile.Text, gutProperties);

            try
            {
                int nSuccess = 0;
                List<string> lExceptionMessages;
                
                nSuccess = gutBuilder.Run(out lExceptionMessages);
            }
            catch (Exception ex)
            {
                Classes.ExceptionHandling.NARException.HandleException(ex);
            }
        }

        private bool ValidateForm()
        {
            if (m_dVisits.Count < 1)
            {
                MessageBox.Show("You must select at least one visit to proceed. Return to the main Workbench grid and select the visits for which you want to run the model.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmdCancel.Select();
                return false;
            }

            if (string.IsNullOrEmpty(txtBatch.Text))
            {
                MessageBox.Show("You must provide a name for the batch.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBatch.Select();
                return false;
            }
            else
            {
                // check that the batch name is unique
                using (SQLiteConnection dbCon = new SQLiteConnection(m_sDBCon))
                {
                    dbCon.Open();

                    SQLiteCommand dbCom = new SQLiteCommand("SELECT ID FROM Model_Batches WHERE BatchName = @BatchName", dbCon);
                    dbCom.Parameters.AddWithValue("@BatchName", txtBatch.Text);
                    object obj = dbCom.ExecuteScalar();
                    if (obj != null & obj != DBNull.Value)
                    {
                        MessageBox.Show(string.Format("A batch with the name '{0}' already exists in the Workbench database. Please choose a unique name.", txtBatch.Text), CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBatch.Select();
                        return false;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtMonitoringDataFolder.Text) || !System.IO.Directory.Exists(txtMonitoringDataFolder.Text))
            {
                MessageBox.Show("You must provide a valid folder that contains the CHaMP monitoring data.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmdBrowseMonitoring.Select();
                return false;
            }

            if (string.IsNullOrEmpty(txtOutputFolder.Text) || !System.IO.Directory.Exists(txtOutputFolder.Text))
            {
                MessageBox.Show("You must provide a valid output folder where the input files will get created.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmdBrowseInputOutput.Select();
                return false;
            }

            if (string.IsNullOrEmpty(txtInputFile.Text))
            {
                MessageBox.Show("You must provide a name for the GUT input XML files that will get created.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtInputFile.Select();
                return false;
            }
            else
            {
                if (!txtInputFile.Text.ToLower().EndsWith(".xml"))
                {
                    MessageBox.Show("The GUT input XML files should end with '.xml'.", CHaMPWorkbench.Properties.Resources.MyApplicationNameLong, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInputFile.Select();
                    return false;
                }
            }

            return true;
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {

        }

        private void cmdBrowseMonitoring_Click(object sender, EventArgs e)
        {
            Classes.InputFileBuilder_Helper.BrowseFolder("Monitoring Data Folder", "Select the top level folder that contains the monitoring data.", ref txtMonitoringDataFolder);
        }

        private void cmdBrowseInputOutput_Click(object sender, EventArgs e)
        {
            Classes.InputFileBuilder_Helper.BrowseFolder("Input/Output Data Folder", "Select the top level folder that will contain the input files.", ref txtOutputFolder);
        }
    }
}
