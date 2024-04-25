using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.IO;
using Pinokio.Database;
using Simulation.Engine;
using Pinokio.Model.Base.Structure;
using Pinokio.Model.Base;

namespace Pinokio.Model.User
{
    public class UserSimResultDBManager : SimResultDBManager
    {       
        public UserSimResultDBManager()
            :base()
        {
        }

        /// <summary>
        /// Simulation Results 파일에 Table 생성
        /// </summary>
        protected override void CreateSimResultTables()
        {
            base.CreateSimResultTables();

            //Please write your function to create SimResultTableAndColumns

            //CreateUserSimResultTableColumns(USER_RESULT_TABLE_TYPE.COMMAND_LOG);
            //CreateUserSimResultTableColumns(USER_RESULT_TABLE_TYPE.MR_LOG);
        }

        /// <summary>
        /// Simulation Results 파일에 Table 생성
        /// </summary>
        /// <param name="tableType"></param>
        /// <returns></returns>
        private bool CreateUserSimResultTableColumns(USER_RESULT_TABLE_TYPE tableType)
        {
            string sql = "CREATE TABLE ";

            switch (tableType)
            {
                //case USER_RESULT_TABLE_TYPE.COMMAND_LOG:
                //    sql += tableType.ToString() + "("
                //        + COMMAND_LOG_COL.ID.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.COMMAND_NAME.ToString() + " nvarchar(100), "
                //        + COMMAND_LOG_COL.ACTIVATED_TIME.ToString() + " DATE, "
                //        + COMMAND_LOG_COL.COMPLETED_TIME.ToString() + " DATE, "
                //        + COMMAND_LOG_COL.CS_NAME.ToString() + " nvarchar(100), "
                //        + COMMAND_LOG_COL.MR_ID.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.VEHICLE_ID.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.FROM_STATION.ToString() + " nvarchar(100), "
                //        //+ COMMAND_LOG_COL.FROM_STATION_TYPE.ToString() + " nvarchar(100), "
                //        + COMMAND_LOG_COL.TO_STATION.ToString() + " nvarchar(100), "
                //        //+ COMMAND_LOG_COL.TO_STATION_TYPE.ToString() + " nvarchar(100), "
                //        + COMMAND_LOG_COL.PART_ID.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.PART_NAME.ToString() + " nvarchar(100), "
                //        + COMMAND_LOG_COL.ASSIGNED_TIME.ToString() + " DATE, "
                //        + COMMAND_LOG_COL.LOADED_TIME.ToString() + " DATE, "
                //        + COMMAND_LOG_COL.WAITING_DISTANCE.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.TRANSFERRING_DISTANCE.ToString() + " DOUBLE, "
                //        //+ COMMAND_LOG_COL.VEHICLE_NAME.ToString() + " nvarchar(100), "
                //        + COMMAND_LOG_COL.SCHEDULE_COST.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.ACQUIRE_ROUTE_ID_LIST.ToString() + " LONGTEXT, "
                //        + COMMAND_LOG_COL.ACTUAL_ROUTE_ID_LIST.ToString() + " LONGTEXT, "
                //        + COMMAND_LOG_COL.LOADING_START_TIME.ToString() + " DATE, "
                //        + COMMAND_LOG_COL.UNLOADING_START_TIME.ToString() + " DATE, "
                //        + COMMAND_LOG_COL.TOTAL_TIME.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.WAITING_TIME.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.TRANSFERRING_TIME.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.QUEUED_TIME.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.LOADING_TIME.ToString() + " DOUBLE, "
                //        + COMMAND_LOG_COL.UNLOADING_TIME.ToString() + " DOUBLE "
                //        + ");";
                //    break;
                //case USER_RESULT_TABLE_TYPE.MR_LOG:
                //    sql += tableType.ToString() + "("
                //        + MR_LOG_COL.ID.ToString() + " DOUBLE, "
                //        + MR_LOG_COL.FROM_NODE_NAME.ToString() + " nvarchar(100), "
                //        + MR_LOG_COL.TO_NODE_NAME.ToString() + " nvarchar(100), "
                //        + MR_LOG_COL.PART_ID.ToString() + " DOUBLE, "
                //        + MR_LOG_COL.PART_NAME.ToString() + " nvarchar(100), "
                //        + MR_LOG_COL.ACTIVATED_TIME.ToString() + " DATE, "
                //        + MR_LOG_COL.COMPLETED_TIME.ToString() + " DATE, "
                //        + MR_LOG_COL.TOTAL_TIME.ToString() + " DOUBLE, "
                //        + MR_LOG_COL.COMMAND_COUNT.ToString() + " DOUBLE, "
                //        + MR_LOG_COL.WAY_POINT_COUNT.ToString() + " DOUBLE, "
                //        + MR_LOG_COL.ROUTE_ID_LIST.ToString() + " LONGTEXT "
                //        + ");";
                //    break;
                //case USER_RESULT_TABLE_TYPE.PART_STEP_LOG:
                //    sql += tableType.ToString() + "("
                //        + PART_STEP_LOG_COL.STEP_ID.ToString() + " DOUBLE, "
                //        + PART_STEP_LOG_COL.STEP_NAME.ToString() + " nvarchar(100), "
                //        + PART_STEP_LOG_COL.STEP_TYPE.ToString() + " nvarchar(100), "
                //        + PART_STEP_LOG_COL.EQP_ID.ToString() + " DOUBLE, "
                //        + PART_STEP_LOG_COL.EQP_NAME.ToString() + " nvarchar(100), "
                //        + PART_STEP_LOG_COL.PART_ID.ToString() + " DOUBLE, "
                //        + PART_STEP_LOG_COL.PART_NAME.ToString() + " nvarchar(100), "
                //        + PART_STEP_LOG_COL.PRODUCT_ID.ToString() + " DOUBLE, "
                //        + PART_STEP_LOG_COL.PRODUCT_NAME.ToString() + " nvarchar(100), "
                //        + PART_STEP_LOG_COL.QUEUE_START_TIME.ToString() + " DATE, "
                //        + PART_STEP_LOG_COL.ASSIGNED_TIME.ToString() + " DATE, "
                //        + PART_STEP_LOG_COL.TRACK_IN_TIME.ToString() + " DATE, "
                //        + PART_STEP_LOG_COL.STEP_START_TIME.ToString() + " DATE, "
                //        + PART_STEP_LOG_COL.STEP_END_TIME.ToString() + " DATE, "
                //        + PART_STEP_LOG_COL.TRACK_OUT_TIME.ToString() + " DATE "
                //        + ");";
                //    break;
                //case USER_RESULT_TABLE_TYPE.EQP_STEP_LOG:
                //    sql += tableType.ToString() + "("
                //        + EQP_STEP_LOG_COL.STEP_ID.ToString() + " DOUBLE, "
                //        + EQP_STEP_LOG_COL.STEP_NAME.ToString() + " nvarchar(100), "
                //        + EQP_STEP_LOG_COL.STEP_GROUP.ToString() + " nvarchar(100), "
                //        + EQP_STEP_LOG_COL.STEP_TYPE.ToString() + " nvarchar(100), "
                //        + EQP_STEP_LOG_COL.EQP_ID.ToString() + " DOUBLE, "
                //        + EQP_STEP_LOG_COL.EQP_NAME.ToString() + " nvarchar(100), "
                //        + EQP_STEP_LOG_COL.EQP_GROUP.ToString() + " nvarchar(100), "
                //        + EQP_STEP_LOG_COL.INPUT_PART_IDS.ToString() + " nvarchar(100), "
                //        + EQP_STEP_LOG_COL.INPUT_PART_NAMES.ToString() + " nvarchar(200), "
                //        + EQP_STEP_LOG_COL.OUTPUT_PART_IDS.ToString() + " nvarchar(100), "
                //        + EQP_STEP_LOG_COL.OUTPUT_PART_NAMES.ToString() + " nvarchar(200), "
                //        + EQP_STEP_LOG_COL.ACTIVATED_TIME.ToString() + " DATE, "
                //        + EQP_STEP_LOG_COL.ASSIGNED_TIME.ToString() + " DATE, "
                //        + EQP_STEP_LOG_COL.STEP_START_TIME.ToString() + " DATE, "
                //        + EQP_STEP_LOG_COL.STEP_END_TIME.ToString() + " DATE "
                //        + ");";
                //    break;
                //case USER_RESULT_TABLE_TYPE.FACTORY_INFO:
                //    sql += tableType.ToString() + "("
                //        + FACTORY_INFO_COL.FACTORY_NAME.ToString() + " nvarchar(100) "
                //        + ");";
                //    break;
                //case USER_RESULT_TABLE_TYPE.RESULT_TIME:
                //    sql += tableType.ToString() + "("
                //        + RESULT_TIME_COL.NAME.ToString() + " nvarchar(100), "
                //        + RESULT_TIME_COL.SIM_TIME.ToString() + " DATE "
                //        + ");";
                //    break;
                //    ///
                //case USER_RESULT_TABLE_TYPE.FACTORY_INOUT_LOG:
                //    sql += tableType.ToString() + "("
                //        + FACTORY_INOUT_COL.INOUT_TIME.ToString() + " DATE, "
                //        + FACTORY_INOUT_COL.NODE_ID.ToString() + " DOUBLE, "
                //        + FACTORY_INOUT_COL.NODE_NAME.ToString() + " nvarchar(100), "
                //        + FACTORY_INOUT_COL.PART_ID.ToString() + " nvarchar(100), "
                //        + FACTORY_INOUT_COL.PART_NAME.ToString() + " nvarchar(100), "
                //        + FACTORY_INOUT_COL.PRODUCT_ID.ToString() + " DOUBLE, "
                //        + FACTORY_INOUT_COL.PRODUCT_NAME.ToString() + " nvarchar(100), "
                //        + FACTORY_INOUT_COL.STATE.ToString() + " nvarchar(100) "
                //        + ");";
                //    break;
            }

            OleDbCommand cmd = new OleDbCommand(sql, AccessDB.Instance.Conn);
            cmd.ExecuteNonQuery();

            return true;
        }

