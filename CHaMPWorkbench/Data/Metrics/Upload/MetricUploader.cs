﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHaMPWorkbench.CHaMPData;
using System.Data.SQLite;

namespace CHaMPWorkbench.Data.Metrics.Upload
{
    public class MetricUploader
    {
        private string UserName { get; set; }
        private string Password { get; set; }
        Dictionary<long, MetricSchema> MetricSchemas;
        Dictionary<long, MetricDefinitions.MetricDefinition> MetricDefs;

        IdentityModel.Client.TokenResponse authToken;

        public event EventHandler MessagesUpdated;

        private void SetMessage(string sMessage)
        {
            System.Diagnostics.Debug.Print(sMessage);

            if (MessagesUpdated != null)
                MessagesUpdated(null, null);
        }

        public MetricUploader(string sUsername, string sPassword)
        {
            UserName = sUsername;
            Password = sPassword;

            MetricSchemas = MetricSchema.Load(naru.db.sqlite.DBCon.ConnectionString);
            MetricDefs = MetricDefinitions.MetricDefinition.Load(naru.db.sqlite.DBCon.ConnectionString);
        }

        public void Run(Dictionary<long, MetricBatch> selectedBatches)
        {
            if (!VerifyMetricSchemasMatch(selectedBatches))
            {
                SetMessage("Aborting due to mismatching metric schemas. No metric uploaded.");
                return;
            }

            foreach (CHaMPData.MetricBatch batch in selectedBatches.Values)
            {
                List<MetricInstance> instances = MetricInstance.Load(batch);
                SetMessage(string.Format("Processing the {0} schema with {1} visits", batch.Schema, instances.Count));

                foreach (MetricInstance inst in instances)
                {
                    SetMessage(string.Format("\tVisit {0} with {1} metric values", inst.VisitID, inst.Metrics.Count));
                }
            }
        }

        private bool VerifyMetricSchemasMatch(Dictionary<long, MetricBatch> selectedBatches)
        {
            // Build a list of distinct metric schemas
            Dictionary<long, string> uniqueSchemas = new Dictionary<long, string>();
            foreach (MetricBatch batch in selectedBatches.Values)
            {
                if (!uniqueSchemas.ContainsKey(batch.Schema.ID))
                    uniqueSchemas[batch.Schema.ID] = batch.Schema.Name;
            }

            bool bStatus = true;
            foreach (long schemaID in uniqueSchemas.Keys)
            {
                SchemaDefinition dbDef = new SchemaDefinition(schemaID, uniqueSchemas[schemaID]);
                SchemaDefinition xmlDef = new SchemaDefinition(MetricSchemas[schemaID].MetricSchemaXMLFile);
                List<string> Messages = null;
                if (!dbDef.Equals(ref xmlDef, out Messages))
                    bStatus = false;

            }

            //    GeoOptix.API.ApiHelper api = new GeoOptix.API.ApiHelper()

            //    GeoOptix.API.Model.P.MetricSchemaModel aschema = new GeoOptix.API.Model.MetricSchemaModel("test",

            //        ((int)visit.ID, visit.ID.ToString(), visitURL, string.Empty, string.Empty, null, null, null, null, null, null);

            //

            return bStatus;
        }

        private class BatchMetrics
        {
            MetricBatch Batch;

            List<MetricInstance> Instances;

            public BatchMetrics(MetricBatch batch)
            {
                Batch = batch;
                Instances = MetricInstance.Load(batch);
            }
        }

        private class MetricInstance
        {
            public long InstanceID { get; internal set; }
            public long VisitID { get; internal set; }
            public string ModelVersion { get; internal set; }
            public List<MetricValueBase> Metrics { get; internal set; }

            public MetricInstance(long nInstanceID, long nVisitID, string sModelVersion)
            {
                InstanceID = nInstanceID;
                VisitID = nVisitID;
                ModelVersion = sModelVersion;
                Metrics = new List<MetricValueBase>();
            }

