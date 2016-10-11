﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Windows.Forms.DataVisualization.Charting;

namespace CHaMPWorkbench.Data
{
    public partial class ucMetricPlot : UserControl
    {
        public string DBCon { get; set; }
        public int VisitID { get; set; }

        private Dictionary<int, ModelResult> m_dModelResults;

        public ucMetricPlot()
        {
            InitializeComponent();
        }

        private void ucMetricPlot_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DBCon))
                return;

            PlotType.LoadPlotTypes(ref cboPlotTypes, DBCon);
            ModelResult.LoadModelResults(ref cboModelResults, DBCon, VisitID, out m_dModelResults);

            ListItem.LoadComboWithListItems(ref cboXAxis,  DBCon, "SELECT MetricID, DisplayNameShort FROM Metric_Definitions WHERE CMMetricID IS NOT NULL ORDER BY DisplayNameShort");
            ListItem.LoadComboWithListItems(ref cboYAxis, DBCon, "SELECT MetricID, DisplayNameShort FROM Metric_Definitions WHERE CMMetricID IS NOT NULL ORDER BY DisplayNameShort");
            cboXAxis.SelectedValue = "Value";

            cboModelResults.SelectedIndexChanged += PlotChanged;
            cboPlotTypes.SelectedIndexChanged += PlotChanged;
        }

        private void PlotChanged(object sender, EventArgs e)
        {
            PlotType thePlot = null;
            if (cboPlotTypes.SelectedItem is PlotType)
                thePlot = cboPlotTypes.SelectedItem as PlotType;
            else
                return;

            // Select the appropriate X and Y metrics
            if (!(thePlot is CustomPlotType))
            {
                cboXAxis.SelectedValue = thePlot.XMetricID;
                cboYAxis.SelectedValue = thePlot.YMetricID;
            }

            ModelResult theResult = null;
            if (cboModelResults.SelectedItem is ModelResult)
                theResult = cboModelResults.SelectedItem as ModelResult;
            else
                return;

            Dictionary<int, double> dXMetricValues = GetMetricValues(thePlot.XMetricID, theResult.ID);
            Dictionary<int, double> dYMetricValues = GetMetricValues(thePlot.YMetricID, theResult.ID);
        }

        private Dictionary<int, double> GetMetricValues(int nMetricID, int nResultID)
        {
            Dictionary<int, double> dResults = new Dictionary<int, double>();
            using (OleDbConnection dbCon = new OleDbConnection(DBCon))
            {
                dbCon.Open();
                OleDbCommand dbCom = new OleDbCommand("SELECT ResultID, MetricValue FROM Metric_VisitMetrics WHERE (MetricID = @MetricID) AND (MetricValue IS NOT NULL)", dbCon);
                dbCom.Parameters.AddWithValue("@MetricID", nMetricID);
                OleDbDataReader dbRead = dbCom.ExecuteReader();
                while (dbRead.Read())
                    dResults.Add(dbRead.GetInt32(dbRead.GetOrdinal("ResultID")), dbRead.GetDouble(dbRead.GetOrdinal("MetricValue")));
            }

            return dResults;
        }

        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblXAxis.Enabled = cboPlotTypes.SelectedItem is CustomPlotType;
            cboXAxis.Enabled = cboPlotTypes.SelectedItem is CustomPlotType;
            lblYAxis.Enabled = cboPlotTypes.SelectedItem is CustomPlotType;
            cboYAxis.Enabled = cboPlotTypes.SelectedItem is CustomPlotType;

            if (cboModelResults.SelectedItem is ModelResult && cboPlotTypes.SelectedItem is PlotType)
            {
                // TODO replot graph
                UpdatePlot(cboPlotTypes.SelectedItem as PlotType, cboModelResults.SelectedItem as ModelResult);
            }
        }

        private void UpdatePlot(PlotType thePlot, ModelResult theResult)
        {
            chtData.Series.Clear();


            Series watershedSeries = chtData.Series.Add(theResult.Watershed);
            watershedSeries.ChartType = SeriesChartType.Point;
            foreach (ModelResult aResult in m_dModelResults.Values)
            {
                if (aResult.MetricValues.ContainsKey(thePlot.XMetricID) && aResult.MetricValues.ContainsKey(thePlot.YMetricID))
                    watershedSeries.Points.AddXY(aResult.MetricValues[thePlot.XMetricID], aResult.MetricValues[thePlot.YMetricID]);
            }

            if (m_dModelResults.ContainsKey(theResult.ID) && theResult.MetricValues.ContainsKey(thePlot.XMetricID) && theResult.MetricValues.ContainsKey(thePlot.YMetricID))
            {
                // Add the specific result for the target visit
                Series visitSeries = chtData.Series.Add(string.Format("Visit {0}", theResult.VisitID));
                visitSeries.ChartType = SeriesChartType.Point;
                visitSeries.Points.AddXY(theResult.MetricValues[thePlot.XMetricID], theResult.MetricValues[thePlot.YMetricID]);

                visitSeries.Color = Color.Red;
                visitSeries.MarkerSize = 10;
            }

            ChartArea pChartArea = chtData.ChartAreas[0];
            if (chtData.Titles.Count < 1)
                chtData.Titles.Add("ChartTitle");
            chtData.Titles[0].Text = thePlot.Title;

            pChartArea.AxisX.Title = thePlot.XMetric;
            pChartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            pChartArea.AxisX.MajorTickMark.LineColor = Color.Black;
            pChartArea.AxisX.MinorTickMark.Enabled = true;
            pChartArea.AxisX.RoundAxisValues();

            pChartArea.AxisY.Title = thePlot.YMetric;
            pChartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            pChartArea.AxisY.MajorTickMark.LineColor = Color.Black;
            pChartArea.AxisY.MinorTickMark.Enabled = true;

            //enable scroll and zoom
            pChartArea.CursorX.IsUserEnabled = true;
            pChartArea.CursorX.IsUserSelectionEnabled = true;
            pChartArea.CursorY.IsUserEnabled = true;
            pChartArea.CursorY.IsUserSelectionEnabled = true;
        }

        # region HelperClasses

        private class PlotType
        {
            public int PlotID { get; internal set; }
            public string Title { get; internal set; }
            public int XMetricID { get; internal set; }
            public string XMetric { get; internal set; }
            public int YMetricID { get; internal set; }
            public string YMetric { get; internal set; }
            public int PlotTypeID { get; internal set; }

            public PlotType(int nPlotID, string sTitle, int nXMetricID, string sXMetric, int nYMetricID, string sYMetric, int nPlotTypeID)
            {
                PlotID = nPlotID;
                Title = sTitle;
                XMetricID = nXMetricID;
                XMetric = sXMetric;
                YMetricID = nYMetricID;
                YMetric = sYMetric;
                PlotTypeID = nPlotTypeID;

            }

            public override string ToString()
            {
                return Title;
            }

            public static void LoadPlotTypes(ref ComboBox cbo, string sDBCon)
            {
                cbo.Items.Clear();

                // Add the new custom plot type that enables the custom axes
                cbo.Items.Add(new CustomPlotType("-- Custom Axes --"));

                using (OleDbConnection dbCon = new OleDbConnection(sDBCon))
                {
                    dbCon.Open();
                    OleDbCommand dbCom = new OleDbCommand("SELECT P.PlotID, P.PlotTitle, P.XMetricID, P.YMetricID, P.PlotTypeID, Y.Title AS YTitle, X.Title AS XTitle" +
                        " FROM (Metric_Definitions AS X INNER JOIN Metric_Plots AS P ON X.MetricID = P.XMetricID) INNER JOIN Metric_Definitions AS Y ON P.YMetricID = Y.MetricID ORDER BY P.PlotTitle", dbCon);

                    OleDbDataReader dbRead = dbCom.ExecuteReader();
                    while (dbRead.Read())
                    {
                        cbo.Items.Add(new PlotType(
                            dbRead.GetInt32(dbRead.GetOrdinal("PlotID"))
                            , dbRead.GetString(dbRead.GetOrdinal("PlotTitle"))
                            , dbRead.GetInt32(dbRead.GetOrdinal("XMetricID"))
                            , dbRead.GetString(dbRead.GetOrdinal("XTitle"))
                            , dbRead.GetInt32(dbRead.GetOrdinal("YMetricID"))
                            , dbRead.GetString(dbRead.GetOrdinal("YTitle"))
                            , dbRead.GetInt32(dbRead.GetOrdinal("PlotTypeID"))));
                    }

                    if (cbo.Items.Count > 1)
                        cbo.SelectedIndex = 1;
                }
            }
        }

        class CustomPlotType : PlotType
        {
            public new int XMetricID { get; set; }
            public new int YMetricID { get; set; }
            public new string XMetric { get; internal set; }
            public new string YMetric { get; internal set; }

            public CustomPlotType(string sTitle)
                : base(0, sTitle, 0, "", 0, "", 0)
            {

            }
        }

        private class ModelResult
        {
            public int ID { get; internal set; }
            public string ModelVersion { get; internal set; }
            public string ScavengeType { get; internal set; }
            public DateTime RunDateTime { get; internal set; }
            public bool HasDateTime { get; set; }

            public int WatershedID { get; internal set; }
            public string Watershed { get; internal set; }
            public int SiteID { get; internal set; }
            public string Site { get; internal set; }
            public int VisitID { get; internal set; }

            public Dictionary<int, double> MetricValues { get; internal set; }

            public void SetMetricValue(int nMetricID, double fValue)
            {
                MetricValues[nMetricID] = fValue;
            }

            public string Title
            {
                get
                {
                    if (HasDateTime)
                        return string.Format("Version {0} on {1} status {2} ({3})", ModelVersion, RunDateTime, ScavengeType, ID);
                    else
                        return string.Format("Version {0} status {1} ({2})", ModelVersion, ScavengeType, ID);
                }
            }

            public override string ToString()
            {
                return Title;
            }

            public ModelResult(int nID, string sModelVersion, DateTime dtRunDateTime, string sScavengeType, int nWatershedID, string sWatershedName, int nSiteID, string sSiteName, int nVisitID)
            {
                RunDateTime = dtRunDateTime;
                HasDateTime = true;

                Init(nID, sModelVersion, sScavengeType, nWatershedID, sWatershedName, nSiteID, sSiteName, nVisitID);
            }

            public ModelResult(int nID, string sModelVersion, string sScavengeType, int nWatershedID, string sWatershedName, int nSiteID, string sSiteName, int nVisitID)
            {
                Init(nID, sModelVersion, sScavengeType, nWatershedID, sWatershedName, nSiteID, sSiteName, nVisitID);
            }

            private void Init(int nID, string sModelVersion, string sScavengeType, int nWatershedID, string sWatershedName, int nSiteID, string sSiteName, int nVisitID)
            {
                ID = nID;
                ModelVersion = sModelVersion;
                HasDateTime = false;
                ScavengeType = sScavengeType;

                WatershedID = nWatershedID;
                Watershed = sWatershedName;
                SiteID = nSiteID;
                Site = sSiteName;
                VisitID = nVisitID;

                MetricValues = new Dictionary<int, double>();
            }

            public static void LoadModelResults(ref ComboBox cbo, string sDBCon, int VisitID, out Dictionary<int, ModelResult> dModelResults)
            {
                cbo.Items.Clear();
                dModelResults = new Dictionary<int, ModelResult>();

                using (OleDbConnection conResults = new OleDbConnection(sDBCon))
                {
                    conResults.Open();
                    OleDbCommand comResults = new OleDbCommand("SELECT R.ResultID, R.ModelVersion, R.RunDateTime, L.Title AS ScavengeType, R.VisitID, S.SiteName, S.SiteID, W.WatershedID, W.WatershedName" +
" FROM LookupListItems AS L INNER JOIN (CHAMP_Watersheds AS W INNER JOIN (CHAMP_Sites AS S INNER JOIN (Metric_Results AS R INNER JOIN CHAMP_Visits AS V ON R.VisitID = V.VisitID) ON S.SiteID = V.SiteID) ON W.WatershedID = S.WatershedID) ON L.ItemID = R.ScavengeTypeID" +
" WHERE (((W.WatershedID) In (SELECT SS.WatershedID FROM CHAMP_Sites AS SS INNER JOIN CHAMP_Visits AS VV ON SS.SiteID = VV.SiteID WHERE (((VV.VisitID)=@VisitID)))))" +
" ORDER BY R.RunDateTime DESC", conResults);
                    comResults.Parameters.AddWithValue("@VisitID", VisitID);

                    using (OleDbConnection conMetricValues = new OleDbConnection(sDBCon))
                    {
                        conMetricValues.Open();
                        OleDbCommand comMetricValues = new OleDbCommand("SELECT MetricID, MetricValue FROM Metric_VisitMetrics WHERE (ResultID = @ResultID) AND (MetricValue IS NOT NULL)", conMetricValues);
                        OleDbParameter pResultID = comMetricValues.Parameters.Add("@ResultID", OleDbType.Integer);


                        OleDbDataReader rdResults = comResults.ExecuteReader();
                        while (rdResults.Read())
                        {
                            int nResultID = rdResults.GetInt32(rdResults.GetOrdinal("ResultID"));

                            ModelResult theResult = null;
                            if (rdResults.IsDBNull(rdResults.GetOrdinal("RunDateTime")))
                                theResult = new ModelResult(nResultID
                                    , rdResults.GetString(rdResults.GetOrdinal("ModelVersion"))
                                    , rdResults.GetString(rdResults.GetOrdinal("ScavengeType"))
                                    , rdResults.GetInt32(rdResults.GetOrdinal("WatershedID"))
                                    , rdResults.GetString(rdResults.GetOrdinal("WatershedName"))
                                    , rdResults.GetInt32(rdResults.GetOrdinal("SiteID"))
                                    , rdResults.GetString(rdResults.GetOrdinal("SiteName"))
                                    , rdResults.GetInt32(rdResults.GetOrdinal("VisitID"))


                                    );
                            else
                                theResult = new ModelResult(nResultID
                                    , rdResults.GetString(rdResults.GetOrdinal("ModelVersion"))
                                    , rdResults.GetDateTime(rdResults.GetOrdinal("RunDateTime"))
                                    , rdResults.GetString(rdResults.GetOrdinal("ScavengeType"))
                                    , rdResults.GetInt32(rdResults.GetOrdinal("WatershedID"))
                                    , rdResults.GetString(rdResults.GetOrdinal("WatershedName"))
                                    , rdResults.GetInt32(rdResults.GetOrdinal("SiteID"))
                                    , rdResults.GetString(rdResults.GetOrdinal("SiteName"))
                                    , rdResults.GetInt32(rdResults.GetOrdinal("VisitID"))
                                    );

                            // Now add the metric values to this result
                            pResultID.Value = nResultID;
                            OleDbDataReader rdMetricValues = comMetricValues.ExecuteReader();
                            while (rdMetricValues.Read())
                                theResult.SetMetricValue(rdMetricValues.GetInt32(rdMetricValues.GetOrdinal("MetricID")), rdMetricValues.GetDouble(rdMetricValues.GetOrdinal("MetricValue")));
                            rdMetricValues.Close();

                            dModelResults.Add(theResult.ID, theResult);

                            if (theResult.VisitID == VisitID)
                                cbo.Items.Add(theResult);
                        }
                    }

                    if (cbo.Items.Count > 0)
                        cbo.SelectedIndex = 0;
                }
            }
        }

        #endregion
    }
}
