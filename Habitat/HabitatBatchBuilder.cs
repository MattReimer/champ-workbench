﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using HMDesktop.Classes;

namespace CHaMPWorkbench.Habitat
{
    class HabitatBatchBuilder
    {
        private OleDbConnection m_dbWorkbenchCon;
        private OleDbConnection m_dbHabitatCon;

        private Classes.CHaMPData m_CHaMPData;
        HSProjectManager m_HabitatManager;
        private System.IO.DirectoryInfo m_dMonitoringDatafolder;

        // This is the database ID of the lookup list item for CSV data source types
        private const int m_nCSVDataSourceTypeID = 61;

        public HabitatBatchBuilder(ref OleDbConnection dbWorkbench, string sHabitatDBPath, string sMonitoringDataFolder)
        {
            m_CHaMPData = new Classes.CHaMPData(ref dbWorkbench);
            m_dMonitoringDatafolder = new System.IO.DirectoryInfo(sMonitoringDataFolder);
            m_HabitatManager = new HSProjectManager(sHabitatDBPath);
        }

        public void BuildBatch(List<int> lVisitIDs, HabitatModelDef theModel, ref int nSucess, ref int nError) //, int nVelocityHSCID, int nDepthHSCID, int nSubstrateHSCID
        {
            nSucess = nError = 0;

            m_CHaMPData.FillByVisitIDS(ref lVisitIDs);

            foreach (RBTWorkbenchDataSet.CHAMP_VisitsRow rVisit in m_CHaMPData.DS.CHAMP_Visits)
            {
                // Placeholder until visits are filtered at load
                if (!lVisitIDs.Contains(rVisit.VisitID))
                    continue;

                // Create the one simulation for this visit
                dsHabitat.SimulationsRow rSimulation = m_HabitatManager.ProjectDatabase.Simulations.NewSimulationsRow();
                string sModelTitle;

                if (theModel.ModelType == HabitatModelDef.ModelTypes.HSI)
                {
                    sModelTitle = m_HabitatManager.ProjectDatabase.HSI.FindByHSIID(theModel.Value).Title;
                    rSimulation.HSIID = theModel.Value;
                }
                else
                {
                    sModelTitle = m_HabitatManager.ProjectDatabase.FIS.FindByFISID(theModel.Value).Title;
                    rSimulation.FISID = theModel.Value;
                }

                rSimulation.Title = GetSimulationName(rVisit, sModelTitle);
                rSimulation.CreatedBy = Environment.UserName;
                rSimulation.CreatedOn = DateTime.Now;
                rSimulation.AddIndividualOutput = true;
                rSimulation.Folder = Paths.GetRelativePath(Paths.GetSpecificSimulationFolder(rSimulation.Title));
                rSimulation.OutputRaster = Paths.GetRelativePath(Paths.GetSpecificOutputFullPath(rSimulation.Title, "tif"));
                rSimulation.OutputCSV = Paths.GetRelativePath(Paths.GetSpecificOutputFullPath(rSimulation.Title, "csv"));
                rSimulation.IsQueuedToRun = true;
                rSimulation.VisitID = rVisit.VisitID;
                rSimulation.CellSize = (float)0.1;

                // Add the simulation. Now that it's XML, the ID should not need to be retrieved.
                m_HabitatManager.ProjectDatabase.Simulations.AddSimulationsRow(rSimulation);

                // Trigger retrieval of SimulationID;
                //m_taSimulations.Update(rSimulation);
                int nSimulationID = rSimulation.SimulationID;
                nSucess++;

                // Temporary fix because the C++ cannot produce a raster when there are no raster inputs.
                // And cannot produce a CSV when there are just rasters.
                Boolean bRasterInputs = false;

                if (theModel.ModelType == HabitatModelDef.ModelTypes.HSI)
                    AddHSISimulationChildRecords(ref rSimulation, rVisit, theModel.Value, ref bRasterInputs);
                else
                    AddFISSimulationChildRecords(ref rSimulation, rVisit, theModel.Value, ref bRasterInputs);

                // Final stage of temporary fix mentioned above. Clear the output paths  for the type of output that is not
                // currently possible in the C++
                rSimulation = m_HabitatManager.ProjectDatabase.Simulations.FindBySimulationID(nSimulationID);
                if (bRasterInputs)
                    rSimulation.SetOutputCSVNull();
                else
                    rSimulation.SetOutputRasterNull();

                HSProjectManager.Instance.Save();
            }
        }

