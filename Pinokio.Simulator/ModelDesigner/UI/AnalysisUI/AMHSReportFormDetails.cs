using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulation.Engine;
using DevExpress.XtraSplashScreen;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using System.IO;
using DevExpress.XtraWaitForm;
using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using devDept.Eyeshot.Entities;
using devDept.Graphics;
using devDept.Eyeshot;
using devDept.Geometry;
using Pinokio.Model.Base;
using Pinokio.Animation;
using Pinokio.Geometry;
using Pinokio.Designer.UI;
using Link = DevExpress.XtraPrinting.Link;
using Pinokio.UI.Base;
using ComboBox = DevExpress.XtraEditors.ComboBox;

namespace Pinokio.Designer
{
    /// <summary>
    /// Command 별 상세 정보 조회 Form
    /// </summary>
    public partial class AMHSReportFormDetails : DevExpress.XtraEditors.XtraForm
    {
        #region Variables

        Command selectedCommand;
        MR selectedMR;

        private bool _firstView = false;
        private bool _RouteRailInfoClick = false;

        public PinokioBaseModel Pinokio3Dmodel = null;
        public ModelDesigner _modelDesigner;

        object beforeSelectedRow;

        Dictionary<string, Tuple<NodeReference, Color>> changedNodeRef = new Dictionary<string, Tuple<NodeReference, Color>>();
        #endregion


        public AMHSReportFormDetails(List<Command> commands, PinokioBaseModel pinokio3DModel, ModelDesigner modelDesigner)
        {
            InitializeComponent();

            Pinokio3Dmodel = pinokio3DModel;
            _modelDesigner = modelDesigner;
            _modelDesigner.changedNodeRef = new Dictionary<string, Tuple<NodeReference, Color>>();
            UpdateGrid(commands);

            pauseSim();
            DoChangeVisibleOfOHT(false);
            _firstView = true;
        }

        public AMHSReportFormDetails(List<MR> commands, PinokioBaseModel pinokio3DModel, ModelDesigner modelDesigner)
        {
            InitializeComponent();

            Pinokio3Dmodel = pinokio3DModel;
            _modelDesigner = modelDesigner;
            _modelDesigner.changedNodeRef = new Dictionary<string, Tuple<NodeReference, Color>>();
            UpdateGrid(commands);

            pauseSim();
            DoChangeVisibleOfOHT(false);
            _firstView = true; 
        }


        /// <summary>
        /// Grid View에 Command/MR 업데이트
        /// </summary>
        private void UpdateGrid<T>(List<T> list)
        {
            try
            {
                gridViewCommandDetails.BeginUpdate();
                gridViewCommandDetails.Columns.Clear();

                gridControlCommandDetails.DataSource = list;

                gridViewCommandDetails.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                gridViewCommandDetails.OptionsScrollAnnotations.ShowFocusedRow = 0;

                gridViewCommandDetails.BestFitColumns();
                gridViewCommandDetails.EndUpdate();
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// 선택된 Command 변경 함수
        /// </summary>
        private void gridViewCommandDetails_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                _RouteRailInfoClick = false;

                if (gridViewCommandDetails.GetRow(e.FocusedRowHandle).GetType().Name == "MR")
                {
                    if (_firstView == false)
                        selectedMR = gridViewCommandDetails.GetRow(0) as MR;
                    else
                        selectedMR = gridViewCommandDetails.GetRow(e.FocusedRowHandle) as MR;

                    //SelectionTXNodeMR(selectedMR);
                    UpdateRouteRailEdgeInfo(selectedMR);
                }
                else
                {
                    if (_firstView == false)
                        selectedCommand = gridViewCommandDetails.GetRow(0) as Command;
                    else
                        selectedCommand = gridViewCommandDetails.GetRow(e.FocusedRowHandle) as Command;

                    UpdateRouteRailEdgeInfo(selectedCommand);
                }
                Pinokio3Dmodel.Invalidate();
                this.gridViewCommandDetails.Focus();
            }
            catch (Exception ex)
            {

            }
        }

