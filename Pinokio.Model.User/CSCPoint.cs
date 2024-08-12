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
    public class CSCPoint : TransportPoint
    {
        private string _zcuName = string.Empty;
        private CSCZCU _zcu; //ZCU
        private ZCU_TYPE _zcuType; //ZCU의 STOP, RESET Type
        private TransportLine _inLine; // in Rail
        private List<CSCLine> zcuStopNResetLines;
        private List<SimPort> _lstStopEvent; //CSC의 대기 예약 리스트

        public CSCCS CSCcs
        {
            get { return ParentNode as CSCCS; }
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
        public CSCZCU Zcu
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
                    if (this.CSCcs.Zcus[_zcu.Name].FromPoints.Count == 0 && this.CSCcs.Zcus[_zcu.Name].ToPoints.Count == 0)
                        this.CSCcs.Zcus.Remove(_zcu.Name);

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
                if (CSCcs != null && _zcu != null && CSCcs.Zcus.ContainsKey(_zcu.Name))
                {
                    if (_zcu.ToPoints.ContainsKey(this.Name))
                        _zcu.ToPoints.Remove(this.Name);

                    if (_zcu.FromPoints.ContainsKey(this.Name))
                        _zcu.FromPoints.Remove(this.Name);
                }

                _zcuName = value;

                //추가 또는 찾아서 설정.
                if (CSCcs != null)
                {
                    if (!CSCcs.Zcus.ContainsKey(value))
                    {
                        _zcu = CSCcs.AddZcu(value);
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
                        _zcu = CSCcs.Zcus[value];

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

        public CSCPoint()
            : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            _lstStopEvent = new List<SimPort>();
            zcuStopNResetLines = new List<CSCLine>();
        }

        public override void InitializeNode(EventCalendar evtCal)
        {
            _lstStopEvent = new List<SimPort>();
            zcuStopNResetLines = new List<CSCLine>();
            base.InitializeNode(evtCal);

                if (Zcu != null && Zcu.FromPoints.First().Value == this)
                    Zcu.SetLines();
        }
        /// <summary>
        /// 지나갈 수 있는지 확인하는 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="csc"></param>
        /// <returns></returns>
        public bool CheckEnter(Time simTime, CSC csc)
        {
            if ((csc.Route.Count > 1 && zcuStopNResetLines.Contains(csc.Route[1]))
            || (Zcu != null && ZcuType == ZCU_TYPE.STOP && Zcu.IsAvailableToEnter(csc))
            || Zcu == null
            || ZcuType == ZCU_TYPE.NON
            || (Zcu != null && ZcuType == ZCU_TYPE.RESET))
            {
                PassCSC(simTime, csc);
                return true;
            }
            else
            {
                WaitCSC(simTime, csc);
                return false;
            }
        }

        /// <summary>
        /// CSC 통과 함수
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="simLogs"></param>
        /// <param name="port"></param>
        private void PassCSC(Time simTime, CSC csc)
        {
            try
            {
                TransportLine inLine = csc.Route[0];

                if (inLine.EndPoint.Name != Name)
                    return;

                TransportLine toLine = null;
                if (csc.Route.Count > 1)
                    toLine = csc.Route[1];

                foreach (SimPort portTemp in _lstStopEvent.ToList())
                {
                    if (portTemp.Object.Name == csc.Name)
                        _lstStopEvent.Remove(portTemp);
                }

                _inLine = inLine;

                if (Zcu != null && ZcuType == ZCU_TYPE.STOP && !zcuStopNResetLines.Contains(toLine))
                {
                    if (!Zcu.Cscs.Contains(csc))
                        Zcu.Cscs.Add(csc);

                    if (Zcu.StopCSCs.Contains(csc))
                        Zcu.StopCSCs.Remove(csc);

                    Zcu.AddCSC(csc);
                }
                else if (Zcu != null && ZcuType == ZCU_TYPE.RESET)
                {
                    Zcu.Cscs.Remove(csc);
                    Zcu.RemoveCSC(csc);
                }
                else if (zcuStopNResetLines.Contains(toLine))
                    Zcu.RemoveReservation(csc);

                SimPort newPort = new SimPort(EXT_PORT.CSC_OUT, this, csc);
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

        private void WaitCSC(Time simTime, CSC csc)
        {
            SimPort newPort = new SimPort(EXT_PORT.MOVE, this, csc);
            //ZCU가 null이 아니고 ZCU의 StopCSCs가 csc를 포함하지 않으면
            if (Zcu != null && !Zcu.StopCSCs.Contains(csc))
            {
                Zcu.StopCSCs.Add(csc);

                bool isPort = false;
                foreach (SimPort portTemp in _lstStopEvent.ToList())
                {
                    if (portTemp.Object.Name == csc.Name)
                        isPort = true;
                }

                if (isPort == false)
                    _lstStopEvent.Add(newPort);

                if (csc.FirstReservationZCU == null) //CurZcu가 없다는 것은 예약이 아직 안되어있다는 뜻.
                    Zcu.AddReservation(simTime, csc, this);
            }
        }
    }
}
