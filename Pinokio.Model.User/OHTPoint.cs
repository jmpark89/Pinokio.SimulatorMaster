using Logger;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Pinokio.Model.Base;

namespace Pinokio.Model.User
{
    [Serializable]
    public class OHTPoint : TransportPoint
    {
        private string _zcuName = string.Empty;
        private OHTZCU _zcu; //ZCU
        private ZCU_TYPE _zcuType; //ZCU의 STOP, RESET Type
        private TransportLine _inLine; // in Rail
        private List<OHTLine> zcuStopNResetLines;
        private List<SimPort> _lstStopEvent; //OHT의 대기 예약 리스트

        public OCS Ocs
        {
            get { return ParentNode as OCS; }
        }
        private bool _zcuDone;
        public bool ZCUDONE
        {
            get { return _zcuDone; }
            set { _zcuDone = value; }
        }
        protected override void SetParentNode(CoupledModel coupledModel)
        {
            base.SetParentNode(coupledModel);
            if (ZcuName != string.Empty && Zcu == null)
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
        public OHTZCU Zcu
        {
            get { return _zcu; }
            set
            {
                if (value != null)
                {
                    _zcu = value;
                    _zcuName = _zcu.Name;
                }
                else
                {
                    if (_zcu.ToPoints.ContainsKey(this.Name))
                        _zcu.ToPoints.Remove(this.Name);
                    if (_zcu.FromPoints.ContainsKey(this.Name))
                        _zcu.FromPoints.Remove(this.Name);

                    _zcuName = string.Empty;
                    _zcuType = ZCU_TYPE.NON;
                    if (this.Ocs.Zcus[_zcu.Name].FromPoints.Count == 0 && this.Ocs.Zcus[_zcu.Name].ToPoints.Count == 0)
                        this.Ocs.Zcus.Remove(_zcu.Name);

                    _zcu = null;
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
                if (value == "") //초기화 오류 수정
                    return;
                //기존 Zcu에서 Point 제외 
                if (Ocs != null && _zcu != null && Ocs.Zcus.ContainsKey(_zcu.Name))
                {
                    if (_zcu.ToPoints.ContainsKey(this.Name))
                        _zcu.ToPoints.Remove(this.Name);

                    if (_zcu.FromPoints.ContainsKey(this.Name))
                        _zcu.FromPoints.Remove(this.Name);
                }

                _zcuName = value;

                //추가 또는 찾아서 설정.
                if (Ocs != null)
                {
                    if (!Ocs.Zcus.ContainsKey(value))
                    {
                        _zcu = Ocs.AddZcu(value);
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
                        _zcu = Ocs.Zcus[value];

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

        public OHTPoint()
            : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            _lstStopEvent = new List<SimPort>();
            zcuStopNResetLines = new List<OHTLine>();
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            _lstStopEvent = new List<SimPort>();
            zcuStopNResetLines = new List<OHTLine>();
            base.InitializeNode(evtCal);

                if (Zcu != null && Zcu.FromPoints.First().Value == this)
                    Zcu.SetLines();
        }
        /// <summary>
        /// 지나갈 수 있는지 확인하는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="oht"></param>
        /// <returns></returns>
        public bool CheckEnter(Time simTime, OHT oht)
        {
            if ((oht.Route.Count > 1 && zcuStopNResetLines.Contains(oht.Route[1]))
            || (Zcu != null && ZcuType == ZCU_TYPE.STOP && Zcu.IsAvailableToEnter(oht))
            || Zcu == null
            || ZcuType == ZCU_TYPE.NON
            || (Zcu != null && ZcuType == ZCU_TYPE.RESET))
            {
                PassOHT(simTime, oht);
                return true;
            }
            else
            {
                WaitOHT(simTime, oht);
                return false;
            }
        }

        /// <summary>
        /// OHT 통과 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="simLogs"></param>
        /// <param name="port"></param>
        private void PassOHT(Time simTime, OHT oht)
        {
            try
            {
                TransportLine inLine = oht.Route[0];

                if (inLine.EndPoint.Name != Name)
                    return;

                TransportLine toLine = null;
                if (oht.Route.Count > 1)
                    toLine = oht.Route[1];

                foreach (SimPort portTemp in _lstStopEvent.ToList())
                {
                    if (portTemp.Object.Name == oht.Name)
                        _lstStopEvent.Remove(portTemp);
                }

                _inLine = inLine;

                if (Zcu != null && ZcuType == ZCU_TYPE.STOP && !zcuStopNResetLines.Contains(toLine))
                {
                    if (!Zcu.Ohts.Contains(oht))
                        Zcu.Ohts.Add(oht);

                    if (Zcu.StopOHTs.Contains(oht))
                        Zcu.StopOHTs.Remove(oht);

                    Zcu.AddOHT(oht);
                }
                else if (Zcu != null && ZcuType == ZCU_TYPE.RESET)
                {
                    Zcu.Ohts.Remove(oht);
                    Zcu.RemoveOHT(oht);
                }
                else if (zcuStopNResetLines.Contains(toLine))
                    Zcu.RemoveReservation(oht);

                SimPort newPort = new SimPort(EXT_PORT.OHT_OUT, this, oht);
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

        private void WaitOHT(Time simTime, OHT oht)
        {
            SimPort newPort = new SimPort(EXT_PORT.MOVE, this, oht);
            //ZCU가 null이 아니고 ZCU의 StopOHTs가 oht를 포함하지 않으면
            if (Zcu != null && !Zcu.StopOHTs.Contains(oht))
            {
                Zcu.StopOHTs.Add(oht);

                bool isPort = false;
                foreach (SimPort portTemp in _lstStopEvent.ToList())
                {
                    if (portTemp.Object.Name == oht.Name)
                        isPort = true;
                }

                if (isPort == false)
                    _lstStopEvent.Add(newPort);

                if (oht.FirstReservationZCU == null) //CurZcu가 없다는 것은 예약이 아직 안되어있다는 뜻.
                    Zcu.AddReservation(simTime, oht, this);
            }
        }

        public override List<SimNodeIntegrityLog> CheckIntegrity()
        {
            List<SimNodeIntegrityLog> logs = new List<SimNodeIntegrityLog>();

            if((OutLines.Count > 1 || InLines.Count > 1) && _zcu == null)
                logs.Add(new SimNodeIntegrityLog
                {
                    NodeType = this.GetType().Name,
                    ID = this.ID,
                    Name = this.Name,
                    ParentNodeName = ParentNode != null ? ParentNode.Name : string.Empty,
                    FloorName = Floor != null? Floor.Name : string.Empty,
                    ErrorType = IntegrityErrors.Type.NO_ZCU.ToString(),
                    LogDetail = IntegrityErrors.Details[IntegrityErrors.Type.NO_ZCU.ToString()],
                });
            else if(OutLines.Count > 1 && _zcu != null && _zcuType != ZCU_TYPE.STOP)
                logs.Add(new SimNodeIntegrityLog
                {
                    NodeType = this.GetType().Name,
                    ID = this.ID,
                    Name = this.Name,
                    ParentNodeName = ParentNode != null ? ParentNode.Name : string.Empty,
                    FloorName = Floor != null ? Floor.Name : string.Empty,
                    ErrorType = IntegrityErrors.Type.INVALID_ZCU_TYPE.ToString(),
                    LogDetail = IntegrityErrors.Details[IntegrityErrors.Type.INVALID_ZCU_TYPE.ToString()],
                });
            else if (InLines.Count > 1 && _zcu != null && _zcuType != ZCU_TYPE.RESET)
                logs.Add(new SimNodeIntegrityLog
                {
                    NodeType = this.GetType().Name,
                    ID = this.ID,
                    Name = this.Name,
                    ParentNodeName = ParentNode != null ? ParentNode.Name : string.Empty,
                    FloorName = Floor != null ? Floor.Name : string.Empty,
                    ErrorType = IntegrityErrors.Type.INVALID_ZCU_TYPE.ToString(),
                    LogDetail = IntegrityErrors.Details[IntegrityErrors.Type.INVALID_ZCU_TYPE.ToString()],
                });

            return logs;
        }
    }
}
