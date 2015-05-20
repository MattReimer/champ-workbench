﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace CHaMPWorkbench.Classes
{
    class ChannelUnitCSVGenerator
    {
        private OleDbConnection m_dbCon;

        public ChannelUnitCSVGenerator(ref OleDbConnection dbCon)
        {
            m_dbCon = dbCon;
        }

        public void Run(int nVisitID, string sFilePath)
        {
            OleDbCommand dbCom = new OleDbCommand("SELECT CHAMP_Sites.SiteName, C.ChannelUnitNumber, C.Tier1, C.Tier2, " +
                " C.BouldersGT256, C.Cobbles65255, C.CoarseGravel1764, C.FineGravel316, C.Sand0062, C.FinesLT006, C.SumSubstrateCover," +
                " W.WatershedName, V.SampleDate, V.CrewName, V.PanelName,C.ID As ChannelUnitID, S.SegmentNumber, S.SegmentName" +
                " FROM CHAMP_Watersheds AS W INNER JOIN ((CHAMP_Sites INNER JOIN CHAMP_Visits AS V ON CHAMP_Sites.SiteID = V.SiteID) INNER JOIN (CHaMP_Segments AS S INNER JOIN CHAMP_ChannelUnits AS C ON S.SegmentID = C.SegmentID) ON V.VisitID = S.VisitID) ON W.WatershedID = CHAMP_Sites.WatershedID" +
                " WHERE V.VisitID=[@VisitID] ORDER BY C.ChannelUnitNumber", m_dbCon);

            dbCom.Parameters.AddWithValue("VisitID", nVisitID);
            try
            {
                OleDbDataReader dbRead = dbCom.ExecuteReader();

                string sUnit;
                List<string> lUnits = new List<string>();
                lUnits.Add("Watershed" +
                    ",SiteID" +
                    ",SampleDate" +
                    ",VisitID" +
                    ",MeasureNbr" +
                    ",Crew" +
                    ",VisitPhase" + 
                    ",VisitStatus" + 
                    ",StreamName" + 
                    ",Panel" +
                    ",ChannelUnitID" + 
                    ",ChannelUnitNumber" + 
                    ",ChannelSegment" +
                    ",SegmentNumber" +
                    ",Tier1" +
                    ",Tier2" +
                    ",FieldNotes" + 
                    ",CountOfPebbles" +
                    ",DataUpdateNotes" +
                    ",PercentFlow" +
                    ",SideChannelPresent" +
                    ",InQualifyingSideChannel" +
                    ",BouldersGT256" +
                    ",Cobbles65255" +
                    ",CoarseGravel1764" +
                    ",FineGravel316" +
                    ",Sand0062" +
                    ",FinesLT006" +
                    ",SumSubstrateCover"
                 );

                while (dbRead.Read())
                {
                    sUnit = AddStringField(ref dbRead, "WatershedName");
                    sUnit += AddStringField(ref dbRead, "SiteName");
                    sUnit += AddStringField(ref dbRead, "SampleDate");
                     sUnit += nVisitID.ToString();
                   sUnit += ",1"; // Measure
                    sUnit += AddStringField(ref dbRead, "CrewName");
                    sUnit += ",1"; // Visit Phase
                    sUnit += ",1"; // Visit Status
                    sUnit += ","; // Stream Name
                    sUnit += AddStringField(ref dbRead, "PanelName");
                    sUnit += AddNumericField(ref dbRead, "ChannelUnitID");
                        sUnit += AddNumericField(ref dbRead, "ChannelUnitNumber");
                        sUnit += AddStringField(ref dbRead, "SegmentName");
                        sUnit += AddNumericField(ref dbRead, "SegmentNumber");
                    sUnit += AddStringField(ref dbRead, "Tier1");
                    sUnit += AddStringField(ref dbRead, "Tier2");
                    sUnit += ",";
                    sUnit += ",0"; // Pebbles
                    sUnit += ",";
                    sUnit += ",0"; // Percent Flow
                    sUnit += ",0"; // Side channel Present
                    sUnit += ",0"; // In qualifying side channel
                 
                    sUnit += AddNumericField(ref dbRead, "BouldersGT256");
                    sUnit += AddNumericField(ref dbRead, "Cobbles65255");
                    sUnit += AddNumericField(ref dbRead, "CoarseGravel1764");
                    sUnit += AddNumericField(ref dbRead, "FineGravel316");
                    sUnit += AddNumericField(ref dbRead, "Sand0062");
                    sUnit += AddNumericField(ref dbRead, "FinesLT006");
                    sUnit += AddNumericField(ref dbRead, "SumSubstrateCover");
                    lUnits.Add(sUnit);
                }
                dbRead.Close();
                System.IO.File.WriteAllLines(sFilePath, lUnits.ToArray<string>());

            }
            catch (Exception ex)
            {
                ex.Data["Visit ID"] = nVisitID.ToString();
                ex.Data["File Path"] = sFilePath;
                throw;
            }
        }

        private string AddNumericField(ref OleDbDataReader dbRead, string sFieldName)
        {
            string sResult = ",0";
            if (DBNull.Value != dbRead[sFieldName])
                sResult = "," + dbRead[sFieldName].ToString();
            return sResult;
        }

        private string AddStringField(ref OleDbDataReader dbRead, string sFieldName)
        {
            string sResult = ",EmptyString";
            if (DBNull.Value != dbRead[sFieldName])
                sResult = "," + dbRead[sFieldName].ToString().Replace(" ", "").Trim();
            return sResult;
        }
    }
}