        #region Example Function(Upload & Select)
        ///// <summary>
        ///// Completed Command를 Simulation Results에 업로드
        ///// </summary>
        ///// <param name="command"></param>
        //public void UploadCompletedCommandTrendLog(Command command)
        //{
        //    if (ModelManager.Instance.EngineWarmUpNode.IsWarmUp) return;

        //    List<object> values = new List<object>();

        //    values.Add(command.Id);
        //    values.Add(command.Name);
        //    values.Add(command.ActivatedDateTime);
        //    values.Add(command.CompletedDateTime);
        //    values.Add(command.CSName);
        //    values.Add(command.MRID);
        //    values.Add(command.Vehicle.ID);
        //    values.Add(command.StartStation.ID);
        //    values.Add(command.EndStation.ID);
        //    values.Add(command.Part.ID);
        //    values.Add(command.Part.Name);
        //    values.Add(command.AssignedDateTime);
        //    values.Add(command.LoadedDateTime);
        //    values.Add(command.WaitingDistance);
        //    values.Add(command.TransferringDistance);
        //    values.Add(command.ScheduleCost);
        //    values.Add(command.AcquireRouteIDString);
        //    values.Add(command.ActualRouteIDString);
        //    values.Add(command.LoadingStartDateTime);
        //    values.Add(command.UnloadingStartDateTime);
        //    values.Add(command.TotalTime);
        //    values.Add(command.WaitingTime);
        //    values.Add(command.TransferringTime);
        //    values.Add(command.QueuedTime);
        //    values.Add(command.LoadingTime);
        //    values.Add(command.UnloadingTime);

        //    AccessDB.Instance.Insert(RESULT_TABLE_TYPE.COMMAND_LOG.ToString(), values);
        //}

        ///// <summary>
        ///// MR Command를 Simulation Results에 업로드
        ///// </summary>
        ///// <param name="mr"></param>
        //public void UploadCompletedMRTrendLog(MR mr)
        //{
        //    if (ModelManager.Instance.EngineWarmUpNode.IsWarmUp) return;

        //    List<object> values = new List<object>();

        //    values.Add(mr.Id);
        //    values.Add(mr.StartNode.Name);
        //    values.Add(mr.EndNode.Name);
        //    values.Add(mr.Part.ID);
        //    values.Add(mr.PartName);
        //    values.Add(mr.ActivatedDateTime);
        //    values.Add(mr.CompletedDateTime);
        //    values.Add(mr.TotalTime);
        //    values.Add(mr.CommandCount);
        //    values.Add(mr.WayPointCount);
        //    values.Add(mr.RouteIDString);
        //    AccessDB.Instance.Insert(RESULT_TABLE_TYPE.MR_LOG.ToString(), values);
        //}

        ///// <summary>
        ///// Completed Part Step을 Simulation Results에 업로드
        ///// </summary>
        ///// <param name="command"></param>
        //public void UploadCompletedPartStepTrendLog(PartStep partStep)
        //{
        //    if (ModelManager.Instance.EngineWarmUpNode.IsWarmUp) return;

