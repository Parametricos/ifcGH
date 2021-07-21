using System.Collections.Generic;
using T_RexEngine;
using System;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace T_Rex
{
    public class CreateIfcGH : GH_Component
    {
        public CreateIfcGH()
          : base("Create IFC", "Create IFC",
              "Creates IFC file. Make sure that groups are modelled in millimeters, because right now all of the input will be exported as millimeters.",
              "S3DX", "IFC")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Groups", "Groups", "T-Rex groups to add to the IFC file",
                GH_ParamAccess.list);
            pManager.AddTextParameter("Project Name", "Project Name", "Name of the project",
                GH_ParamAccess.item);
            pManager.AddTextParameter("Building Name", "Building Name", "Name of the building",
                GH_ParamAccess.item);
            pManager.AddTextParameter("Path", "Path", "Path where the IFC file will be saved, should end up with .ifc",
                GH_ParamAccess.item);
            pManager.AddBooleanParameter("EnableGenerate", "EnableGenerate", "Enable generation of ifc",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<ElementGroup> elementGroups = new List<ElementGroup>();
            string projectName = string.Empty;
            string buildingName = string.Empty;
            string path = string.Empty;
            bool enableGen = false;

            DA.GetDataList(0, elementGroups);
            DA.GetData(1, ref projectName);
            DA.GetData(2, ref buildingName);
            DA.GetData(3, ref path);
            DA.GetData(4, ref enableGen);

            if (enableGen)
            {
                Ifc Ifc = new Ifc(elementGroups, projectName, buildingName, path);
            }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.IFC;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("7f23dff5-2e3f-4190-9aae-e57e988f29d3"); }
        }
    }
}