        private void AddHSISimulationChildRecords(ref dsHabitat.SimulationsRow rSimulation, RBTWorkbenchDataSet.CHAMP_VisitsRow rVisit, int nHSIID, ref bool bRasterInputs)
        {
            dsHabitat.HSIRow rHSI = m_HabitatManager.ProjectDatabase.HSI.FindByHSIID(nHSIID);

            // Loop over all the input curves and create the necessary project data sources and inputs
            dsHabitat.ProjectDataSourcesRow rCSVDataSource = null;
            foreach (dsHabitat.HSICurvesRow rHSICurveRow in rHSI.GetHSICurvesRows())
            {
                dsHabitat.VariablesRow rVariable = m_HabitatManager.ProjectDatabase.Variables.FindByVariableID(rHSICurveRow.HSCRow.HSCVariableID);
                dsHabitat.ProjectVariablesRow rProjectVariable = null;

                if (rVariable.VariableName.ToLower().Contains("substrate") || rVariable.VariableName.ToLower().Contains("d50"))
                {
                    // Create raster data source
                    string sOriginalPath = System.IO.Path.Combine(m_dMonitoringDatafolder.FullName, rVisit.Folder, rVisit.ICRPath);
                    dsHabitat.ProjectDataSourcesRow rSubstrateSource = BuildAndCopyProjectDataSource(rVisit.VisitID, "SubstrateRaster", sOriginalPath, false, "raster");

                    // Create project variable
                    rProjectVariable = BuildProjectVariable("D50", rHSICurveRow.HSCRow.HSCName, rHSICurveRow.HSCRow.UnitID, rHSICurveRow.HSCRow.VariablesRow.VariableID, rSubstrateSource.DataSourceID);
                    bRasterInputs = true;
                }
                else
                {
                    // Create a Data Source for the CSV
                    if (rCSVDataSource == null)
                    {
                        string sOriginalPath = System.IO.Path.Combine(m_dMonitoringDatafolder.FullName, rVisit.Folder);
                        sOriginalPath = System.IO.Path.Combine((new System.IO.DirectoryInfo(sOriginalPath)).Parent.FullName, "Hydro", rVisit.HydraulicModelCSV);
                        rCSVDataSource = BuildAndCopyProjectDataSource(rVisit.VisitID, "Delft 3D CSV Output", sOriginalPath, true, "csv");
                    }

                    if (rVariable.VariableName.ToLower().Contains("velocity"))
                        rProjectVariable = BuildProjectVariable("Velocity.Magnitude", rHSICurveRow.HSCRow.HSCName, rHSICurveRow.HSCRow.UnitID, rHSICurveRow.HSCRow.VariablesRow.VariableID, rCSVDataSource.DataSourceID);
                    else
                        rProjectVariable = BuildProjectVariable("Depth", rHSICurveRow.HSCRow.HSCName, rHSICurveRow.HSCRow.UnitID, rHSICurveRow.HSCRow.VariablesRow.VariableID, rCSVDataSource.DataSourceID);
                }

                // Insert the Simulation HSC input
                dsHabitat.SimulationHSCInputsRow rSimHSCInput = m_HabitatManager.ProjectDatabase.SimulationHSCInputs.NewSimulationHSCInputsRow();
                rSimHSCInput.SimulationsRow = rSimulation;
                rSimHSCInput.HSICurvesRow = rHSICurveRow;
                rSimHSCInput.HSOutputPath = Paths.GetRelativePath(Paths.GetSpecificOutputHSFullPath(rSimulation.Title, rProjectVariable.Title));
                rSimHSCInput.HSPreparedPath = Paths.GetRelativePath(Paths.GetSpecificPreparedHSFullPath(rSimulation.Title, rProjectVariable.Title));
                rSimHSCInput.ProjectVariablesRow = rProjectVariable;
                m_HabitatManager.ProjectDatabase.SimulationHSCInputs.AddSimulationHSCInputsRow(rSimHSCInput);
            }
        }