        //    List<object> values = new List<object>();

        //    values.Add(partStep.StepID);
        //    values.Add(partStep.StepName);
        //    values.Add(partStep.StepType);
        //    values.Add(partStep.EqpID);
        //    values.Add(partStep.EqpName);
        //    values.Add(partStep.PartID);
        //    values.Add(partStep.PartName);
        //    values.Add(partStep.ProductID);
        //    values.Add(partStep.ProductName);
        //    values.Add(partStep.WipStartDateTime);
        //    values.Add(partStep.AssignedDateTime);
        //    values.Add(partStep.TrackInDateTime);
        //    values.Add(partStep.StepStartDateTime);
        //    values.Add(partStep.StepEndDateTime);
        //    values.Add(partStep.TrackOutDateTime);

        //    AccessDB.Instance.Insert(RESULT_TABLE_TYPE.PART_STEP_LOG.ToString(), values);
        //}

        ///// <summary>
        ///// Completed Part Step을 Simulation Results에 업로드
        ///// </summary>
        ///// <param name="command"></param>
        //public void UploadCompletedEqpStepTrendLog(EqpStep eqpStep)
        //{
        //    if (ModelManager.Instance.EngineWarmUpNode.IsWarmUp) return;

        //    List<object> values = new List<object>();

        //    values.Add(eqpStep.StepID);
        //    values.Add(eqpStep.StepName);
        //    values.Add(eqpStep.StepGroup);
        //    values.Add(eqpStep.StepType);
        //    values.Add(eqpStep.EqpID);
        //    values.Add(eqpStep.EqpName);
        //    values.Add(eqpStep.EqpGroup);
        //    eqpStep.StringInputPartIDs = eqpStep.GetStringInputPartIDs();
        //    eqpStep.StringInputPartNames = eqpStep.GetStringInputPartNames();
        //    values.Add(eqpStep.StringInputPartIDs);
        //    values.Add(eqpStep.StringInputPartNames);
        //    eqpStep.StringOutputPartIDs = eqpStep.GetStringOutputPartIDs();
        //    eqpStep.StringOutputPartNames = eqpStep.GetStringOutputPartNames();
        //    values.Add(eqpStep.StringOutputPartIDs);
        //    values.Add(eqpStep.StringOutputPartNames);
        //    values.Add(eqpStep.ActivatedDateTime);
        //    values.Add(eqpStep.AssignedDateTime);
        //    values.Add(eqpStep.StepStartDateTime);
        //    values.Add(eqpStep.StepEndDateTime);

        //    AccessDB.Instance.Insert(RESULT_TABLE_TYPE.EQP_STEP_LOG.ToString(), values);
        //}

        ///// <summary>
        ///// Factory Inout을 Simulation Results에 업로드
        ///// </summary>
        ///// <param name="command"></param>
        //public void UploadFactoryInoutLog(FactoryInout factoryInout)
        //{
        //    if (ModelManager.Instance.EngineWarmUpNode.IsWarmUp) return;

        //    List<object> values = new List<object>();

        //    values.Add(factoryInout.InoutTime);
        //    values.Add(factoryInout.NodeID);
        //    values.Add(factoryInout.NodeName);
        //    values.Add(factoryInout.PartID);
        //    values.Add(factoryInout.PartName);
        //    values.Add(factoryInout.ProductID);
        //    values.Add(factoryInout.ProductName);
        //    values.Add(factoryInout.State);

        //    AccessDB.Instance.Insert(RESULT_TABLE_TYPE.FACTORY_INOUT_LOG.ToString(), values);
        //}

        ///// <summary>
        ///// Sim Start / End Time을 Simultion Results에 업로드
        ///// </summary>
        //public void UploadResultTimeTable()
        //{
        //    List<object> values = new List<object>();

        //    values.Add(RESULT_TIME_LOW.SIMULATION_START_TIME.ToString());
        //    values.Add(SimEngine.Instance.StartDateTime);
        //    AccessDB.Instance.Insert(RESULT_TABLE_TYPE.RESULT_TIME.ToString(), values);

        //    values.Clear();

        //    values.Add(RESULT_TIME_LOW.SIMULATION_END_TIME.ToString());
        //    values.Add(SimEngine.Instance.EndDateTime);
        //    AccessDB.Instance.Insert(RESULT_TABLE_TYPE.RESULT_TIME.ToString(), values);
        //}

        ///// <summary>
        ///// 검색조건에 맞는 Command 모두 검색
        ///// </summary>
        ///// <param name="fromTime"></param>
        ///// <param name="toTime"></param>
        ///// <param name="selectedCSName"></param>
        ///// <returns></returns>
        //public Dictionary<Command, string> SelectCommandLog(DateTime fromTime, DateTime toTime, string selectedCSName, List<string> selectedSubCSName, bool isLoad)
        //{
        //    Dictionary<Command, string> dicCommand = new Dictionary<Command, string>();

        //    string connString;
        //    string dbPath;
        //    OleDbConnection conn;

        //    if (isLoad)
        //    {
        //        connString = _Load_connString;
        //        dbPath = _LoadDBPath;
        //        conn = _Load_conn;
        //    }
        //    else
        //    {
        //        connString = _connString;
        //        dbPath = _DBPath;
        //        conn = _conn;
        //    }

        //    if (AccessDB.Instance.ConnectDB(ref connString, ref dbPath, ref conn))
        //    {
        //        double ID;
        //        string commandName;
        //        DateTime activatedTime;
        //        DateTime completedTime;
        //        string csName;
        //        double mrID;
        //        uint vehicleID;
        //        uint startStationID;
        //        //string fromStationType;
        //        uint endStationID;
        //        //string toStationType;
        //        //double partID;
        //        DateTime assignedTime;
        //        DateTime loadedTime;
        //        double waitingDistance;
        //        double transferringDistance;
        //        //string vehicleName;
        //        double scheduleCost;
        //        string AcquireRouteIDString;
        //        string ActualRouteIDString;
        //        DateTime loadingStartTime;
        //        DateTime unloadingStartTime;
        //        double totalTime;
        //        double waitingTime;
        //        double transferringTime;
        //        double queuedTime;
        //        double loadingTime;
        //        double unloadingTime;

        //        string query = string.Empty;
        //        query = GetCommandLogQuery(fromTime, toTime, selectedCSName, selectedSubCSName, RESULT_TABLE_TYPE.COMMAND_LOG.ToString());

