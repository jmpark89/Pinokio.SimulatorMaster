using Pinokio.Database;
using Pinokio.Model.Base;
using Simulation.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pinokio.Designer
{
    public class SnapShotManager
    {
        [NonSerialized]
        public deleAlarmGeneratePart GeneratePart;
        [NonSerialized]
        public deleAlarmGenerateParts GenerateParts;
        [NonSerialized]
        public deleAlarmDeletePart DeletePart;
        [NonSerialized]
        public deleSimulationEnd SimulationEnd;
        [NonSerialized]
        public deleFaliSimulation FailSimulation;

        [NonSerialized]
        private PlayBackNode _playBackNode = null;
        [NonSerialized]
        private PlayBackSaveNode _playBackSaveNode = null;

        [NonSerialized]
        private SimTimeNode _simTimeNode = null;
        [NonSerialized]
        private AnimationNode _animationNode = null;
        [NonSerialized]
        private MeasureAccelerationTimeNode _accelerationTimeNode = null;
        [NonSerialized]
        private SetAccelerationTimeNode _setAccelerationTimeNode = null;
        [NonSerialized]
        private EnginePauseNode _enginePauseNode = null;
        [NonSerialized]
        private EngineStopNode _engineStopNode = null;
        [NonSerialized]
        private EngineWarmUpNode _engineWarmUpNode = null;
        [NonSerialized]
        private SimResultDBManager _simResultDBManager = null;

        public SnapShotData SnapShotData { get; set; }

        public SnapShotManager(ModelManager modelMngr, FactoryManager factoryManager, SimEngine simEngine, string path)
        {
            GeneratePart = modelMngr.GeneratePart;
            GenerateParts = modelMngr.GenerateParts;
            DeletePart = modelMngr.DeletePart;
            SimulationEnd = modelMngr.SimulationEnd;
            FailSimulation = modelMngr.FailSimulation;
            _playBackNode = modelMngr.PlayBackNode;
            _playBackSaveNode = modelMngr.PlayBackSaveNode;
            _simTimeNode = modelMngr.TimeNode;
            _animationNode = modelMngr.AnimationNode;
            _accelerationTimeNode = modelMngr.MeasureAccelerationTimeNode;
            _setAccelerationTimeNode = modelMngr.SetAccelerationTimeNode;
            _enginePauseNode = modelMngr.EnginePauseNode;
            _engineStopNode = modelMngr.EngineStopNode;
            _engineWarmUpNode = modelMngr.EngineWarmUpNode;
            _simResultDBManager = modelMngr.SimResultDBManager;
            SnapShotData = new SnapShotData(modelMngr, factoryManager, simEngine, path);

            modelMngr.GeneratePart = null;
            modelMngr.GenerateParts = null;
            modelMngr.DeletePart = null;
            modelMngr.SimulationEnd = null;
            modelMngr.FailSimulation = null;
            modelMngr.RemoveNode(ModelManager.Instance.PlayBackNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.PlayBackNode);
            modelMngr.PlayBackNode = null;
            modelMngr.RemoveNode(ModelManager.Instance.PlayBackSaveNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.PlayBackSaveNode);
            modelMngr.PlayBackSaveNode = null;
            modelMngr.RemoveNode(ModelManager.Instance.TimeNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.TimeNode);
            modelMngr.TimeNode = null;
            modelMngr.RemoveNode(ModelManager.Instance.AnimationNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.AnimationNode);
            modelMngr.AnimationNode = null;
            modelMngr.RemoveNode(ModelManager.Instance.MeasureAccelerationTimeNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.MeasureAccelerationTimeNode);
            modelMngr.MeasureAccelerationTimeNode = null;
            modelMngr.RemoveNode(ModelManager.Instance.SetAccelerationTimeNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.SetAccelerationTimeNode);
            modelMngr.SetAccelerationTimeNode = null;
            modelMngr.RemoveNode(ModelManager.Instance.EnginePauseNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.EnginePauseNode);
            modelMngr.EnginePauseNode = null;
            modelMngr.RemoveNode(ModelManager.Instance.EngineStopNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.EngineStopNode);
            modelMngr.EngineStopNode = null;
            modelMngr.RemoveNode(ModelManager.Instance.EngineWarmUpNode);
            simEngine.SimNodes.Remove(ModelManager.Instance.EngineWarmUpNode);
            modelMngr.EngineWarmUpNode = null;
            modelMngr.SimResultDBManager = null;
        }

        // Serialize 예제
        public void SerializeSingleton(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, SnapShotData);
                SetModelManagerVariables();
                Console.WriteLine("Singleton 객체를 파일로 Serialize 완료");
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Serialize 실패: " + e.Message);
            }
            finally
            {
                fileStream.Close();
            }
        }

        private void SetModelManagerVariables()
        {
            ModelManager.Instance.GeneratePart = GeneratePart;
            ModelManager.Instance.GenerateParts = GenerateParts;
            ModelManager.Instance.DeletePart = DeletePart;
            ModelManager.Instance.SimulationEnd = SimulationEnd;
            ModelManager.Instance.FailSimulation = FailSimulation;
            if (_playBackNode != null)
            {
                ModelManager.Instance.AddNode(_playBackNode);
                ModelManager.Instance.PlayBackNode = _playBackNode;
                SimEngine.Instance.SimNodes.Add(_playBackNode);
                _playBackNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }
            if (_playBackSaveNode != null)
            {
                ModelManager.Instance.AddNode(_playBackSaveNode);
                ModelManager.Instance.PlayBackSaveNode = _playBackSaveNode;
                SimEngine.Instance.SimNodes.Add(_playBackSaveNode);
                _playBackSaveNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }
            if (_simTimeNode != null)
            {
                ModelManager.Instance.AddNode(_simTimeNode);
                ModelManager.Instance.TimeNode = _simTimeNode;
                SimEngine.Instance.SimNodes.Add(_simTimeNode);
                _simTimeNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }
            if (_animationNode != null)
            {
                ModelManager.Instance.AddNode(_animationNode);
                ModelManager.Instance.AnimationNode = _animationNode;
                SimEngine.Instance.SimNodes.Add(_animationNode);
                _animationNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }
            if (_accelerationTimeNode != null)
            {
                ModelManager.Instance.AddNode(_accelerationTimeNode);
                ModelManager.Instance.MeasureAccelerationTimeNode = _accelerationTimeNode;
                SimEngine.Instance.SimNodes.Add(_accelerationTimeNode);
                _accelerationTimeNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }
            if (_setAccelerationTimeNode != null)
            {
                ModelManager.Instance.AddNode(_setAccelerationTimeNode);
                ModelManager.Instance.SetAccelerationTimeNode = _setAccelerationTimeNode;
                SimEngine.Instance.SimNodes.Add(_setAccelerationTimeNode);
                _setAccelerationTimeNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }
            if (_enginePauseNode != null)
            {
                ModelManager.Instance.AddNode(_enginePauseNode);
                ModelManager.Instance.EnginePauseNode = _enginePauseNode;
                SimEngine.Instance.SimNodes.Add(_enginePauseNode);
                _enginePauseNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }
            if (_engineStopNode != null)
            {
                ModelManager.Instance.AddNode(_engineStopNode);
                ModelManager.Instance.EngineStopNode = _engineStopNode;
                SimEngine.Instance.SimNodes.Add(_engineStopNode);
                _engineStopNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }
            if (_engineWarmUpNode != null)
            {
                ModelManager.Instance.AddNode(_engineWarmUpNode);
                ModelManager.Instance.EngineWarmUpNode = _engineWarmUpNode;
                SimEngine.Instance.SimNodes.Add(_engineWarmUpNode);
                _engineWarmUpNode.EvtCalendar = SimEngine.Instance.EventCalender;
            }

            ModelManager.Instance.SimResultDBManager = _simResultDBManager;
        }

        // Deserialize 예제
        public void DeserializeSingleton(string filePath)
        {
            FileStream readFileStream = new FileStream(filePath, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                SnapShotData snapShotData = (SnapShotData)formatter.Deserialize(readFileStream);
                ModelManager.Instance = snapShotData.ModelManager;
                FactoryManager.Instance = snapShotData.FactoryManager;
                SimEngine.Instance = snapShotData.SimEngine;
                AccessDB.Instance.DBPath = snapShotData.LayoutPath;
                SetModelManagerVariables();
                Console.WriteLine("Singleton 객체를 파일에서 Deserialize 완료");
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Deserialize 실패: " + e.Message);
            }
            finally
            {
                readFileStream.Close();
            }
        }
    }

    [Serializable]
    public class SnapShotData
    {
        public ModelManager ModelManager { get; set; }
        public FactoryManager FactoryManager { get; set; }
        public SimEngine SimEngine { get; set; }
        public string LayoutPath { get;set;}
        
        public SnapShotData(ModelManager modelManager, FactoryManager factoryManager, SimEngine simEngine, string layoutPath)
        {
            ModelManager = modelManager;
            FactoryManager = factoryManager;
            SimEngine = simEngine;
            LayoutPath = layoutPath;
        }
    }
}