        private void AddFISSimulationChildRecords(ref dsHabitat.SimulationsRow rSimulation, RBTWorkbenchDataSet.CHAMP_VisitsRow rVisit, int nFISID, ref bool bRasterInputs)
        {
            dsHabitat.FISRow rFIS = m_HabitatManager.ProjectDatabase.FIS.FindByFISID(nFISID);

            // Loop over all the input curves and create the necessary project data sources and inputs
            dsHabitat.ProjectDataSourcesRow rCSVDataSource = null;

            string sFISRuleFile = Paths.GetAbsolutePath(rFIS.FISRuleFile);
            if (System.IO.File.Exists(sFISRuleFile))
            {
                FISRuleFile fis = new FISRuleFile(sFISRuleFile);
                foreach (string sInput in fis.FISInputs)
                {
                    dsHabitat.ProjectVariablesRow rProjectVariable = null;

                    if (string.Compare(sInput, "velocity", true) == 0 || string.Compare(sInput, "depth", true) == 0)
                    {
                        // Create a Data Source for the CSV
                        if (rCSVDataSource == null)
                        {
                            string sOriginalPath = System.IO.Path.Combine(m_dMonitoringDatafolder.FullName, rVisit.Folder);
                            sOriginalPath = System.IO.Path.Combine((new System.IO.DirectoryInfo(sOriginalPath)).Parent.FullName, "Hydro", rVisit.HydraulicModelCSV);
                            rCSVDataSource = BuildAndCopyProjectDataSource(rVisit.VisitID, "Delft 3D CSV Output", sOriginalPath, true, "csv");
                        }

                        if (sInput.ToLower().Contains("velocity"))
                        {
                            rProjectVariable = BuildProjectVariable("Velocity.Magnitude", "Velocity", 5, 22, rCSVDataSource.DataSourceID);
                        }
                        else
                            rProjectVariable = BuildProjectVariable("Depth", "Depth", 8, 8, rCSVDataSource.DataSourceID);
                    }
                    else if (string.Compare(sInput, "grainsize_mm", true) == 0)
                    {
                        // Create raster data source
                        string sOriginalPath = System.IO.Path.Combine(m_dMonitoringDatafolder.FullName, rVisit.Folder, rVisit.ICRPath);
                        dsHabitat.ProjectDataSourcesRow rSubstrateSource = BuildAndCopyProjectDataSource(rVisit.VisitID, "SubstrateRaster", sOriginalPath, false, "raster");

                        // Create project variable
                        rProjectVariable = BuildProjectVariable("D50", "D50", 12, 12, rSubstrateSource.DataSourceID);
                        bRasterInputs = true;
                    }
                    else
                        throw new Exception("Unhandled FIS input name '" + sInput + "'.");

                    // Insert the Simulation HSC input
                    dsHabitat.SimulationFISInputsRow rSimInput = m_HabitatManager.ProjectDatabase.SimulationFISInputs.NewSimulationFISInputsRow();
                    rSimInput.SimulationsRow = rSimulation;
                    rSimInput.FISInputName = sInput;
                    rSimInput.ProjectVariablesRow = rProjectVariable;
                    m_HabitatManager.ProjectDatabase.SimulationFISInputs.AddSimulationFISInputsRow(rSimInput);
                }
            }
        }

        private string GetSimulationName(RBTWorkbenchDataSet.CHAMP_VisitsRow rVisit, string sHSIName)
        {
            // CBW5583-34565_VISIT_217
            string sSiteName = Paths.RemoveDangerousCharacters(rVisit.CHAMP_SitesRow.SiteName);
            sHSIName = Paths.RemoveDangerousCharacters(sHSIName);

            string sResult = string.Format("{0}_VISIT_{1}_{2}", sSiteName, rVisit.VisitID.ToString(), sHSIName);
            return sResult;
        }

        /// <summary>
        /// Creates the record in the database for the project data source (CSV or raster)
        /// </summary>
        /// <param name="sDataSourceName">Habitat project data source name</param>
        /// <param name="sOriginalPath">Full, absolute path to the original file (CSV or TIF)</param>
        /// <param name="bCopySingleFile">When true this routine copies on the original file path. When false it wildcards the file name .* and copies all files.</param>
        /// <param name="sProjectInputType">"csv" or "raster". Used to lookup the lookuplist item ID in the habitat database</param>
        /// <returns>Remember that this is used for both CSV and raster data sources</returns>
        private dsHabitat.ProjectDataSourcesRow BuildAndCopyProjectDataSource(int nVisitID, string sDataSourceName, string sOriginalPath, Boolean bCopySingleFile, string sProjectInputType)
        {
            // check that the project data source does not already exist for this combination of visit ID and data source.
            string sProjectDataSourcePath = Paths.GetSpecificInputFullPath(String.Format("{0}_VisitID{1}", sDataSourceName, nVisitID), System.IO.Path.GetExtension(sOriginalPath));
            string sRelativeProjectDataSourcePath = Paths.GetRelativePath(sProjectDataSourcePath);
            foreach (dsHabitat.ProjectDataSourcesRow rDS in m_HabitatManager.ProjectDatabase.ProjectDataSources)
            {
                if (string.Compare(rDS.ProjectPath, sRelativeProjectDataSourcePath) == 0)
                    return rDS;
            }

            if (!System.IO.File.Exists(sOriginalPath))
                return null;

            int i = 0;
            string sFileName;
            bool bAnyFilesExist = false;
            do
            {
                sProjectDataSourcePath = Paths.GetSpecificInputFullPath(sDataSourceName, System.IO.Path.GetExtension(sOriginalPath));
                sFileName = System.IO.Path.GetFileNameWithoutExtension(sProjectDataSourcePath);
                if (i > 0)
                    sFileName = string.Format("{0}_{1}", sFileName, i);

                sProjectDataSourcePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(sProjectDataSourcePath), sFileName);
                sProjectDataSourcePath = System.IO.Path.ChangeExtension(sProjectDataSourcePath, System.IO.Path.GetExtension(sOriginalPath));
                string sFolder = System.IO.Path.GetDirectoryName(sProjectDataSourcePath);
                if (System.IO.Directory.Exists(sFolder))
                {
                    string[] sMatch = System.IO.Directory.GetFiles(sFolder, System.IO.Path.ChangeExtension(sFileName, "*"));
                    bAnyFilesExist = sMatch.Count<string>() > 0;
                }
                i++;

            } while (bAnyFilesExist);

            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(sProjectDataSourcePath));