        //        OleDbCommand cmd = new OleDbCommand(query, conn);
        //        cmd.ExecuteNonQuery();

        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read() == true)
        //        {
        //            ID = Convert.ToDouble(reader[COMMAND_LOG_COL.ID.ToString()].ToString());
        //            commandName = reader[COMMAND_LOG_COL.COMMAND_NAME.ToString()].ToString();
        //            activatedTime = Convert.ToDateTime(reader[COMMAND_LOG_COL.ACTIVATED_TIME.ToString()].ToString());
        //            completedTime = Convert.ToDateTime(reader[COMMAND_LOG_COL.COMPLETED_TIME.ToString()].ToString());
        //            csName = reader[COMMAND_LOG_COL.CS_NAME.ToString()].ToString();
        //            mrID = Convert.ToDouble(reader[COMMAND_LOG_COL.MR_ID.ToString()].ToString());
        //            vehicleID = Convert.ToUInt16(reader[COMMAND_LOG_COL.VEHICLE_ID.ToString()].ToString());
        //            startStationID = Convert.ToUInt16(reader[COMMAND_LOG_COL.FROM_STATION.ToString()].ToString());
        //            //fromStationType = reader[COMMAND_LOG_COL.FROM_STATION.ToString()].ToString();
        //            endStationID = Convert.ToUInt16(reader[COMMAND_LOG_COL.TO_STATION.ToString()].ToString());
        //            //toStationType = reader[COMMAND_LOG_COL.FROM_STATION.ToString()].ToString();
        //            //partID = Convert.ToDouble(reader[COMMAND_LOG_COL.PART_ID.ToString()].ToString());
        //            assignedTime = Convert.ToDateTime(reader[COMMAND_LOG_COL.ASSIGNED_TIME.ToString()].ToString());
        //            loadedTime = Convert.ToDateTime(reader[COMMAND_LOG_COL.LOADED_TIME.ToString()].ToString());
        //            waitingDistance = Convert.ToDouble(reader[COMMAND_LOG_COL.WAITING_DISTANCE.ToString()].ToString());
        //            transferringDistance = Convert.ToDouble(reader[COMMAND_LOG_COL.TRANSFERRING_DISTANCE.ToString()].ToString());
        //            //vehicleName = reader[COMMAND_LOG_COL.VEHICLE_NAME.ToString()].ToString();
        //            scheduleCost = Convert.ToDouble(reader[COMMAND_LOG_COL.SCHEDULE_COST.ToString()].ToString());
        //            AcquireRouteIDString = reader[COMMAND_LOG_COL.ACQUIRE_ROUTE_ID_LIST.ToString()].ToString();
        //            ActualRouteIDString = reader[COMMAND_LOG_COL.ACTUAL_ROUTE_ID_LIST.ToString()].ToString();
        //            loadingStartTime = Convert.ToDateTime(reader[COMMAND_LOG_COL.LOADING_START_TIME.ToString()].ToString());
        //            unloadingStartTime = Convert.ToDateTime(reader[COMMAND_LOG_COL.UNLOADING_START_TIME.ToString()].ToString());
        //            totalTime = Convert.ToDouble(reader[COMMAND_LOG_COL.TOTAL_TIME.ToString()].ToString());
        //            waitingTime = Convert.ToDouble(reader[COMMAND_LOG_COL.WAITING_TIME.ToString()].ToString());
        //            transferringTime = Convert.ToDouble(reader[COMMAND_LOG_COL.TRANSFERRING_TIME.ToString()].ToString());
        //            queuedTime = Convert.ToDouble(reader[COMMAND_LOG_COL.QUEUED_TIME.ToString()].ToString());
        //            loadingTime = Convert.ToDouble(reader[COMMAND_LOG_COL.LOADING_TIME.ToString()].ToString());
        //            unloadingTime = Convert.ToDouble(reader[COMMAND_LOG_COL.UNLOADING_TIME.ToString()].ToString());

        //            Command command = new Command();
        //            command.Id = Convert.ToUInt32(ID);
        //            command.Name = commandName;
        //            command.MRID = Convert.ToUInt32(mrID);
        //            command.CSName = csName;
        //            command.ActivatedDateTime = activatedTime;
        //            command.CompletedDateTime = completedTime;
        //            command.AssignedDateTime = assignedTime;
        //            command.LoadedDateTime = loadedTime;

        //            string[] splitString = AcquireRouteIDString.Split('/');
        //            foreach (string lineID in splitString)
        //            {
        //                if (lineID == "")
        //                    continue;

        //                TransportLine line = (TransportLine)ModelManager.Instance.GetSimNodebyID(Convert.ToUInt32(lineID));
        //                command.AcquireRoute.Add(line);
        //            }

        //            splitString = ActualRouteIDString.Split('/');
        //            foreach (string lineID in splitString)
        //            {
        //                if (lineID == "")
        //                    continue;

        //                TransportLine line = ModelManager.Instance.GetSimNodebyID(Convert.ToUInt32(lineID)) as TransportLine;
        //                command.ActualRoute.Add(line);
        //            }

        //            command.LoadingStartDateTime = loadingStartTime;
        //            command.UnloadingStartDateTime = unloadingStartTime;
        //            command.WaitingDistance = waitingDistance;
        //            command.TransferringDistance = transferringDistance;
        //            command.ScheduleCost = scheduleCost;
        //            command.Vehicle = (Vehicle)ModelManager.Instance.SimNodes[vehicleID];
        //            command.StartStation = (Station)ModelManager.Instance.SimNodes[startStationID];
        //            command.EndStation = (Station)ModelManager.Instance.SimNodes[endStationID];

        //            dicCommand.Add(command, commandName);
        //        }
        //        reader.Close();
        //        cmd.Dispose();
        //    }

        //    return dicCommand;
        //}


        //public Dictionary<MR, double> SelectMRLog(DateTime fromTime, DateTime toTime, List<string> selectedStartMRName, List<string> selectedEndMRName, bool isLoad,bool isEndBtn)
        //{
        //    Dictionary<MR, double> dicMR = new Dictionary<MR, double>();

        //    string connString;
        //    string dbPath;
        //    OleDbConnection conn;

        //    if (isLoad)
        //    {
        //        connString = _Load_connString;
        //        dbPath = _LoadDBPath;
        //        conn = _Load_conn;
        //    }
        //    else
        //    {
        //        connString = _connString;
        //        dbPath = _DBPath;
        //        conn = _conn;
        //    }

