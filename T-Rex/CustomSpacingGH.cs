using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CustomSpacingGH : GH_Component
    {
        public CustomSpacingGH()
          : base("Custom Spacing", "Custom Spacing",
              "Creates the Rebar Group without any additional spacing - you have to set the spaces by creating Rebar Shapes that have already have spaces between them." +
              " Add 1 or more Rebar Shapes to create that Rebar Group.",
              "S3DX", "Rebar Spacing")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Id", "Id", "Id as an integer for Rebar Group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Rebar Shapes", "Rebar Shapes", "Rebar Shapes to create the Rebar Group",
                GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of the reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh group representation", GH_ParamAccess.list);
            pManager.AddCurveParameter("Curve", "Curve", "Curves that represents reinforcement", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int id = 0;
            List<RebarShape> rebarShapes = new List<RebarShape>();

            DA.GetData(0, ref id);
            DA.GetDataList(1, rebarShapes);

            RebarGroup rebarGroup = new RebarGroup(id, rebarShapes);

            DA.SetData(0, rebarGroup);
            DA.SetDataList(1, rebarGroup.RebarGroupMesh);
            DA.SetDataList(2, rebarGroup.RebarGroupCurves);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.WithoutSpacing;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("01cc9089-168d-4422-8aff-1012a3a85f9f"); }
        }
    }
}