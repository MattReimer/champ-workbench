﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHaMPWorkbench.Experimental.Philip
{
    public partial class frmMetricScraper : Form
    {
        public frmMetricScraper()
        {
            InitializeComponent();
        }

        private void frmMetricScraper_Load(object sender, EventArgs e)
        {
            txtFileName.Text = "metrics.xml";
            if (!string.IsNullOrEmpty(CHaMPWorkbench.Properties.Settings.Default.LastMetricFolder)
                && System.IO.Directory.Exists(CHaMPWorkbench.Properties.Settings.Default.LastMetricFolder))
            {
                txtFolder.Text = CHaMPWorkbench.Properties.Settings.Default.LastMetricFolder;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            try
            {
                System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;

                CHaMPWorkbench.Properties.Settings.Default.LastMetricFolder = txtFolder.Text;
                CHaMPWorkbench.Properties.Settings.Default.Save();

                Classes.MetricXPathValidator x = new Classes.MetricXPathValidator(naru.db.sqlite.DBCon.ConnectionString);
                Dictionary<string, long> lMetricSchemas = new Dictionary<string, long>();
                lMetricSchemas[@"https://raw.githubusercontent.com/Riverscapes/CHaMPAutomation/master/templates/XML/TopoVisitMetrics.xml"] = 3;
                lMetricSchemas[@"https://raw.githubusercontent.com/Riverscapes/CHaMPAutomation/master/templates/XML/TopoChannelUnitMetrics.xml"] = 6;
                lMetricSchemas[@"https://raw.githubusercontent.com/Riverscapes/CHaMPAutomation/master/templates/XML/TopoTier1Metrics.xml"] = 4;
                lMetricSchemas[@"https://raw.githubusercontent.com/Riverscapes/CHaMPAutomation/master/templates/XML/TopoTier2Metrics.xml"] = 5;

                List<string> lErros = x.Run(lMetricSchemas);
                if (lErros.Count>0)
                {
                    frmToolResults frm = new frmToolResults("Metric XPath Validation Failed",
                        "The following metrics are defined in the program XML files but failed validation in the workbench database. Metrics can only be scraped once all these issues are resolved" +
                        " and each metric defined in the XML metric schema definition XML file(s) occurs exactly once in the Workbench database.", ref lErros);
                    frm.ShowDialog();
                    return;
                }


                Experimental.Philip.TopoMetricScavenger scraper = new Experimental.Philip.TopoMetricScavenger();
                int nFilesProcessed = scraper.Run(txtFolder.Text, txtFileName.Text);
                System.Windows.Forms.Cursor.Current = Cursors.Default;
                MessageBox.Show(string.Format("{0} result XML files processed.", nFilesProcessed), "Process Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                Classes.ExceptionHandling.NARException.HandleException(ex);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtFolder.Text)
               || !System.IO.Directory.Exists(txtFolder.Text))
            {
                MessageBox.Show("You must select an existing top level folder where your metric result XML files are stored.", "Invalid Top Level Folder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmdBrowse.Select();
                return false;
            }

            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageBox.Show("You must specify the name of the metric XML files to scrape. Wildcards (* and ?) are allowed.", "Invalid Metric XML File Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Select();
                return false;
            }

            return true;
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            naru.os.Folder.BrowseFolder(ref txtFolder, "Top Level Metric Folder", txtFolder.Text);
        }
    }
}