            public static List<MetricInstance> Load(CHaMPData.MetricBatch batch)
            {
                List<MetricInstance> instances = new List<MetricInstance>();

                using (SQLiteConnection dbCon = new SQLiteConnection(naru.db.sqlite.DBCon.ConnectionString))
                {
                    dbCon.Open();
                    SQLiteCommand dbCom = new SQLiteCommand("SELECT * FROM Metric_Instances WHERE BatchID = @BatchID", dbCon);
                    dbCom.Parameters.AddWithValue("BatchID", batch.ID);
                    SQLiteDataReader dbRead = dbCom.ExecuteReader();
                    while (dbRead.Read())
                    {
                        instances.Add(new MetricInstance(dbRead.GetInt64(dbRead.GetOrdinal("InstanceID")), dbRead.GetInt64(dbRead.GetOrdinal("VisitID")), naru.db.sqlite.SQLiteHelpers.GetSafeValueStr(ref dbRead, "ModelVersion")));
                    }
                    dbRead.Close();

                    string sqlMetrics = string.Empty;
                    switch (batch.DatabaseTable.ToLower())
                    {
                        case "metric_visitmetrics":
                            sqlMetrics = "SELECT * FROM Metric_VisitMetrics WHERE InstanceID = @InstanceID";
                            break;

                        case "metric_channelunitmetrics":
                            sqlMetrics = "SELECT M.MetricID, MetricValue, C.ChannelUnitNumber AS ChannelUnitNumber, T1.Title AS Tier1, T2.Title AS Tier2" +
                                " FROM Metric_ChannelUnitMetrics M" +
                                " INNER JOIN Metric_Instances I ON M.InstanceID = I.InstanceID" +
                                " INNER JOIN CHaMP_ChannelUnits C ON M.ChannelUnitNumber = C.ChannelUnitNumber AND I.VisitID = C.VisitID" +
                                " INNER JOIN LookupListItems T1 ON C.Tier1 = T1.ItemID" +
                                " INNER JOIN LookupListItems T2 ON C.Tier2 = T2.ItemID" +
                                " WHERE I.InstanceID = @InstanceID";
                            break;

                        case "metric_tiermetrics":
                            sqlMetrics = "SELECT M.MetricID, MetricValue, T.Title AS Tier" +
                                " FROM Metric_TierMetrics M" +
                                " INNER JOIN Metric_Instances I ON M.InstanceID = I.InstanceID" +
                                " INNER JOIN LookupListItems T ON M.TierID = T.ItemID" +
                                " WHERE I.InstanceID = @InstanceID";
                            break;

                        default:
                            throw new Exception(string.Format("Unhandled metric table: {0}", batch.DatabaseTable));
                    }

                    dbCom = new SQLiteCommand(sqlMetrics, dbCon);
                    SQLiteParameter pInstanceID = dbCom.Parameters.Add("@InstanceID", System.Data.DbType.Int64);

                    // Now load all the metric values for this instance.
                    foreach (MetricInstance instance in instances)
                    {
                        pInstanceID.Value = instance.InstanceID;
                        dbRead = dbCom.ExecuteReader();
                        while (dbRead.Read())
                        {
                            switch (batch.DatabaseTable.ToLower())
                            {
                                case "metric_visitmetrics":
                                    instance.Metrics.Add(new MetricValueBase(dbRead.GetInt64(dbRead.GetOrdinal("MetricID")), dbRead.GetDouble(dbRead.GetOrdinal("MetricValue"))));
                                    break;

                                case "metric_channelunitmetrics":
                                    instance.Metrics.Add(new ChannelUnitMetricValue(dbRead.GetInt64(dbRead.GetOrdinal("MetricID"))
                                        , dbRead.GetDouble(dbRead.GetOrdinal("MetricValue"))
                                        , dbRead.GetInt64(dbRead.GetOrdinal("ChannelUnityNumber"))
                                        , dbRead.GetString(dbRead.GetOrdinal("Tier1"))
                                        , dbRead.GetString(dbRead.GetOrdinal("Tier2"))));
                                    break;

                                case "metric_tiermetrics":
                                    instance.Metrics.Add(new TierMetricValue(dbRead.GetInt64(dbRead.GetOrdinal("MetricID"))
                                        , dbRead.GetDouble(dbRead.GetOrdinal("MetricValue"))
                                        , dbRead.GetString(dbRead.GetOrdinal("Tier"))));
                                    break;

                                default:
                                    throw new Exception(string.Format("Unhandled metric table: {0}", batch.DatabaseTable));
                            }
                        }
                        dbRead.Close();
                    }
                }

                return instances;
            }
        }

        private class MetricValueBase
        {
            public long MetricID { get; internal set; }
            public double MetricValue { get; internal set; }

            public MetricValueBase(long nMetricID, double fMetricValue)
            {
                MetricID = nMetricID;
                MetricValue = fMetricValue;
            }
        }

        private class TierMetricValue : MetricValueBase
        {
            public string Tier { get; internal set; }

            public TierMetricValue(long nMetricID, double fMetricValue, string sTier)
                : base(nMetricID, fMetricValue)
            {
                Tier = sTier;
            }
        }

        private class ChannelUnitMetricValue : MetricValueBase
        {
            public long ChannelUnitNumber { get; internal set; }
            public string Tier1 { get; internal set; }
            public string Tier2 { get; internal set; }

            public ChannelUnitMetricValue(long nMetricID, double fMetricValue, long nChannelUnitNumber, string sTier1, string sTier2)
                : base(nMetricID, fMetricValue)
            {
                ChannelUnitNumber = nChannelUnitNumber;
                Tier1 = sTier1;
                Tier2 = sTier2;
            }
        }
    }
}
