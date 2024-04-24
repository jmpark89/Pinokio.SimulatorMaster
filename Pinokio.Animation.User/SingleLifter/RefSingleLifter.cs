using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using DevExpress.XtraBars.Navigation;
using Logger;
using Pinokio.Geometry;
using Pinokio.Model.Base;
using Pinokio.Model.User;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Animation.User
{
    public class RefSingleLifter : NodeReference
    {
        #region [Public Variable]
        [StorableAttribute(false)]
        public static bool IsInserted = true;
        private static string _blockName = typeof(RefSingleLifter).Name;
        protected static string InnerBlockName = typeof(RefSingleLifter).Name + "_INNER";

        [Browsable(false), StorableAttribute(false)]
        public new static string[] UsableBaseSimNodeType = { typeof(Lifter).Name };

        [Browsable(false), StorableAttribute(false)]
        private static List<string> _usableSimNodeTypes;
        [Browsable(false), StorableAttribute(false)]
        public new static List<string> UsableSimNodeTypes
        {
            get
            {
                if (_usableSimNodeTypes == null)
                    _usableSimNodeTypes = BaseUtill.GetUsableSimNodeTypes(UsableBaseSimNodeType);

                return _usableSimNodeTypes;
            }
        }
        private RefTransportPoint _startPoint;
        private RefTransportPoint _endPoint;
        [StorableAttribute(true)]
        public uint StartStationID
        { get; set; }
        [StorableAttribute(true)]
        public uint EndStationID
        { get; set; }

        public RefTransportPoint StartStation
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
                StartStationID = _startPoint.ID;
            }
        }
        public RefTransportPoint EndStation
        {
            get => _endPoint;
            set
            {
                _endPoint = value;
                EndStationID = _endPoint.ID;
            }
        }
        #endregion
        #region [Private Variable]
        [StorableAttribute(false)]
        private double _angle = 0;
        [StorableAttribute(false)]
        private Point3D _start;
        private int _totalHeight = 10000;
        private int _stationHeight = 1000;
        private double _direction = 180;

        //몇층까지 커버 할 수 있니 변수
        private int floor4Lifter = 0;


        private Block _createdLineBlock = null;
        private BlockReference _createdLineReference = null;
        [CategoryAttribute("노드 정보"), DescriptionAttribute("Rail의 높이")]
        public Block CreatedLineBlock { get => _createdLineBlock; set => _createdLineBlock = value; }
        public BlockReference CreatedLineReference { get => _createdLineReference; set => _createdLineReference = value; }
        #endregion
        public RefSingleLifter(string blockName) : base(blockName)
        {
            
        }
        

        #region [Mouse Event Methods]
        public override bool Insert_MouseUp(PinokioBaseModel model, Point3D moveTo, out List<NodeReference> returnValues)
        {
            

            returnValues = new List<NodeReference>();
            _start = moveTo;

            Dictionary<string, object> datas = new Dictionary<string, object>();
            ModifyUserNodeValueForm valueForm = new ModifyUserNodeValueForm();
            valueForm.SetUserData(nameof(_totalHeight), _totalHeight);
            valueForm.SetUserData(nameof(_stationHeight), _stationHeight);
            valueForm.SetUserData(nameof(_direction), _direction);
            valueForm.ShowDialog();

            if (valueForm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _totalHeight = Convert.ToInt32(valueForm.gridView1.GetRowCellValue(0, "Value").ToString());
                _stationHeight = Convert.ToInt32(valueForm.gridView1.GetRowCellValue(1, "Value").ToString());
                _direction = Convert.ToDouble(valueForm.gridView1.GetRowCellValue(2, "Value").ToString());
            }


            List<NodeReference> node = new List<NodeReference>();

            if (model.Blocks.Contains(CreatedLineBlock))
                model.Blocks.Remove(CreatedLineBlock);

            if (model.Entities.Contains(CreatedLineReference))
                model.Entities.Remove(CreatedLineReference);

            //Direction 만들어야해ㅠ
            Vector3D direction = new Vector3D(0, 0, 1);


            var end = Vector3D.Add(moveTo, direction * _totalHeight);

            CreateObject(model, _start, end,  direction, out returnValues);
            return true;
        }


        #endregion
        #region [CAD Methods]
        public static void CreateBlock(PinokioBaseModel model, params object[] objects)
        {
            try
            {
                AnimationModelManager.CreateCylinder(_blockName, model, 1, 10, System.Drawing.Color.DodgerBlue);
                AnimationModelManager.CreateCylinder(InnerBlockName, model, 1, 10, System.Drawing.Color.DodgerBlue);
            }
            catch (Exception ex)
            {
                ErrorLogger.SaveLog(ex);
            }
        }

        protected BlockReference ConstructInnerBlockReference(double x, double y, double z, double radian)
        {
            return new BlockReference(x, y, z, this.GetType().Name.ToString() + "_INNER", radian);
        }

        public void CreateObject(PinokioBaseModel model, Point3D startPoint, Point3D endPoint, Vector3D normalDirection, out List<NodeReference> returnValues)
        {
            //층 정보 알아야해
            List<int[]> floorInfo = new List<int[]>();
            Dictionary<uint, CoupledModel> coupledModel = ModelManager.Instance.DicCoupledModel;
            foreach (KeyValuePair<uint, CoupledModel> _Cmodel in coupledModel)
            {
                if (_Cmodel.Value.GetType() == typeof(Pinokio.Model.Base.Floor))
                {
                    Floor _cFloorInfo = _Cmodel.Value as Floor;
                    floorInfo.Add(new int[] { _cFloorInfo.FloorNum, _cFloorInfo.FloorBottom });
                }
            }
            foreach (int[] eachFloor in floorInfo)
            {
                if (eachFloor[1] + _stationHeight < _totalHeight)
                {
                    floor4Lifter += 1;
                }
            }


            endPoint.Z = startPoint.Z + _totalHeight;

            returnValues = new List<NodeReference>();

            Lifter lifter = new Lifter();
            lifter.ParentNode = ModelManager.Instance.SimNodes[model.SelectedFloorID] as CoupledModel;
            ModelManager.Instance.AddNode(lifter);

            LCS Lcs = new LCS();
            Lcs.ParentNode = lifter;
            ModelManager.Instance.AddNode(Lcs);

            //Line 및 Point 만들기
            RefOHTLine line = new RefOHTLine(typeof(RefOHTLine).Name);
            line.MatchingObjType = typeof(ElevatorLine).Name;
            line.Height = Height;
            line.Insert_MouseUp(model, startPoint, out returnValues);
            line.Insert_MouseUp(model, endPoint, out returnValues);
            ((TransportLine)returnValues.First().Core).IsTwoWay = true;

            foreach (NodeReference nodeReference in returnValues)
            {
                nodeReference.Core.ParentNode = lifter;
                AddNodeReference(model, nodeReference);
            }


            //일정 간격으로 Station 만들기
            Point3D stationPoint = startPoint + normalDirection * _stationHeight;

            List<NodeReference> subNodeReturnValues;
            for (int i = 0; i < floor4Lifter; i ++)
            {
                RefLifterLineStation station = new RefLifterLineStation(typeof(RefLifterLineStation).Name);
                station.LineID = line.Core.ID;
                station.MatchingObjType = typeof(ElevatorLineStation).Name;
                subNodeReturnValues = new List<NodeReference>();
                station.SetDirection(_direction); 
                stationPoint.Z = floorInfo[i][1] + _stationHeight;
                station.Insert_MouseUp(model, stationPoint, out subNodeReturnValues);
                station.Core.ParentNode = Lcs;
                AddNodeReference(model, station);
                returnValues.AddRange(subNodeReturnValues);

            }


            //Elevator 만들기
            RefSingleForkElevator elevator = new RefSingleForkElevator(typeof(RefSingleForkElevator).Name);
            elevator.LineID = line.Core.ID;
            elevator.MatchingObjType = typeof(Elevator).Name;
            subNodeReturnValues = new List<NodeReference>();
            elevator.SetDirection(_direction);
            elevator.Insert_MouseUp(model, (startPoint + endPoint) / 2, out subNodeReturnValues);
            subNodeReturnValues.First().Core.ParentNode = Lcs;
            AddNodeReference(model, subNodeReturnValues.First());
            returnValues.AddRange(subNodeReturnValues);

            model.Invalidate();
        }
        private void AddNodeReference(PinokioBaseModel model, NodeReference nodeReference)
        {
            model.AddNodeReference(nodeReference);
            if (model.Entities.Contains(nodeReference) is false)
                model.Entities.Add(nodeReference);

            if (nodeReference.Core != null)
                nodeReference.Core.Size = new PVector3(nodeReference.BoxSize.X, nodeReference.BoxSize.Y, nodeReference.BoxSize.Z);

            if (ModelManager.Instance.SimNodes.ContainsKey(nodeReference.Core.ID) is false)
                ModelManager.Instance.AddNode(nodeReference.Core);
        }
        #endregion
        
    }
}
