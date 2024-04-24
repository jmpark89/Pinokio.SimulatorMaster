using Pinokio.DevTool;
using Pinokio.DevTool.GenCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinokio.Designer.UI.AssetEdit
{
    public class AssemblyDataSource
    {
        public bool UploadedObj
        {
            get
            {
                if (_objPath == string.Empty)
                    return false;
                else
                    return true;
            }
        }
        public bool UploadedReferenceClass
        {
            get
            {
                if (_referenceClassPath == string.Empty)
                    return false;
                else
                    return true;
            }
        }
        public bool UploadedSimulationClass
        {
            get
            {
                if (_simulationClassPath == string.Empty)
                    return false;
                else
                    return true;
            }
        }

        private string _referenceClassPath;
        private string _simulationClassPath;
        private string _objPath;

        public string ReferenceClassPath { get => _referenceClassPath; set => _referenceClassPath = value; }
        public string SimulationClassPath { get => _simulationClassPath; set => _simulationClassPath = value; }
        public string ObjPath { get => _objPath; set => _objPath = value; }


        public PinokioCodeDomRefBy3DModel ReferenceClassGenerator;
        public PinokioCodeDomNode SimulationClassGenerator;
    }
}
