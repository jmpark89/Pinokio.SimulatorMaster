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
    /// Step 없이 일정시간 머물다가 나가는 목적의 Node
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
        /// Part를 Loading하는데 걸리는 시간
        /// </summary>
        [StorableAttribute(true)]
        public double LoadingTime { get => _loadingTime.TotalSeconds; set => _loadingTime = value; }

        /// <summary>
        /// Part를 Unloading하는데 걸리는 시간
        /// </summary>
        [StorableAttribute(true)]
        public double UnloadingTime { get => _unloadingTime.TotalSeconds; set => _unloadingTime = value; }

        /// <summary>
        /// Part를 Stay하는 시간
        /// </summary>
        [StorableAttribute(true)]
        public double StayTime { get => _stayTime.TotalSeconds; set => _stayTime = value; }

        /// <summary>
        /// 상태(VEHICLE_STATE를 사용해서 표현, 다른 상태 표현을 원하면 5번 보다 큰 숫자로 State를 표현) 
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
        /// Loading 시작할 때 실행되는 함수. Vehicle을 Loading State로 변경하고 ParentNode가 VSubCS일 경우 SetLoadingCommand를 실행하고 Vehicle의 LoadTime 뒤에 EndLoading 관련 이벤트 함수를 호출 ⇒ EndLoading
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
        /// Part를 Vehicle에서 unload하기 위해서 UnloadTime 이후에 EndUnloading 함수를 호출하는 함수 ⇒ EndUnloading
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="port"></param>
        protected void StartUnloading(Time simTime, SimPort port)
        {
            port = new SimPort(INT_PORT.COMPLETE_UNLOAD, port.Object, port.FromNode);
            EvtCalendar.AddEvent(simTime + this.UnloadingTime, this, port);
        }

        /// <summary>
        /// Part를 받고, 상태를 MOVE_TO_UNLOAD로 바꾸고 Command를 Transferring 처리 하고 목적지와 경로를 할당해서 이동시키는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="port"></param>
        protected void EndLoading(Time simTime, SimPort port)
        {
            SimPort requestPort = new SimPort(EXT_PORT.REQUEST_ENTITY, port.Object, this);
            port.FromNode.ExternalFunction(simTime, requestPort);
        }

        /// <summary>
        /// Part를 End TXNode에 주고, 상태를 Idle로 바꾸고 Command를 완료처리 하고 Idle한 상태의 목적지를 정하고 이동시키는 함수
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
                #region Simulation 모델 INT_PORT
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