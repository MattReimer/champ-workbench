﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SQLite;

namespace CHaMPWorkbench.Classes
{
    public class MetricXPathValidator
    {
        public string DBCon { get; internal set; }

        public MetricXPathValidator(string sDBCon)
        {
            DBCon = sDBCon;
        }

        public List<string> Run(Dictionary<string, long> lMetricSchemas)
        {
            List<string> messages = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();

            using (SQLiteConnection dbCon = new SQLiteConnection(DBCon))
            {
                dbCon.Open();
                SQLiteCommand dbCom = new SQLiteCommand("SELECT Count(*) FROM Metric_Definitions WHERE XPath = @XPath", dbCon);
                SQLiteParameter pXPath = dbCom.Parameters.Add("XPath", System.Data.DbType.String);

                foreach (string sURL in lMetricSchemas.Keys)
                {
                    xmlDoc.Load(sURL);
                    string sMetricXMLFileName = System.IO.Path.GetFileName(sURL);

                    XmlNode nodRoot = xmlDoc.SelectSingleNode("/MetricSchema/RootXPath");
                    if (nodRoot == null)
                    {
                        messages.Add(string.Format("Error: Failed to find RootXPath node in {0}", sMetricXMLFileName));
                        continue;
                    }

                    foreach (XmlNode nodMetric in xmlDoc.SelectNodes("MetricSchema/Metrics/Metric"))
                    {
                        string sMetricNameXML = GetMetricDefinitionAttribute(nodMetric, "name", sMetricXMLFileName, ref messages);
                        if (string.IsNullOrEmpty(sMetricNameXML))
                            continue;

                        string sMetricXPath = GetMetricDefinitionAttribute(nodMetric, "xpath", sMetricXMLFileName, ref messages);
                        if (string.IsNullOrEmpty(sMetricXPath))
                            continue;
                        sMetricXPath = string.Format("{0}/{1}", nodRoot.InnerText, sMetricXPath);

                        string sMetricType = GetMetricDefinitionAttribute(nodMetric, "type", sMetricXMLFileName, ref messages);
                        if (string.IsNullOrEmpty(sMetricType) || string.Compare(sMetricType, "string", true) == 0)
                            continue;

                        pXPath.Value = sMetricXPath;
                        object objCount = dbCom.ExecuteScalar();
                        int nCount = 0;
                        if (objCount != null && objCount != DBNull.Value && int.TryParse(objCount.ToString(), out nCount))
                        {
                            if (nCount == 1)
                                Console.WriteLine(string.Format("Found {1} metric '{0}' with 1 occurance in DB with XPath {2}", sMetricXMLFileName, sMetricNameXML, sMetricXPath));
                            else
                                messages.Add(string.Format("The {0} metric '{1}' has {2} occurances in the database: {3}", sMetricXMLFileName, sMetricNameXML, nCount, sMetricXPath));
                        }
                        else
                            messages.Add(string.Format("The {0} metric '{1}' does not occur in the Workbench metric definitions: {2}", sMetricXMLFileName, sMetricNameXML, sMetricXPath));
                    }

                    Console.WriteLine(string.Format("Metric schema complete for XML file {0}", sMetricXMLFileName));
                }
            }

            Console.WriteLine(messages.Count.ToString());
            return messages;
        }

        private string GetMetricDefinitionAttribute(XmlNode nodMetric, string sAttibuteName, string sMetricXMLFileName, ref List<string> messages)
        {
            if (nodMetric.Attributes[sAttibuteName] == null)
            {
                messages.Add(string.Format("Error: Failed to find {0} attribute on metric node in {1}", sAttibuteName, sMetricXMLFileName));
                return string.Empty;
            }
            else
            {
                if (string.IsNullOrEmpty(nodMetric.Attributes[sAttibuteName].InnerText))
                {
                    messages.Add(string.Format("Error: Empty metric {0} attribute in {1}.", sAttibuteName, sMetricXMLFileName));
                    return string.Empty;
                }
            }

            return nodMetric.Attributes[sAttibuteName].InnerText;
        }
    }
}