        //    if (AccessDB.Instance.ConnectDB(ref connString, ref dbPath, ref conn))
        //    {
        //        double ID;
        //        DateTime activatedTime;
        //        DateTime completedTime;
        //        uint commandCount;
        //        uint wayPointCount;
        //        string startNodeName;
        //        string endNodeName;
        //        uint partID;
        //        string partName;
        //        string routeIDString;

        //        string query = string.Empty;
        //        if (isEndBtn)
        //        {

        //            query = GetMRLogQuery(fromTime, toTime, selectedStartMRName, selectedEndMRName, RESULT_TABLE_TYPE.MR_LOG.ToString(), isEndBtn);
        //            _endMRNames.Clear();
        //        }
        //        else
        //        {
        //            query = GetMRLogQuery(fromTime, toTime, selectedStartMRName, selectedEndMRName, RESULT_TABLE_TYPE.MR_LOG.ToString(), isEndBtn);
        //        }

        //        OleDbCommand cmd = new OleDbCommand(query, conn);
        //        cmd.ExecuteNonQuery();

        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read() == true)
        //        {
        //            ID = Convert.ToDouble(reader[MR_LOG_COL.ID.ToString()].ToString());
        //            activatedTime = Convert.ToDateTime(reader[MR_LOG_COL.ACTIVATED_TIME.ToString()].ToString());
        //            completedTime = Convert.ToDateTime(reader[MR_LOG_COL.COMPLETED_TIME.ToString()].ToString());
        //            commandCount = Convert.ToUInt32(reader[MR_LOG_COL.COMMAND_COUNT.ToString()].ToString());
        //            startNodeName = reader[MR_LOG_COL.FROM_NODE_NAME.ToString()].ToString();
        //            endNodeName = reader[MR_LOG_COL.TO_NODE_NAME.ToString()].ToString();
        //            partID = Convert.ToUInt32(reader[MR_LOG_COL.PART_ID.ToString()].ToString());
        //            partName = reader[MR_LOG_COL.PART_NAME.ToString()].ToString();
        //            wayPointCount = Convert.ToUInt32(reader[MR_LOG_COL.WAY_POINT_COUNT.ToString()].ToString());
        //            routeIDString = reader[MR_LOG_COL.ROUTE_ID_LIST.ToString()].ToString();

        //            MR mr = new MR();
        //            mr.Id = Convert.ToUInt32(ID);
        //            mr.ActivatedDateTime = activatedTime;
        //            mr.CompletedDateTime = completedTime;
        //            mr.CommandCount = commandCount;
        //            mr.StartNodeName = startNodeName;
        //            mr.EndNodeName = endNodeName;
        //            mr.PartName = partName;
        //            mr.WayPointCount = wayPointCount;
        //            if (isEndBtn&&!_endMRNames.Contains(mr.EndNodeName))
        //            {
        //                _endMRNames.Add(mr.EndNodeName);
        //            }

        //            string[] splitString = routeIDString.Split('/');
        //            foreach (string nodeID in splitString)
        //            {
        //                if (nodeID == "")
        //                    continue;

        //                TXNode line = (TXNode)ModelManager.Instance.GetSimNodebyID(Convert.ToUInt32(nodeID));
        //                mr.Route.Add(line);
        //            }

        //            dicMR.Add(mr, ID);
        //        }

        //        reader.Close();
        //        cmd.Dispose();
        //    }

        //    return dicMR;
        //}
        //public Dictionary<PartStep, string> SelectPartStepLog(DateTime fromTime, DateTime toTime, List<string> selectedEqpName, bool isLoad, bool isStep = false,bool isProduct=false)
        //{
        //    Dictionary<PartStep, string> dicPartStep = new Dictionary<PartStep, string>();

        //    string connString;
        //    string dbPath;
        //    OleDbConnection conn;

        //    if (isLoad)
        //    {
        //        connString = _Load_connString;
        //        dbPath = _LoadDBPath;
        //        conn = _Load_conn;
        //    }
        //    else
        //    {
        //        connString = _connString;
        //        dbPath = _DBPath;
        //        conn = _conn;
        //    }

        //    if (AccessDB.Instance.ConnectDB(ref connString, ref dbPath, ref conn))
        //    {
        //        uint stepID;
        //        string stepName;
        //        STEP_TYPE stepType;
        //        uint eqpID;
        //        string eqpName;
        //        uint partID;
        //        string partName;
        //        uint productID;
        //        string productName;
        //        DateTime queueStartTime;
        //        DateTime assignedTime;
        //        DateTime trackInTime;
        //        DateTime stepStartTime;
        //        DateTime stepEndTime;
        //        DateTime trackOutTime;

        //        string query = string.Empty;
        //        query = GetPartStepLogQuery(fromTime, toTime, selectedEqpName, RESULT_TABLE_TYPE.PART_STEP_LOG.ToString(), isStep,isProduct);

        //        OleDbCommand cmd = new OleDbCommand(query, conn);
        //        cmd.ExecuteNonQuery();

        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read() == true)
        //        {
        //            stepID = Convert.ToUInt32(reader[PART_STEP_LOG_COL.STEP_ID.ToString()].ToString());
        //            stepName = reader[PART_STEP_LOG_COL.STEP_NAME.ToString()].ToString();
        //            stepType = (STEP_TYPE)Enum.Parse(typeof(STEP_TYPE), reader[PART_STEP_LOG_COL.STEP_TYPE.ToString()].ToString());
        //            eqpID = Convert.ToUInt32(reader[PART_STEP_LOG_COL.EQP_ID.ToString()].ToString());
        //            eqpName = reader[PART_STEP_LOG_COL.EQP_NAME.ToString()].ToString();
        //            partID = Convert.ToUInt32(reader[PART_STEP_LOG_COL.PART_ID.ToString()].ToString());
        //            partName = reader[PART_STEP_LOG_COL.PART_NAME.ToString()].ToString();
        //            productID = Convert.ToUInt32(reader[PART_STEP_LOG_COL.PRODUCT_ID.ToString()].ToString());
        //            productName= reader[PART_STEP_LOG_COL.PRODUCT_NAME.ToString()].ToString();
        //            queueStartTime = Convert.ToDateTime(reader[PART_STEP_LOG_COL.QUEUE_START_TIME.ToString()].ToString());
        //            assignedTime = Convert.ToDateTime(reader[PART_STEP_LOG_COL.ASSIGNED_TIME.ToString()].ToString());
        //            trackInTime = Convert.ToDateTime(reader[PART_STEP_LOG_COL.TRACK_IN_TIME.ToString()].ToString());
        //            stepStartTime = Convert.ToDateTime(reader[PART_STEP_LOG_COL.STEP_START_TIME.ToString()].ToString());
        //            stepEndTime = Convert.ToDateTime(reader[PART_STEP_LOG_COL.STEP_END_TIME.ToString()].ToString());
        //            trackOutTime = Convert.ToDateTime(reader[PART_STEP_LOG_COL.TRACK_OUT_TIME.ToString()].ToString());

        //            PartStep partStep = new PartStep(stepID, stepName, stepType, eqpID, eqpName, partID, partName, productID,productName);
        //            partStep.WipStartDateTime = queueStartTime;
        //            partStep.AssignedDateTime = assignedTime;
        //            partStep.TrackInDateTime = trackInTime;
        //            partStep.StepStartDateTime = stepStartTime;
        //            partStep.StepEndDateTime = stepEndTime;
        //            partStep.TrackOutDateTime = trackOutTime;

        //            dicPartStep.Add(partStep, partName + "-" + partID + "-" + stepName + "-" + queueStartTime.ToString("yyyy-MM-dd HH:mm:ss"));
        //        }
        //        reader.Close();
        //        cmd.Dispose();
        //    }

        //    return dicPartStep;
        //}

        //public Dictionary<EqpStep, string> SelectEqpStepLog(DateTime fromTime, DateTime toTime, List<string> selectedGroupByName, List<string> selectedGroupName, bool isLoad, bool isEqpGroup = false, bool isCheckGroup=false)
        //{
        //    Dictionary<EqpStep, string> dicEqpStep = new Dictionary<EqpStep, string>();

        //    string connString;
        //    string dbPath;
        //    OleDbConnection conn;

        //    if (isLoad)
        //    {
        //        connString = _Load_connString;
        //        dbPath = _LoadDBPath;
        //        conn = _Load_conn;
        //    }
        //    else
        //    {
        //        connString = _connString;
        //        dbPath = _DBPath;
        //        conn = _conn;
        //    }

        //    if (AccessDB.Instance.ConnectDB(ref connString, ref dbPath, ref conn))
        //    {
        //        uint stepID;
        //        string stepName;
        //        string stepGroup;
        //        STEP_TYPE stepType;
        //        uint eqpID;
        //        string eqpName;
        //        string eqpGroup;
        //        string inputPartIDs;
        //        string inputPartNames;
        //        string outputPartIDs;
        //        string outputPartNames;
        //        DateTime activatedTime;
        //        DateTime assignedTime;
        //        DateTime stepStartTime;
        //        DateTime stepEndTime;

        //        string query = string.Empty;
        //        query = GetEqpStepLogQuery(fromTime, toTime, selectedGroupByName, selectedGroupName, RESULT_TABLE_TYPE.EQP_STEP_LOG.ToString(), isEqpGroup,isCheckGroup);

        //        OleDbCommand cmd = new OleDbCommand(query, conn);
        //        cmd.ExecuteNonQuery();

        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read() == true)
        //        {
        //            stepID = Convert.ToUInt32(reader[EQP_STEP_LOG_COL.STEP_ID.ToString()].ToString());
        //            stepName = reader[EQP_STEP_LOG_COL.STEP_NAME.ToString()].ToString();
        //            stepGroup =reader[EQP_STEP_LOG_COL.STEP_GROUP.ToString()].ToString();
        //            stepType = (STEP_TYPE)Enum.Parse(typeof(STEP_TYPE), reader[EQP_STEP_LOG_COL.STEP_TYPE.ToString()].ToString());
        //            eqpID = Convert.ToUInt32(reader[EQP_STEP_LOG_COL.EQP_ID.ToString()].ToString());
        //            eqpName = reader[EQP_STEP_LOG_COL.EQP_NAME.ToString()].ToString();
        //            eqpGroup = reader[EQP_STEP_LOG_COL.EQP_GROUP.ToString()].ToString();
        //            inputPartIDs = reader[EQP_STEP_LOG_COL.INPUT_PART_IDS.ToString()].ToString();
        //            inputPartNames = reader[EQP_STEP_LOG_COL.INPUT_PART_NAMES.ToString()].ToString();
        //            outputPartIDs = reader[EQP_STEP_LOG_COL.OUTPUT_PART_IDS.ToString()].ToString();
        //            outputPartNames = reader[EQP_STEP_LOG_COL.OUTPUT_PART_NAMES.ToString()].ToString();
        //            activatedTime = Convert.ToDateTime(reader[EQP_STEP_LOG_COL.ACTIVATED_TIME.ToString()].ToString());
        //            assignedTime = Convert.ToDateTime(reader[EQP_STEP_LOG_COL.ASSIGNED_TIME.ToString()].ToString());
        //            stepStartTime = Convert.ToDateTime(reader[EQP_STEP_LOG_COL.STEP_START_TIME.ToString()].ToString());
        //            stepEndTime = Convert.ToDateTime(reader[EQP_STEP_LOG_COL.STEP_END_TIME.ToString()].ToString());

        //            EqpStep eqpStep = new EqpStep(stepID, stepName,stepGroup, stepType, eqpID, eqpName,eqpGroup, inputPartIDs, inputPartNames, outputPartIDs, outputPartNames);
        //            eqpStep.ActivatedDateTime = activatedTime;
        //            eqpStep.AssignedDateTime = assignedTime;
        //            eqpStep.StepStartDateTime = stepStartTime;
        //            eqpStep.StepEndDateTime = stepEndTime;

        //            dicEqpStep.Add(eqpStep, eqpName + "-" + eqpID + "-" + stepName + "-" + activatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
        //        }
        //        reader.Close();
        //        cmd.Dispose();
        //    }

        //    return dicEqpStep;
        //}

        //public Dictionary<FactoryInout, string> SelectFactoryInoutLog(DateTime fromTime, DateTime toTime, List<string> SelectedNodeInoutNames, bool isLoad,bool isNode=false)
        //{
        //    Dictionary<FactoryInout, string> dicFactoryInout = new Dictionary<FactoryInout, string>();

        //    List<object> listFactoryInout = new List<object>();


        //    string connString;
        //    string dbPath;
        //    OleDbConnection conn;

        //    if (isLoad)
        //    {
        //        connString = _Load_connString;
        //        dbPath = _LoadDBPath;
        //        conn = _Load_conn;
        //    }
        //    else
        //    {
        //        connString = _connString;
        //        dbPath = _DBPath;
        //        conn = _conn;
        //    }

        //    if (AccessDB.Instance.ConnectDB(ref connString, ref dbPath, ref conn))
        //    {
        //        DateTime inoutTime;
        //        uint nodeID;
        //        string nodeName;
        //        string partID;
        //        string partName;
        //        string state;
        //        uint productID;
        //        string productName;

        //        string query = string.Empty;
        //        query = GetFactoryInoutLogQuery(fromTime, toTime, SelectedNodeInoutNames, RESULT_TABLE_TYPE.FACTORY_INOUT_LOG.ToString(),isNode);

        //        OleDbCommand cmd = new OleDbCommand(query, conn);
        //        cmd.ExecuteNonQuery();

        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read() == true)
        //        {
        //            inoutTime = Convert.ToDateTime(reader[FACTORY_INOUT_COL.INOUT_TIME.ToString()].ToString());
        //            nodeID = Convert.ToUInt32(reader[FACTORY_INOUT_COL.NODE_ID.ToString()].ToString());
        //            nodeName = reader[FACTORY_INOUT_COL.NODE_NAME.ToString()].ToString();
        //            partID = reader[FACTORY_INOUT_COL.PART_ID.ToString()].ToString();
        //            partName = reader[FACTORY_INOUT_COL.PART_NAME.ToString()].ToString();
        //            state = reader[FACTORY_INOUT_COL.STATE.ToString()].ToString();
        //            productID = Convert.ToUInt32(reader[FACTORY_INOUT_COL.PRODUCT_ID.ToString()].ToString());
        //            productName = reader[FACTORY_INOUT_COL.PRODUCT_NAME.ToString()].ToString();


        //            FactoryInout factoryInout = new FactoryInout(inoutTime, nodeID, nodeName, partID, partName, state,productID,productName);

        //            dicFactoryInout.Add(factoryInout, state);

        //            object[] arrayFactoryInout = new object[8] { inoutTime, nodeID, nodeName, partID, partName, state , productID,productName };
        //            listFactoryInout.Add(arrayFactoryInout);

        //        }
        //        reader.Close();
        //        cmd.Dispose();
        //    }

        //    return dicFactoryInout;
        //}

        ///// <summary>
        ///// 검색할 Command Query 생성
        ///// </summary>
        ///// <param name="fromTime"></param>
        ///// <param name="toTime"></param>
        ///// <param name="csName"></param>
        ///// <param name="tableName"></param>
        ///// <returns></returns>
        //public string GetCommandLogQuery(DateTime fromTime, DateTime toTime, string csName, List<string> selectedCSNames, string tableName)
        //{
        //    string query = "SELECT * FROM " + tableName + " WHERE " + COMMAND_LOG_COL.ACTIVATED_TIME + " >= #" + fromTime.ToString("yyyy/MM/dd HH:mm:ss")
        //                    + "# AND " + COMMAND_LOG_COL.ACTIVATED_TIME + " < #" + toTime.ToString("yyyy/MM/dd HH:mm:ss") + "#";

        //    if (csName != string.Empty && csName != "ALL")
        //        query = query + " AND " + COMMAND_LOG_COL.CS_NAME + " = " + $"\"{csName}\"";

        //    if (selectedCSNames.Count > 1)
        //    {
        //        query = query + " AND ( ";
        //        foreach (string csNames in selectedCSNames)
        //        {
        //            query = query + COMMAND_LOG_COL.CS_NAME + " = " + $"\"{csNames}\"";

        //            if (csNames != selectedCSNames.Last())
        //                query = query + " OR ";
        //        }
        //        query = query + ") ";
        //    }


        //    return query;
        //}
        //public string GetMRLogQuery(DateTime fromTime, DateTime toTime, List<string> selectedStartMRName, List<string> selectedEndMRName, string tableName, bool isEndbtn)
        //{
        //    string query = "SELECT * FROM " + tableName + " WHERE " + MR_LOG_COL.ACTIVATED_TIME + " >= #" + fromTime.ToString("yyyy/MM/dd HH:mm:ss")
        //                    + "# AND " + MR_LOG_COL.ACTIVATED_TIME + " < #" + toTime.ToString("yyyy/MM/dd HH:mm:ss") + "#";

        //    if (selectedStartMRName.Count > 0)
        //    {
        //        query = query + " AND ( ";
        //        foreach (string mrName in selectedStartMRName)
        //        {
        //            if (!mrName.Contains("ALL"))
        //            {

        //                    query = query + MR_LOG_COL.FROM_NODE_NAME + " = " + $"\"{mrName}\"";


        //                if (mrName != selectedStartMRName.Last())
        //                    query = query + " OR ";
        //            }
        //        }
        //        query = query + ") ";
        //    }
        //    if (selectedEndMRName.Count > 0&&!isEndbtn&& selectedStartMRName.Count > 0)
        //    {
        //        query = query + " AND ( ";
        //        foreach (string mrName in selectedEndMRName)
        //        {
        //            if (!mrName.Contains("ALL"))
        //            {

        //                query = query + MR_LOG_COL.TO_NODE_NAME + " = " + $"\"{mrName}\"";


        //                if (mrName != selectedEndMRName.Last())
        //                    query = query + " OR ";
        //            }
        //        }
        //        query = query + ") ";
        //    }

        //    return query;
        //}

        //public string GetPartStepLogQuery(DateTime fromTime, DateTime toTime, List<string> selectedEqpNames, string tableName, bool isStep,bool isProduct)
        //{
        //    string query = "SELECT * FROM " + tableName + " WHERE " + PART_STEP_LOG_COL.QUEUE_START_TIME + " >= #" + fromTime.ToString("yyyy/MM/dd HH:mm:ss")
        //                    + "# AND " + PART_STEP_LOG_COL.QUEUE_START_TIME + " < #" + toTime.ToString("yyyy/MM/dd HH:mm:ss") + "#";

        //    if (selectedEqpNames.Count > 0)
        //    {
        //        query = query + " AND ( ";
        //        foreach (string eqpName in selectedEqpNames)
        //        {
        //            if (!eqpName.Contains("ALL"))
        //            {
        //                if (isStep && !isProduct)
        //                {
        //                    query = query + PART_STEP_LOG_COL.STEP_NAME + " = " + $"\"{eqpName}\"";
        //                }
        //                else if (isProduct && !isStep)
        //                {
        //                    query = query + PART_STEP_LOG_COL.PRODUCT_NAME + " = " + $"{eqpName}";
        //                }
        //                else
        //                {
        //                    query = query + PART_STEP_LOG_COL.EQP_NAME + " = " + $"\"{eqpName}\"";
        //                }

        //                if (eqpName != selectedEqpNames.Last())
        //                    query = query + " OR ";
        //            }
        //        }
        //        query = query + ") ";
        //    }

        //    return query;
        //}

        //public string GetEqpStepLogQuery(DateTime fromTime, DateTime toTime, List<string> selectedGroupByNames, List<string> selectedGroupNames, string tableName, bool isEqpGroup,bool isCheckGroup)
        //{
        //    string query = "SELECT * FROM " + tableName + " WHERE " + EQP_STEP_LOG_COL.ACTIVATED_TIME + " >= #" + fromTime.ToString("yyyy/MM/dd HH:mm:ss")
        //                    + "# AND " + EQP_STEP_LOG_COL.ACTIVATED_TIME + " < #" + toTime.ToString("yyyy/MM/dd HH:mm:ss") + "#";

        //    if (selectedGroupByNames.Count > 0)
        //    {
        //        query = query + " AND ( ";
        //        foreach (string groupByName in selectedGroupByNames)
        //        {
        //            if (!groupByName.Contains("ALL"))
        //            {
        //                if (isEqpGroup)
        //                { query = query + EQP_STEP_LOG_COL.EQP_GROUP + " = " + $"\"{groupByName}\""; }
        //                else
        //                {
        //                    query = query + EQP_STEP_LOG_COL.STEP_GROUP + " = " + $"\"{groupByName}\"";
        //                }

        //                if (groupByName != selectedGroupByNames.Last())
        //                    query = query + " OR ";
        //            }
        //        }
        //        query = query + ") ";
        //    }
        //    if (isCheckGroup&& selectedGroupNames.Count>0)
        //    {
        //        query = query + " AND ( ";
        //        foreach (string groupName in selectedGroupNames)
        //        {
        //            if (!groupName.Contains("ALL"))
        //            {

        //                    query = query + EQP_STEP_LOG_COL.EQP_NAME + " = " + $"\"{groupName}\"";

        //                if (groupName != selectedGroupNames.Last())
        //                {
        //                    query = query + " OR ";
        //                }
        //            }
        //        }
        //        query = query + ") ";
        //    }
        //    return query;
        //}

        //public string GetFactoryInoutLogQuery(DateTime fromTime, DateTime toTime, List<string> SelectedNodeInoutNames, string tableName,bool isNode)
        //{
        //    string query = "SELECT * FROM " + tableName + " WHERE " + FACTORY_INOUT_COL.INOUT_TIME + " >= #" + fromTime.ToString("yyyy/MM/dd HH:mm:ss")
        //                    + "# AND " + FACTORY_INOUT_COL.INOUT_TIME + " < #" + toTime.ToString("yyyy/MM/dd HH:mm:ss") + "#";

        //    if (SelectedNodeInoutNames.Count > 0)
        //    {
        //        query = query + " AND ( ";
        //        foreach (string nodeName in SelectedNodeInoutNames)
        //        {
        //            if (!nodeName.Contains("ALL"))
        //            {
        //                if (isNode)
        //                {
        //                    query = query + FACTORY_INOUT_COL.NODE_NAME + " = " + $"\"{nodeName}\"";
        //                }
        //                else
        //                {
        //                    query = query + FACTORY_INOUT_COL.PRODUCT_NAME + " = " + $"\"{nodeName}\"";
        //                }

        //                if (nodeName != SelectedNodeInoutNames.Last())
        //                { query = query + " OR "; }
        //            }
        //        }
        //        query = query + ") ";
        //    }

        //    return query;
        //}
        #endregion

        #region TABLE AND COLOUMN TYPE
        public enum USER_RESULT_TABLE_TYPE
        {
            //COMMAND_LOG,
            //MR_LOG,
            //EQP_STEP_LOG,
            //PART_STEP_LOG,
            //FACTORY_INFO,
            //RESULT_TIME,
            //FACTORY_INOUT_LOG
        }

        #region Example enum
        //public enum COMMAND_LOG_COL
        //{
        //    ID, COMMAND_NAME, FACTORY_NAME, ACTIVATED_TIME, COMPLETED_TIME, FROM_STATION, FROM_STATION_TYPE, TO_STATION, TO_STATION_TYPE, PART_ID, PART_NAME,
        //    ASSIGNED_TIME, LOADED_TIME, WAITING_DISTANCE, TRANSFERRING_DISTANCE, VEHICLE_NAME, SCHEDULE_COST, ACQUIRE_ROUTE_ID_LIST, ACTUAL_ROUTE_ID_LIST, LOADING_START_TIME, UNLOADING_START_TIME,
        //    TOTAL_TIME, WAITING_TIME, TRANSFERRING_TIME, QUEUED_TIME, LOADING_TIME, UNLOADING_TIME, CS_NAME, MR_ID, CS_TYPE, VEHICLE_ID
        //}
        //public enum MR_LOG_COL
        //{
        //    ID, FROM_NODE_NAME, TO_NODE_NAME, PART_ID, PART_NAME, ACTIVATED_TIME, COMPLETED_TIME, TOTAL_TIME, COMMAND_COUNT, WAY_POINT_COUNT, ROUTE_ID_LIST
        //}
        //public enum PART_STEP_LOG_COL
        //{
        //    STEP_ID, STEP_NAME, STEP_TYPE, EQP_ID, EQP_NAME, PART_ID, PART_NAME, PRODUCT_ID, PRODUCT_NAME, QUEUE_START_TIME, ASSIGNED_TIME, TRACK_IN_TIME, STEP_START_TIME, STEP_END_TIME, TRACK_OUT_TIME
        //}

        //public enum EQP_STEP_LOG_COL
        //{
        //    STEP_ID, STEP_NAME,STEP_GROUP, STEP_TYPE, EQP_ID, EQP_NAME, EQP_GROUP, INPUT_PART_IDS, INPUT_PART_NAMES, OUTPUT_PART_IDS, OUTPUT_PART_NAMES, ACTIVATED_TIME, ASSIGNED_TIME, STEP_START_TIME, STEP_END_TIME
        //}

        //public enum FACTORY_INFO_COL
        //{
        //    FACTORY_NAME
        //}
        //public enum RESULT_TIME_COL
        //{
        //    NAME, SIM_TIME
        //}
        //public enum RESULT_TIME_LOW
        //{
        //    SIMULATION_START_TIME, SIMULATION_END_TIME
        //}
        //public enum FACTORY_INOUT_COL
        //{
        //    INOUT_TIME, NODE_ID, NODE_NAME, PART_ID, PART_NAME, PRODUCT_ID, PRODUCT_NAME, STATE
        //}
        #endregion

        #endregion
    }
}