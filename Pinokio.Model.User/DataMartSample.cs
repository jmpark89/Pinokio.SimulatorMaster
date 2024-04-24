using Pinokio.Database;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pinokio.Model.Base.SimResultDBManager;

namespace Pinokio.Model.User
{
    public class DataMartSample
    {
        public static Dictionary<uint, Command_ViewModel> dicCommand = new Dictionary<uint, Command_ViewModel>();

        public static Dictionary<uint, MR_ViewModel> dicMR = new Dictionary<uint, MR_ViewModel>();

        public static void UploadCompletedCommandTrendLog(Command_ViewModel command)
        {
            List<object> values = new List<object>();

            values.Add(command.Id);
            values.Add(string.Empty);
            values.Add(command.ActivatedDateTime);
            values.Add(command.CompletedDateTime);
            values.Add(command.TargetCSNetwork);
            values.Add(command.SubCSName);
            values.Add(command.MRId);
            values.Add(command.FromNodeName);
            values.Add(command.FromNodeType);
            values.Add(command.ToNodeName);
            values.Add(command.ToNodeType);
            values.Add(command.PartId);
            values.Add(new DateTime());
            values.Add(command.LoadedDateTime);
            values.Add(0);
            values.Add(command.TransferringDistance);
            values.Add("NO_NAME");
            values.Add(0);
            values.Add(command.LoadingStartDateTime);
            values.Add(command.UnloadingStartDateTime);
            values.Add(command.TotalTime);
            values.Add(0);
            values.Add(0);
            values.Add(0);
            values.Add(command.LoadingTime);
            values.Add(command.UnloadingTime);

            AccessDB.Instance.Insert(RESULT_TABLE_TYPE.COMMAND_LOG.ToString(), values);
        }

        public static void UploadCompletedMRTrendLog(MR_ViewModel mr)
        {
            List<object> values = new List<object>();

            values.Add(mr.Id);
            values.Add(mr.StartNode.Name);
            values.Add(mr.EndNode.Name);
            values.Add(mr.ActivatedDateTime);
            values.Add(mr.CompletedDateTime);
            values.Add(mr.TotalTime);
            values.Add(mr.CommandCount);
            values.Add(mr.WayPointCount);

            AccessDB.Instance.Insert(RESULT_TABLE_TYPE.MR_LOG.ToString(), values);
        }
    }

    public class Command_ViewModel
    {
        #region Member Variables
        public uint Id { get; set; }
        public string NullName { get; set; }
        public DateTime ActivatedDateTime { get; set; }
        public DateTime CompletedDateTime { get; set; }
        public string TargetCSNetwork { get; set; }
        public string SubCSName { get; set; }
        public uint MRId { get; set; }
        public string FromNodeName { get; set; }
        public string FromNodeType { get; set; }
        public string ToNodeName { get; set; }
        public string ToNodeType { get; set; }
        public uint PartId { get; set; }
        public DateTime AssignedDateTime { get; set; }
        public DateTime LoadedDateTime { get; set; }
        public double WaitingDistance { get; set; }
        public double TransferringDistance { get; set; }
        public string NoName { get; set; }
        public double ScheduleCost { get; set; }
        public DateTime LoadingStartDateTime { get; set; }
        public DateTime UnloadingStartDateTime { get; set; }
        public double TotalTime { get; set; }
        public double WaitingTime { get; set; }
        public double TransferringTime { get; set; }
        public double QueuedTime { get; set; }
        public double LoadingTime { get; set; }
        public double UnloadingTime { get; set; } 
        #endregion

        public Command_ViewModel()
        {


        }
    }

    public class MR_ViewModel
    {
        public uint Id { get; set; }
        public TXNode StartNode { get; set; }
        public TXNode EndNode { get; set; }
        public DateTime ActivatedDateTime { get; set; }
        public DateTime CompletedDateTime { get; set; }
        public double TotalTime { get; set; }
        public uint CommandCount { get; set; }
        public uint WayPointCount { get; set; }
        public MR_ViewModel()
        {
        }
    }
}
