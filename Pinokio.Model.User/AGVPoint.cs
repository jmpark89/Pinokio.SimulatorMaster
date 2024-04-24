using Logger;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Pinokio.Model.Base;

namespace Pinokio.Model.User
{
    public class AGVPoint : TransportPoint
    {
        private string _zcuName = string.Empty;
        private AGVZCU _zcu; //ZCU
        private ZCU_TYPE _zcuType; //ZCU의 STOP, RESET Type
        private TransportLine _inLine; // in Rail
        private List<AGVLine> zcuStopNResetLines;
        private List<SimPort> _lstStopEvent; //OHT의 대기 예약 리스트

        public ACS Acs
        {
            get { return ParentNode as ACS; }
        }

        protected override void SetParentNode(CoupledModel coupledModel)
        {
            base.SetParentNode(coupledModel);
            if(ZcuName != string.Empty && Zcu == null)
                ZcuName = ZcuName;
            
            ZcuType = ZcuType;
        }

        [Browsable(false), StorableAttribute(true)]
        public int ZcuTypeInt
        {
            get { return Convert.ToInt32(ZcuType); }
            set
            {
                ZcuType = (ZCU_TYPE)value;
            }
        }


        [Browsable(true)]
        public ZCU_TYPE ZcuType
        {
            get { return _zcuType; }
            set
            {
                SetZcuType(value);
            }
        }

        protected void SetZcuType(ZCU_TYPE value)
        {
            _zcuType = value;

            if (_zcu != null)
            {
                if (_zcuType == ZCU_TYPE.STOP)
                {
                    if (_zcu.ToPoints.ContainsKey(this.Name))
                        _zcu.ToPoints.Remove(this.Name);

                    if (_zcu.FromPoints.ContainsKey(this.Name) is false)
                        _zcu.FromPoints.Add(this.Name, this);
                }
                else if (_zcuType == ZCU_TYPE.RESET)
                {
                    if (_zcu.FromPoints.ContainsKey(this.Name))
                        _zcu.FromPoints.Remove(this.Name);

                    if (_zcu.ToPoints.ContainsKey(this.Name) is false)
                        _zcu.ToPoints.Add(this.Name, this);
                }
            }
        }


        [Browsable(false)]
        public AGVZCU Zcu
        {
            get { return _zcu; }
            set {
                if (value != null)
                {
                    _zcu = value;
                    _zcuName = _zcu.Name;
                }
            }
        }

        [Browsable(true), StorableAttribute(true)]
        public string ZcuName
        {
            get
            {
                return _zcuName;
            }
            set
            {
                //기존 Zcu에서 Point 제외 
                if (Acs != null && _zcu != null && Acs.Zcus.ContainsKey(_zcu.Name))
                {
                    if (_zcu.ToPoints.ContainsKey(this.Name))
                        _zcu.ToPoints.Remove(this.Name);

                    if (_zcu.FromPoints.ContainsKey(this.Name))
                        _zcu.FromPoints.Remove(this.Name);
                }

                _zcuName = value;

                //추가 또는 찾아서 설정.
                if (Acs != null)
                {
                    if (!Acs.Zcus.ContainsKey(value))
                    {
                        _zcu = Acs.AddZcu(value);
                        if (_zcuType == ZCU_TYPE.STOP)
                        {
                            if (_zcu.ToPoints.ContainsKey(this.Name))
                                _zcu.ToPoints.Remove(this.Name);

                            _zcu.FromPoints.Add(this.Name, this);
                        }
                        else if (_zcuType == ZCU_TYPE.RESET)
                        {
                            if (_zcu.FromPoints.ContainsKey(this.Name))
                                _zcu.FromPoints.Remove(this.Name);

                            _zcu.ToPoints.Add(this.Name, this);
                        }
                    }
                    else
                    {
                        _zcu = Acs.Zcus[value];

                        if (_zcuType == ZCU_TYPE.STOP)
                        {
                            if (_zcu.ToPoints.ContainsKey(this.Name))
                                _zcu.ToPoints.Remove(this.Name);

                            _zcu.FromPoints.Add(this.Name, this);
                        }
                        else if (_zcuType == ZCU_TYPE.RESET)
                        {
                            if (_zcu.FromPoints.ContainsKey(this.Name))
                                _zcu.FromPoints.Remove(this.Name);

                            _zcu.ToPoints.Add(this.Name, this);
                        }
                    }
                }
            }
        }
        [Browsable(true)]
        public List<SimPort> StopPorts
        { get { return _lstStopEvent; } }

