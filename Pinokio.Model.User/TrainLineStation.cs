﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Pinokio.Model.Base;

namespace Pinokio.Model.User
{
    [Serializable]
    public class TrainLineStation : LineStation
    {
        private bool _waitAllowed; // waitAllowed 여부
        private bool _bumpAllowed; // BumpAllowed 여부

        private TrainLine _line;

        public new TrainLine Line 
        { 
            get
            {
                if (base.Line != null && _line == null)
                    _line = base.Line as TrainLine;

                return _line;
            }
            set 
            {
                _line = value;
            } 
        }

        [Browsable(true)]
        public bool WaitAllowed
        {
            get { return _waitAllowed; }
            set
            {
                _waitAllowed = value;

                if (_waitAllowed)
                {
                    if (!Line.DicWaitingRailPort.Contains(this))
                        Line.DicWaitingRailPort.Add(this);
                }
                else
                {
                    if (Line.DicWaitingRailPort.Contains(this))
                        Line.DicWaitingRailPort.Remove(this);
                }
            }
        }

        [Browsable(true)]
        public bool BumpAllowed
        {
            get { return _bumpAllowed; }
            set { _bumpAllowed = value; }
        }

        public TrainLineStation()
            :base ()
        {

        }
    }
}