            // TIF rasters require all the files to be copied.
            string sCopySource;
            if (bCopySingleFile)
                sCopySource = sOriginalPath;
            else
                sCopySource = System.IO.Path.ChangeExtension(sOriginalPath, "*");

            string[] sMatchingFiles = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(sCopySource), System.IO.Path.GetFileName(sCopySource));
            foreach (string sFileToCopy in sMatchingFiles)
            {
                string sCleanName = System.IO.Path.GetFileNameWithoutExtension(sProjectDataSourcePath);
                string sOrigFileName = System.IO.Path.GetFileNameWithoutExtension(sFileToCopy);
                string sFileMiddle = "";
                string sExtension = System.IO.Path.GetExtension(sFileToCopy);

                if (sOrigFileName.IndexOf('.') >= 0)
                {
                    sFileMiddle = sOrigFileName.Substring(sOrigFileName.IndexOf('.'));
                    sExtension = sFileMiddle + sExtension;
                }

                string sDestinationFile = System.IO.Path.ChangeExtension(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(sProjectDataSourcePath), sCleanName), sExtension);

                if (System.IO.File.Exists(sDestinationFile))
                    bAnyFilesExist = true;


                System.IO.File.Copy(sFileToCopy, sDestinationFile, false);
                if (!System.IO.File.Exists(sDestinationFile))
                    return null;
            }

            int nDataSourceTypeID = GetLookupListItemID("Project Input Types", sProjectInputType);

            dsHabitat.ProjectDataSourcesRow rDataSource = m_HabitatManager.ProjectDatabase.ProjectDataSources.NewProjectDataSourcesRow();
            rDataSource.OriginalPath = sOriginalPath;
            rDataSource.CreatedOn = DateTime.Now;
            rDataSource.DataSourceTypeID = nDataSourceTypeID;
            rDataSource.ProjectPath = Paths.GetRelativePath(sProjectDataSourcePath);
            rDataSource.Title = sDataSourceName;

            if (nDataSourceTypeID == m_nCSVDataSourceTypeID)
            {
                rDataSource.XField = "X";
                rDataSource.YField = "Y";
            }

            m_HabitatManager.ProjectDatabase.ProjectDataSources.AddProjectDataSourcesRow(rDataSource);
            return rDataSource;
        }


        private dsHabitat.ProjectVariablesRow BuildProjectVariable(string sValueField, string sVariableTitle, int nUnitID, int nVariableID, int nProjectDataSourceID)
        {
            dsHabitat.ProjectVariablesRow rProjectVariable = m_HabitatManager.ProjectDatabase.ProjectVariables.NewProjectVariablesRow();
            rProjectVariable.DataSourceID = nProjectDataSourceID;
            rProjectVariable.Title = sVariableTitle;
            rProjectVariable.UnitsID = nUnitID;
            rProjectVariable.VariableID = nVariableID;

            if (string.IsNullOrWhiteSpace(sValueField))
                rProjectVariable.SetValueFieldNull();
            else
                rProjectVariable.ValueField = sValueField;

            m_HabitatManager.ProjectDatabase.ProjectVariables.AddProjectVariablesRow(rProjectVariable);
            return rProjectVariable;
        }

        /// <summary>
        /// Find the lookup list item ID using the wildcard for the list and item name
        /// </summary>
        /// <param name="sListName"></param>
        /// <param name="sItemWildCard"></param>
        /// <param name="hData"></param>
        /// <returns></returns>
        private int GetLookupListItemID(string sListName, string sItemWildCard)
        {
            foreach (dsHabitat.LookupListItemsRow rItem in m_HabitatManager.ProjectDatabase.LookupListItems)
                if (rItem.LookupListsRow.ListName.ToLower().Contains(sListName.ToLower()))
                    if (rItem.ItemName.ToLower().Contains(sItemWildCard.ToLower()))
                        return rItem.ItemID;

            return -1;
        }
    }
}