        public AGVPoint()
            :base()
        {
            Initialize();
        }

        private void Initialize()
        {
            _lstStopEvent = new List<SimPort>();
            zcuStopNResetLines = new List<AGVLine>();
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            base.InitializeNode(evtCal);

            if(Zcu != null)
                Zcu.SetLines();
        }

        /// <summary>
        /// 지나갈 수 있는지 확인하는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="agv"></param>
        /// <returns></returns>
        public bool CheckEnter(Time simTime, AGV agv)
        {
            if ((agv.Route.Count > 1 && zcuStopNResetLines.Contains(agv.Route[1]))
            || (Zcu != null && ZcuType == ZCU_TYPE.STOP && Zcu.IsAvailableToEnter(agv))
            || Zcu == null
            || ZcuType == ZCU_TYPE.NON
            || (Zcu != null && ZcuType == ZCU_TYPE.RESET))
            {
                PassOHT(simTime, agv);
                return true;
            }
            else
            {
                WaitOHT(simTime, agv);
                return false;
            }
        }

        /// <summary>
        /// OHT 통과 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="simLogs"></param>
        /// <param name="port"></param>
        private void PassOHT(Time simTime, AGV agv)
        {
            try
            {
                TransportLine inLine = agv.Route[0];

                if (inLine.EndPoint.Name != Name)
                    return;

                TransportLine toLine = null;
                if (agv.Route.Count > 1)
                    toLine = agv.Route[1];

                foreach (SimPort portTemp in _lstStopEvent.ToList())
                {
                    if (portTemp.Object.Name == agv.Name)
                        _lstStopEvent.Remove(portTemp);
                }

                _inLine = inLine;

                if (Zcu != null && ZcuType == ZCU_TYPE.STOP && !zcuStopNResetLines.Contains(toLine))
                {
                    if (!Zcu.Agvs.Contains(agv))
                        Zcu.Agvs.Add(agv);

                    if (Zcu.StopAGVs.Contains(agv))
                        Zcu.StopAGVs.Remove(agv);

                    Zcu.AddAGV(agv);
                }
                else if (Zcu != null && ZcuType == ZCU_TYPE.RESET)
                {
                    Zcu.Agvs.Remove(agv);
                    Zcu.RemoveAGV(agv);
                }
                else if (zcuStopNResetLines.Contains(toLine))
                    Zcu.RemoveReservation(agv);

                SimPort newPort = new SimPort(EXT_PORT.OHT_OUT, this, agv);
                _inLine.ExternalFunction(simTime, newPort);

                if (Zcu != null && ZcuType == ZCU_TYPE.RESET)
                {
                    Zcu.ProcessReservation(simTime, this);
                }
                else if (zcuStopNResetLines.Contains(toLine))
                {
                    Zcu.ProcessReservation(simTime, this);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        private void WaitOHT(Time simTime, AGV agv)
        {
            SimPort newPort = new SimPort(EXT_PORT.MOVE, this, agv);
            //ZCU가 null이 아니고 ZCU의 StopOHTs가 oht를 포함하지 않으면
            if (Zcu != null && !Zcu.StopAGVs.Contains(agv))
            {
                Zcu.StopAGVs.Add(agv);

                bool isPort = false;
                foreach (SimPort portTemp in _lstStopEvent.ToList())
                {
                    if (portTemp.Object.Name == agv.Name)
                        isPort = true;
                }

                if (isPort == false)
                    _lstStopEvent.Add(newPort);

                if (agv.FirstReservationZCU == null) //CurZcu가 없다는 것은 예약이 아직 안되어있다는 뜻.
                    Zcu.AddReservation(simTime, agv, this);
            }
        }
    }
}
