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
    public partial class ProductionReportFormDetails : DevExpress.XtraEditors.XtraForm
    {
        #region Variables

        PartStep selectedPartStepCommand;
        EqpStep selectedEqpStepCommand;

        public bool partStepReportForm = false;
        public bool eqpStepReportForm = false;

        private bool _firstView = false;

        public PinokioBaseModel Pinokio3Dmodel = null;
        public ModelDesigner _modelDesigner;

        object beforeSelectedRow;
        #endregion


        public ProductionReportFormDetails(List<PartStep> commands, PinokioBaseModel pinokio3DModel, ModelDesigner modelDesigner)
        {
            InitializeComponent();
            partStepReportForm = true;
            Pinokio3Dmodel = pinokio3DModel;
            _modelDesigner = modelDesigner;
            UpdateGrid(commands);

            pauseSim();
            _firstView = true;
        }
        public ProductionReportFormDetails(List<EqpStep> commands, PinokioBaseModel pinokio3DModel, ModelDesigner modelDesigner)
        {
            InitializeComponent();
            eqpStepReportForm = true;
            Pinokio3Dmodel = pinokio3DModel;
            _modelDesigner = modelDesigner;
            UpdateGridEqp(commands);

            pauseSim();
            _firstView = true;
        }

        /// <summary>
        /// Grid View에 Command/MR 업데이트
        /// </summary>
        private void UpdateGrid<T>(List<T> partList)
        {
            try
            {
                gridViewCommandDetails.BeginUpdate();
                gridViewCommandDetails.Columns.Clear();

                gridControlCommandDetails.DataSource = partList;

                gridViewCommandDetails.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                gridViewCommandDetails.OptionsScrollAnnotations.ShowFocusedRow = 0;

                gridViewCommandDetails.BestFitColumns();
                gridViewCommandDetails.EndUpdate();
            }
            catch (Exception ex)
            {

            }
        }
        private void UpdateGridEqp<T>(List<T> eqpList)
        {
            try
            {
                gridViewCommandDetails.BeginUpdate();
                gridViewCommandDetails.Columns.Clear();

                gridControlCommandDetails.DataSource = eqpList;

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
                if (partStepReportForm)
                {
                    if (_firstView == false)
                        selectedPartStepCommand = gridViewCommandDetails.GetRow(0) as PartStep;
                    else
                        selectedPartStepCommand = gridViewCommandDetails.GetRow(e.FocusedRowHandle) as PartStep;

                    SelectionPartStepTXNodeEQPplan(selectedPartStepCommand);


                    Pinokio3Dmodel.Invalidate();
                    this.gridViewCommandDetails.Focus();
                }
                else if (eqpStepReportForm)
                {
                    if (_firstView == false)
                        selectedEqpStepCommand = gridViewCommandDetails.GetRow(0) as EqpStep;
                    else
                        selectedEqpStepCommand = gridViewCommandDetails.GetRow(e.FocusedRowHandle) as EqpStep;

                    SelectionEqpStepTXNodeEQPplan(selectedEqpStepCommand);


                    Pinokio3Dmodel.Invalidate();
                    this.gridViewCommandDetails.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void gridViewCommandDetails_Click(object sender, EventArgs e)
        {
            if (partStepReportForm)
            {

                selectedPartStepCommand = gridViewCommandDetails.GetRow(gridViewCommandDetails.FocusedRowHandle) as PartStep;
                SelectionPartStepTXNodeEQPplan(selectedPartStepCommand);
                
            }
            else if (eqpStepReportForm)
            {
                selectedEqpStepCommand = gridViewCommandDetails.GetRow(gridViewCommandDetails.FocusedRowHandle) as EqpStep;
                SelectionEqpStepTXNodeEQPplan(selectedEqpStepCommand);

            }
            Pinokio3Dmodel.Invalidate();
            this.gridViewCommandDetails.Focus();
        }

        /// <summary>
        /// Row 변경에 따른 MR TXnode들 Seletion
        /// </summary>
        private void SelectionPartStepTXNodeEQPplan(PartStep selectedCommand)
        {
            foreach (NodeReference node in Pinokio3Dmodel.NodeReferenceByID.Values)
            {
                node.Selected = false;
            }

            if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(selectedCommand.EqpID))
                Pinokio3Dmodel.NodeReferenceByID[selectedCommand.EqpID].Selected = true;
           
            _modelDesigner.CheangeSelectedSimObject4PropertyGrid(Pinokio3Dmodel.NodeReferenceByID[selectedCommand.EqpID].Core);
        }
        /// <summary>
        /// Row 변경에 따른 MR TXnode들 Seletion
        /// </summary>
        private void SelectionEqpStepTXNodeEQPplan(EqpStep selectedCommand)
        {
            foreach (NodeReference node in Pinokio3Dmodel.NodeReferenceByID.Values)
            {
                node.Selected = false;
            }

            if (Pinokio3Dmodel.NodeReferenceByID.ContainsKey(selectedCommand.EqpID))
                Pinokio3Dmodel.NodeReferenceByID[selectedCommand.EqpID].Selected = true;

            _modelDesigner.CheangeSelectedSimObject4PropertyGrid(Pinokio3Dmodel.NodeReferenceByID[selectedCommand.EqpID].Core);
        }

        /// <summary>
        /// Form 종료 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductionReportFormDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            partStepReportForm = false;
            eqpStepReportForm = false;
            Pinokio3Dmodel.Entities.ClearSelection();

            startSim();
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
    }
}