        private void gridViewCommandDetails_Click(object sender, EventArgs e)
        {
            if (gridViewCommandDetails.GetRow(gridViewCommandDetails.FocusedRowHandle).GetType().Name == "MR")
            {
                selectedMR = gridViewCommandDetails.GetRow(gridViewCommandDetails.FocusedRowHandle) as MR;
                //SelectionTXNodeMR(selectedMR);
                UpdateRouteRailEdgeInfo(selectedMR);
            }
            else
            {
                selectedCommand = gridViewCommandDetails.GetRow(gridViewCommandDetails.FocusedRowHandle) as Command;
                UpdateRouteRailEdgeInfo(selectedCommand);
            }
            Pinokio3Dmodel.Invalidate();
            this.gridViewCommandDetails.Focus();
        }

        /// <summary>
        /// Row 변경에 따른 MR TXnode들 Seletion
        /// </summary>
        private void SelectionTXNodeMR(MR selectedMR)
        {
            // 추가된 Line 삭제
            foreach (var nodeRef in changedNodeRef.Values)
            {
                nodeRef.Item1.Color = nodeRef.Item2;
                nodeRef.Item1.LineWeight = 0.5f;
                nodeRef.Item1.Selected = false;
            }

            foreach (TXNode txNode in selectedMR.Route)
            {
                if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(txNode.ID))
                {
                    NodeReference nodeRef = Pinokio3Dmodel.NodeReferenceByID[txNode.ID];
                    Color originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 0, 254);

                    if (!changedNodeRef.ContainsKey(nodeRef.Name))
                        changedNodeRef.Add(nodeRef.Name, new Tuple<NodeReference, Color>(nodeRef, originColor));
                }
            }
        }
        private void UpdateRouteRailEdgeInfo(MR selectedMR)
        {
            try
            {
                foreach (var nodeRef in _modelDesigner.changedNodeRef.Values)
                {
                    nodeRef.Item1.Color = nodeRef.Item2;
                    nodeRef.Item1.LineWeight = 0.5f;
                    nodeRef.Item1.Selected = false;
                }

                List<DetailRouteRailInfo> detailInfo = new List<DetailRouteRailInfo>();
                int order = 1;

                foreach (TXNode txNode in selectedMR.Route)
                {
                    DetailRouteRailInfo DetailRouteRailInfo = new DetailRouteRailInfo();
                    DetailRouteRailInfo.Name = txNode.Name;

                    if (txNode == selectedMR.Route[0])
                    {
                        _modelDesigner.changeTXNodeColor(txNode, RouteRailType.StartNode);
                        DetailRouteRailInfo.Type = RouteRailType.StartNode;
                    }

                    else if (txNode == selectedMR.Route[selectedMR.Route.Count() - 1])
                    {
                        _modelDesigner.changeTXNodeColor(txNode, RouteRailType.EndNode);
                        DetailRouteRailInfo.Type = RouteRailType.EndNode;
                    }
                    else
                    {
                        _modelDesigner.changeTXNodeColor(txNode, RouteRailType.Transferring);
                        DetailRouteRailInfo.Type = RouteRailType.Transferring;
                    }

                    DetailRouteRailInfo.Order = order;
                    detailInfo.Add(DetailRouteRailInfo);
                    ++order;
                }

                this.gridControlRouteRailEdge.DataSource = detailInfo;
                this.gridControlRouteRailEdge.RefreshDataSource();
                this.gridViewRouteRailEdge.FocusInvalidRow();
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Row 변경에 따른 Command Line 중 Acquire / Actual Route Line 추가
        /// </summary>
        private void UpdateRouteRailEdgeInfo(Command selectedCommand)
        {
            try
            {
                // 추가된 Line 삭제
                foreach (var nodeRef in _modelDesigner.changedNodeRef.Values)
                {
                    nodeRef.Item1.Color = nodeRef.Item2;
                    nodeRef.Item1.LineWeight = 0.5f;
                    nodeRef.Item1.Selected = false;
                }

                List<DetailRouteRailInfo> detailInfo = new List<DetailRouteRailInfo>();
                int order = 1;

                // Acquire Route 그리기
                foreach (TransportLine AcqLine in selectedCommand.AcquireRoute)
                {
                    DetailRouteRailInfo DetailRouteRailInfo = new DetailRouteRailInfo();
                    DetailRouteRailInfo.Name = AcqLine.Name;

                    
                    if (AcqLine == selectedCommand.AcquireRoute[selectedCommand.AcquireRoute.Count - 1])
                    {
                        DetailRouteRailInfo.Type = RouteRailType.Acquire;
                        _modelDesigner.changeNodeColor(AcqLine, RouteRailType.Acquire);
                        //changeNodeColor(AcqLine, RouteRailType.Acquire);
                    }
                    else
                    {
                        DetailRouteRailInfo.Type = RouteRailType.Waiting;
                        _modelDesigner.changeNodeColor(AcqLine, RouteRailType.Waiting);
                        //changeNodeColor(AcqLine, RouteRailType.Waiting);
                    }

                    DetailRouteRailInfo.Order = order;
                    detailInfo.Add(DetailRouteRailInfo);
                    ++order;
                }

                //Actual Route 그리기
                foreach (TransportLine ActLine in selectedCommand.ActualRoute)
                {
                    DetailRouteRailInfo DetailRouteRailInfo = new DetailRouteRailInfo();
                    DetailRouteRailInfo.Name = ActLine.Name;

                    if (ActLine == selectedCommand.ActualRoute[selectedCommand.ActualRoute.Count - 1])
                    {
                        DetailRouteRailInfo.Type = RouteRailType.Deposit;
                        _modelDesigner.changeNodeColor(ActLine, RouteRailType.Deposit);
                        //changeNodeColor(ActLine, RouteRailType.Deposit);
                    }
                    else if (ActLine == selectedCommand.ActualRoute[0])
                    {
                        DetailRouteRailInfo.Type = RouteRailType.Transferring;
                    }
                    else
                    {
                        DetailRouteRailInfo.Type = RouteRailType.Transferring;
                        _modelDesigner.changeNodeColor(ActLine, RouteRailType.Transferring);
                        //changeNodeColor(ActLine, RouteRailType.Transferring);
                    }


                    DetailRouteRailInfo.Order = order;
                    detailInfo.Add(DetailRouteRailInfo);
                    ++order;
                }

                this.gridControlRouteRailEdge.DataSource = detailInfo;
                this.gridControlRouteRailEdge.RefreshDataSource();
                this.gridViewRouteRailEdge.FocusInvalidRow();
            }
            catch (Exception e)
            {

            }
        }



        private void changeNodeColor(TransportLine line, RouteRailType type)
        {
            NodeReference nodeRef = null;
            Color originColor = Color.Gray;
            switch (type)
            {
                case RouteRailType.Acquire:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 129, 0);
                    nodeRef.LineWeight = 1f;

                    break;

                case RouteRailType.Waiting:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 173, 255, 50);
                    nodeRef.LineWeight = 1f;

                    break;
                case RouteRailType.Deposit:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 0, 0, 254);
                    nodeRef.LineWeight = 1f;

                    break;
                case RouteRailType.Transferring:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 255, 166, 0);
                    nodeRef.LineWeight = 1f;

                    break;
                case RouteRailType.Select:
                    nodeRef = Pinokio3Dmodel.NodeReferenceByID[line.ID];
                    originColor = nodeRef.Color;
                    nodeRef.Color = Color.FromArgb(255, 254, 0, 0);
                    nodeRef.LineWeight = 1f;

                    break;
            }
            if (!changedNodeRef.ContainsKey(nodeRef.Name))
                changedNodeRef.Add(nodeRef.Name, new Tuple<NodeReference, Color>(nodeRef, originColor));

        }

        /// <summary>
        /// Detail Route Info 선택 시 Line 추가 (Red)
        /// </summary>
        private void gridViewRouteRailEdge_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (selectedCommand == null || _RouteRailInfoClick == false)
                     ;
                else
                {
                    var rowlist = gridViewRouteRailEdge.GetRow(e.FocusedRowHandle);
                    if (rowlist == null) return;
                    var node = Pinokio3Dmodel.NodeReferenceByID.FirstOrDefault(pair => pair.Value.Name == ((DetailRouteRailInfo)rowlist).Name.ToString());

                    RouteRailInfo_color(rowlist);
                }

                if (selectedMR != null && _RouteRailInfoClick != false)
                {
                    var rowlist = gridViewRouteRailEdge.GetRow(e.FocusedRowHandle);
                    if (rowlist == null) return;
                    var node = Pinokio3Dmodel.NodeReferenceByID.FirstOrDefault(pair => pair.Value.Name == ((DetailRouteRailInfo)rowlist).Name.ToString());

                    RouteRailInfo_color(rowlist);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void gridViewRouteRailEdge_Click(object sender, EventArgs e)
        {
            if (!_RouteRailInfoClick) _RouteRailInfoClick = true;

            var rowlist = gridViewRouteRailEdge.GetFocusedRow();
            if (rowlist == null) return;

            RouteRailInfo_color(rowlist);            

        }

        private void RouteRailInfo_color(object rowlist)
        {
            bool beforeSelectComplete = false;

            if (selectedCommand != null)
            {
                if (rowlist == beforeSelectedRow) { Pinokio3Dmodel.Invalidate(); beforeSelectedRow = null; return; }

                string select_lineName = ((DetailRouteRailInfo)rowlist).Name.ToString();

                foreach (TransportLine select_line in selectedCommand.AcquireRoute)
                {
                    if (select_line.Name == select_lineName)
                    {
                        _modelDesigner.changeNodeColor(select_line, RouteRailType.Select);
                        //changeNodeColor(select_line, RouteRailType.Select);
                        continue;
                    }
                    if (beforeSelectedRow != null && beforeSelectComplete == false && select_line.Name == ((DetailRouteRailInfo)beforeSelectedRow).Name)
                    {
                        _modelDesigner.changeNodeColor(select_line, ((DetailRouteRailInfo)beforeSelectedRow).Type);
                        //changeNodeColor(select_line, ((DetailRouteRailInfo)beforeSelectedRow).Type);
                        beforeSelectComplete = true;
                    }
                }
                foreach (TransportLine select_line in selectedCommand.ActualRoute)
                {
                    if (select_line.Name == select_lineName)
                    {
                        _modelDesigner.changeNodeColor(select_line, RouteRailType.Select);
                        //changeNodeColor(select_line, RouteRailType.Select);
                        continue;
                    }
                    if (beforeSelectedRow != null && beforeSelectComplete == false && select_line.Name == ((DetailRouteRailInfo)beforeSelectedRow).Name)
                    {
                        _modelDesigner.changeNodeColor(select_line, ((DetailRouteRailInfo)beforeSelectedRow).Type);
                        //changeNodeColor(select_line, ((DetailRouteRailInfo)beforeSelectedRow).Type);
                        beforeSelectComplete = true;
                    }
                }

                if (beforeSelectedRow != null && ((DetailRouteRailInfo)beforeSelectedRow).Name != select_lineName && selectedCommand.AcquireRoute.FirstOrDefault(line => line.Name == ((DetailRouteRailInfo)beforeSelectedRow).Name) != null && selectedCommand.ActualRoute.FirstOrDefault(line => line.Name == ((DetailRouteRailInfo)beforeSelectedRow).Name) != null)
                {
                    _modelDesigner.changeNodeColor(selectedCommand.AcquireRoute.FirstOrDefault(line => line.Name == ((DetailRouteRailInfo)beforeSelectedRow).Name), RouteRailType.Acquire);
                    //changeNodeColor(selectedCommand.AcquireRoute.FirstOrDefault(line => line.Name == ((DetailRouteRailInfo)beforeSelectedRow).Name), RouteRailType.Acquire);
                }

                Pinokio3Dmodel.Invalidate();
                
                beforeSelectedRow = rowlist;
                
            }

            var node = Pinokio3Dmodel.NodeReferenceByID.FirstOrDefault(pair => pair.Value.Name == ((DetailRouteRailInfo)rowlist).Name.ToString());
            _modelDesigner.CheangeSelectedSimObject4PropertyGrid(node.Value.Core);
        }

        ///// <summary>
        ///// Route Rail Type에 따른 색깔 표현 함수 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void gridViewRouteRailEdge_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "Type")
                {
                    switch ((RouteRailType)e.CellValue)
                    {

                        case RouteRailType.Acquire:
                            Pen pen = new Pen(Color.FromArgb(255, 0, 129, 0), 1);
                            e.Cache.DrawRectangle(pen, e.Bounds);
                            break;

                        case RouteRailType.Deposit:
                            pen = new Pen(Color.FromArgb(255, 0, 0, 254), 1);
                            e.Cache.DrawRectangle(pen, e.Bounds);
                            break;

                        case RouteRailType.Waiting:
                            pen = new Pen(Color.FromArgb(255, 173, 255, 50), 1);
                            e.Cache.DrawRectangle(pen, e.Bounds);
                            break;

                        case RouteRailType.Transferring:
                            pen = new Pen(Color.FromArgb(255, 255, 166, 0), 1);
                            e.Cache.DrawRectangle(pen, e.Bounds);
                            break;

                        case RouteRailType.StartNode:
                            pen = new Pen(Color.FromArgb(255, 0, 129, 0), 1);
                            e.Cache.DrawRectangle(pen, e.Bounds);
                            break;

                        case RouteRailType.EndNode:
                            pen = new Pen(Color.FromArgb(255, 0, 0, 254), 1);
                            e.Cache.DrawRectangle(pen, e.Bounds);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Form 종료 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AMHSReportFormDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Form 종료시 색깔 원래대로
            foreach (var nodeRef in _modelDesigner.changedNodeRef.Values)
            {
                nodeRef.Item1.Color = nodeRef.Item2;
                nodeRef.Item1.LineWeight = 0.5f;
                nodeRef.Item1.Selected = false;
            }
            _modelDesigner.changedNodeRef.Clear();
            startSim();
            DoChangeVisibleOfOHT(true);
        }

        private void pauseSim()
        {
            if (SimEngine.Instance.EngineState != ENGINE_STATE.STOP)
            { 
                _modelDesigner.Animation_Tog_Switch.Checked = false;
                _modelDesigner.PauseSimulation();

                _modelDesigner.DoChangeEnabledOfBBI_DetailForm(false);
            }
        }

        private void startSim()
        {
            if (SimEngine.Instance.EngineState != ENGINE_STATE.STOP)
            {
                _modelDesigner.startSimulation();
                _modelDesigner.Animation_Tog_Switch.Checked = true;

                _modelDesigner.DoChangeEnabledOfBBI_DetailForm(true);
            }
        }

        private void DoChangeVisibleOfOHT(bool visible)
        {
            foreach (Block bl in Pinokio3Dmodel.Blocks.ToArray())
            {
                if (bl.Name.Contains("OHT_"))
                {
                    foreach (Entity en in bl.Entities)
                    {
                        if (visible)
                            en.Visible = true;
                        else
                            en.Visible = false;
                    }
                }
            }
        }
        

    }
}