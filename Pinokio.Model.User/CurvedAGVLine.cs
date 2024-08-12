using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Logger;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Pinokio.Object;
using Simulation.Engine;

namespace Pinokio.Model.User
{
    [Serializable]
    public class CurvedAGVLine : AGVLine, ICurvedLine
    {
        private double _radius;
        private double _startDegree;
        private double _arcDegree;
        private PVector2 _origin;

        [StorableAttribute(true)]
        public double Radius
        {
            get { return _radius; }
            set
            {
                _radius = value;
                _origin = PVector2.GetOrigin(_radius, _startDegree, _arcDegree);
            }
        }

        [StorableAttribute(true)]
        public double StartDegree
        {
            get { return _startDegree; }
            set
            {
                _startDegree = value;
                _origin = PVector2.GetOrigin(_radius, _startDegree, _arcDegree);
            }
        }

        public PVector2 Origin
        {
            get { return _origin; }
        }

        [StorableAttribute(true)]
        public double ArcDegree
        {
            get { return _arcDegree; }
            set
            {
                _arcDegree = value;
            }
        }
        //---------------------충돌 방지 위한 변수-----------------------

        public CurvedAGVLine() : base()
        {
            Initialize();
        }


        public CurvedAGVLine(uint id, string name) : base(id, name)
        {
            Initialize();
        }

        public CurvedAGVLine(string name, TransportPoint start, TransportPoint end, int width, int height)
            : base(name, start, end, width, height)
        {
            Initialize();
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsCurve = true;
        }

        public override PVector3 GetObjectPosition(SimObj entity, Time simTime)
        {
            double distance = GetDistanceAtTime(entity, simTime);
            double angle;
            if (_arcDegree > 0)
                angle = distance / (2 * Math.PI * _radius) * 360 - 90;
            else
                angle = -distance / (2 * Math.PI * _radius) * 360 + 90;

            PVector3 pos = StartPoint.PosVec3 + new PVector3(_origin.X, _origin.Y, 0) + PVector3.DegreeToDirection(_startDegree + angle) * _radius;

            return pos;
        }

        public override double GetObjectDegree(SimObj entity, Time simTime)
        {
            double angle = GetObjectRadian(entity, simTime) * 180 / Math.PI;
            return angle;
        }

        public override double GetObjectRadian(SimObj entity, Time simTime)
        {
            double distance = GetDistanceAtTime(entity, simTime);
            double radian = GetObjectRadianAtDistance(distance);
            return radian;
        }

        public double GetObjectRadianAtDistance(double distance)
        {
            double radian;
            if (_arcDegree > 0)
                radian = (_startDegree + distance / (2 * Math.PI * _radius) * 360) / 180 * Math.PI;
            else
                radian = (_startDegree - (distance / (2 * Math.PI * _radius) * 360)) / 180 * Math.PI;

            return radian;
        }

        public double GetLengthByRadiusNArcDegree(double radius, double arcDegree)
        {
            return radius * 2 * Math.PI * arcDegree / 360;
        }

        public PVector3 GetPositionByDistance(PVector3 startPoint, double distance)
        {
            double angle;
            if (_arcDegree > 0)
                angle = distance / (2 * Math.PI * _radius) * 360 - 90;
            else
                angle = -distance / (2 * Math.PI * _radius) * 360 + 90;

            PVector3 pos = startPoint + new PVector3(_origin.X, _origin.Y, 0) + PVector3.DegreeToDirection(_startDegree + angle) * _radius;

            return pos;
        }
    }
}
