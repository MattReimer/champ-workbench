﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHaMPWorkbench.Data.MetricDefinitions
{
    public partial class frmMetricDefinitions : Form
    {
        private naru.ui.SortableBindingList<MetricDefinition> MetricDefs;
        SaveFileDialog frmExport;

        public frmMetricDefinitions()
        {
            InitializeComponent();

            frmExport = new SaveFileDialog();
            frmExport.Title = "Export Metric Schema";
            frmExport.Filter = "XML Files (*.xml)|*.xml";
            frmExport.AddExtension = true;
            frmExport.InitialDirectory = CHaMPWorkbench.MainForm.GetDatabasePathFromConnectionString(naru.db.sqlite.DBCon.ConnectionString);
        }

        private void frmMetricDefinitions_Load(object sender, EventArgs e)
        {
            MetricDefs = MetricDefinitions.MetricDefinition.Load(naru.db.sqlite.DBCon.ConnectionString);
            grdData.AutoGenerateColumns = false;
            grdData.DataSource = MetricDefs;
            grdData.Dock = DockStyle.Fill;
            grdData.AllowUserToAddRows = false;
            grdData.AllowUserToDeleteRows = false;
            grdData.AllowUserToResizeRows = false;
            grdData.RowHeadersVisible = false;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            naru.db.sqlite.CheckedListItem.LoadCheckListbox(ref chkModel, naru.db.sqlite.DBCon.ConnectionString, string.Format("SELECT ItemID, Title FROM LookupListItems WHERE ListID = {0} ORDER BY Title", 4), false);
            naru.db.sqlite.CheckedListItem.LoadCheckListbox(ref chkSchema, naru.db.sqlite.DBCon.ConnectionString, "SELECT SchemaID, Title FROM Metric_Schemas ORDER BY Title", false);

            chkModel.ItemCheck += FilterListBoxCheckChanged;
            chkSchema.ItemCheck += FilterListBoxCheckChanged;
            txtTitle.TextChanged += FilterList;
            chkActive.CheckedChanged += FilterList;
            chkXPath.CheckedChanged += FilterList;

            // Make the textbox the default control
            txtTitle.Select();
            FilterList(null, null);
        }

        private void FilterListBoxCheckChanged(object sender, ItemCheckEventArgs e)
        {
            ((CheckedListBox)sender).ItemCheck -= FilterListBoxCheckChanged;
            ((CheckedListBox)sender).SetItemChecked(e.Index, e.NewValue == CheckState.Checked);
            ((CheckedListBox)sender).ItemCheck += FilterListBoxCheckChanged;

            try
            {
                FilterList(sender, e);
            }
            catch (Exception ex)
            {
                Classes.ExceptionHandling.NARException.HandleException(ex);
            }
        }

        private void FilterList(object sender, EventArgs e)
        {
            naru.ui.SortableBindingList<MetricDefinition> filteredItems = MetricDefs;

            List<long> modelIDs = new List<long>();
            if (chkModel.CheckedItems.Count > 0 && chkModel.CheckedItems.Count < chkModel.Items.Count)
            {
                foreach (naru.db.NamedObject item in chkModel.CheckedItems)
                    modelIDs.Add(item.ID);

                filteredItems = new naru.ui.SortableBindingList<MetricDefinition>(filteredItems.Where<MetricDefinition>(x => modelIDs.Contains(x.ModelID)).ToList<MetricDefinition>());
            }

            List<long> schemaIDs = new List<long>();
            if (chkSchema.CheckedItems.Count > 0 && chkSchema.CheckedItems.Count < chkSchema.Items.Count)
            {
                foreach (naru.db.NamedObject item in chkSchema.CheckedItems)
                    schemaIDs.Add(item.ID);

                filteredItems = new naru.ui.SortableBindingList<MetricDefinition>(filteredItems.Where<MetricDefinition>(x => schemaIDs.Contains(x.SchemaID)).ToList<MetricDefinition>());
            }

            if (!string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                filteredItems = new naru.ui.SortableBindingList<MetricDefinition>(filteredItems.Where<MetricDefinition>(x => x.Name.ToLower().Contains(txtTitle.Text.ToLower())
                    || x.DisplayNameShort.ToLower().Contains(txtTitle.Text.ToLower()) || x.XPath.ToLower().Contains(txtTitle.Text.ToLower())).ToList<MetricDefinition>());
            }

            if (chkActive.Checked)
            {
                filteredItems = new naru.ui.SortableBindingList<MetricDefinition>(filteredItems.Where<MetricDefinition>(x => x.IsActive).ToList<MetricDefinition>());
            }

            if (chkXPath.Checked)
            {
                filteredItems = new naru.ui.SortableBindingList<MetricDefinition>(filteredItems.Where<MetricDefinition>(x => !string.IsNullOrEmpty(x.XPath)).ToList<MetricDefinition>());
            }

            if (filteredItems.Count != MetricDefs.Count)
                grdData.DataSource = filteredItems;
        }

        private void grdData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                frmMetricProperties frm = new frmMetricProperties((MetricDefinition)grdData.Rows[e.RowIndex].DataBoundItem);
                frm.ShowDialog();
            }
        }

        private void createNewMetricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmMetricProperties frm = new frmMetricProperties(null);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MetricDefs.Add(frm.MetricDef);
                }
            }
            catch (Exception ex)
            {
                Classes.ExceptionHandling.NARException.HandleException(ex);
            }
        }

        private void exportSchemaXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chkSchema.CheckedItems.Count != 1)
            {
                MessageBox.Show("You must check the box beside one and only one metric schema to export.", "Invalid Number of Metric Schemas Checked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (frmExport.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MetricDefinition.ExportMetricSchemaToXML(frmExport.FileName, ((naru.db.NamedObject)chkSchema.CheckedItems[0]).ID);
                }
                catch (Exception ex)
                {
                    Classes.ExceptionHandling.NARException.HandleException(ex);
                }
            }
        }
    }
}