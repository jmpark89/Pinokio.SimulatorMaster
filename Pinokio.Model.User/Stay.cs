namespace Pinokio.Model.User
{
    using Logger;
    using Pinokio.Database;
    using Pinokio.Geometry;
    using Pinokio.Model.Base;
    using global::Simulation.Engine;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Step ���� �����ð� �ӹ��ٰ� ������ ������ Node
    /// </summary>
    public class Stay : TXNode
    {
        [StorableAttribute(false)]
        private VEHICLE_STATE _state = VEHICLE_STATE.IDLE;
        [StorableAttribute(false)]
        protected Time _loadingTime = 1;
        [StorableAttribute(false)]
        protected Time _unloadingTime = 1;
        [StorableAttribute(false)]
        protected Time _stayTime = 5;
        protected TXNode fromNode = null;
        protected TXNode toNode = null;
        [StorableAttribute(false)]
        Random random = new Random();
        /// <summary>
        /// Part�� Loading�ϴµ� �ɸ��� �ð�
        /// </summary>
        [StorableAttribute(true)]
        public double LoadingTime { get => _loadingTime.TotalSeconds; set => _loadingTime = value; }

        /// <summary>
        /// Part�� Unloading�ϴµ� �ɸ��� �ð�
        /// </summary>
        [StorableAttribute(true)]
        public double UnloadingTime { get => _unloadingTime.TotalSeconds; set => _unloadingTime = value; }

        /// <summary>
        /// Part�� Stay�ϴ� �ð�
        /// </summary>
        [StorableAttribute(true)]
        public double StayTime { get => _stayTime.TotalSeconds; set => _stayTime = value; }

        /// <summary>
        /// ����(VEHICLE_STATE�� ����ؼ� ǥ��, �ٸ� ���� ǥ���� ���ϸ� 5�� ���� ū ���ڷ� State�� ǥ��) 
        /// </summary>
        [StorableAttribute(true), Browsable(false)]
        public VEHICLE_STATE State { get => _state; set => _state = value; }

        public Stay()
        {
        }

        public Stay(uint id, string name) :
                base(id, name)
        {
            // Write your code here.
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);
            try
            {
                EnteredObjects.Clear();
                RequestedObjects.Clear();
                ArrivedPorts.Clear();
            }
            catch (System.Exception ex)
            {
                // Handle any other exception type here.
                ErrorLogger.SaveLog(ex);
            }
        }

        /// <summary>
        /// Loading ������ �� ����Ǵ� �Լ�. Vehicle�� Loading State�� �����ϰ� ParentNode�� VSubCS�� ��� SetLoadingCommand�� �����ϰ� Vehicle�� LoadTime �ڿ� EndLoading ���� �̺�Ʈ �Լ��� ȣ�� �� EndLoading
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="port"></param>
        protected void StartLoading(Time simTime, SimPort port)
        {
            RequestedObjects.Add(port.Object);

            SimPort newPort = new SimPort(INT_PORT.COMPLETE_LOAD, port.Object, port.FromNode);
            EvtCalendar.AddEvent(simTime + this.LoadingTime, this, newPort);
        }

        /// <summary>
        /// Part�� Vehicle���� unload�ϱ� ���ؼ� UnloadTime ���Ŀ� EndUnloading �Լ��� ȣ���ϴ� �Լ� �� EndUnloading
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="port"></param>
        protected void StartUnloading(Time simTime, SimPort port)
        {
            port = new SimPort(INT_PORT.COMPLETE_UNLOAD, port.Object, port.FromNode);
            EvtCalendar.AddEvent(simTime + this.UnloadingTime, this, port);
        }

        /// <summary>
        /// Part�� �ް�, ���¸� MOVE_TO_UNLOAD�� �ٲٰ� Command�� Transferring ó�� �ϰ� �������� ��θ� �Ҵ��ؼ� �̵���Ű�� �Լ�
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="port"></param>
        protected void EndLoading(Time simTime, SimPort port)
        {
            SimPort requestPort = new SimPort(EXT_PORT.REQUEST_ENTITY, port.Object, this);
            port.FromNode.ExternalFunction(simTime, requestPort);
        }

        /// <summary>
        /// Part�� End TXNode�� �ְ�, ���¸� Idle�� �ٲٰ� Command�� �Ϸ�ó�� �ϰ� Idle�� ������ �������� ���ϰ� �̵���Ű�� �Լ�
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="port"></param>
        protected void EndUnloading(Time simTime, SimPort port)
        {
            SimPort arrivePort = new SimPort(EXT_PORT.ENTER, port.Object, this);
            port.FromNode.ExternalFunction(simTime, arrivePort);

            if (ArrivedPorts.Count > 0)
            {
                SimPort newPort = new SimPort(INT_PORT.ARRIVE_TO_LOAD, this);
                InternalFunction(simTime, newPort);
            }
        }

        public override void RequestEntity(Time simTime, SimPort port)
        {
            StartUnloading(simTime, port);
        }

        public override void ExternalFunction(Time simTime, SimPort port)
        {
            base.ExternalFunction(simTime, port);
            switch (port.PortType)
            {
                case EXT_PORT.LOAD:
                    {
                        StartLoading(simTime, port);
                        break;
                    }
                case EXT_PORT.UNLOAD:
                    {
                        SimPort newPort = new SimPort(EXT_PORT.ARRIVE, port.Object, this);
                        port.ToNode.ExternalFunction(simTime, newPort);
                        break;
                    }
                default:
                    break;
            }
        }

        public override void InternalFunction(Time simTime, SimPort port)
        {
            base.InternalFunction(simTime, port);
            switch (port.PortType)
            {
                #region Simulation �� INT_PORT
                case INT_PORT.ARRIVE_TO_LOAD:
                    {
                        ArriveToLoad(simTime, port);
                        break;
                    }
                case INT_PORT.ARRIVE_TO_UNLOAD:
                    {
                        ArriveToUnload(simTime, port);
                        break;
                    }
                case INT_PORT.COMPLETE_LOAD:
                    {
                        EndLoading(simTime, port);
                        break;
                    }
                case INT_PORT.COMPLETE_UNLOAD:
                    {
                        EndUnloading(simTime, port);
                        break;
                    }
                #endregion
                default:
                    break;
            }
        }

        protected override void Enter(Time simTime, SimPort port)
        {
            SimPort newPort = new SimPort(INT_PORT.ARRIVE_TO_UNLOAD, port.Object);

            newPort.ToNode = OutLinkNodes[random.Next(0, OutLinkCount)];
            EvtCalendar.AddEvent(simTime + _stayTime, this, newPort);
        }
 
        protected void ArriveToLoad(Time simTime, SimPort port)
        {
            if (ArrivedPorts.Count > 0)
            {
                SimPort partPort = ArrivedPorts.First();
                ArrivedPorts.Remove(partPort);
                SimPort newPort = new SimPort(EXT_PORT.LOAD, partPort.Object, partPort.FromNode);
                ExternalFunction(simTime, newPort);
            }
        }

        protected void ArriveToUnload(Time simTime, SimPort port)
        {
            SimPort newPort = new SimPort(EXT_PORT.UNLOAD, port.Object, this);

            newPort.ToNode = port.ToNode;

            ExternalFunction(simTime, newPort);
        }

        public override bool IsEnter(SimPort port)
        {
            if (Capa == -1 || Capa > EnteredObjects.Count)
            {
                Time timeNow = SimEngine.Instance.TimeNow;
                SimPort newPort = new SimPort(INT_PORT.ARRIVE_TO_LOAD, port.Object);
                InternalFunction(timeNow, newPort);

                return false;
            }
            else
                return false;
        }

        public override void UpdateAnimationPos()
        {

            try
            {
                double partHeight = 0;
                foreach (SimObj obj in EnteredObjects)
                {
                    partHeight += obj.Size.Z;
                    obj.PosVec3 = PosVec3 + new PVector3(0, 0, partHeight);
                }
            }
            catch (System.Exception ex)
            {
                // Handle any other exception type here.
                ErrorLogger.SaveLog(ex);
            }
        }
    }
}