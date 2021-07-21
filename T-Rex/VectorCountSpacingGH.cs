using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;
using T_RexEngine.ElementLibrary;

namespace T_Rex
{
    public class VectorCountSpacingGH : GH_Component
    {
        public VectorCountSpacingGH()
          : base("Vector Count Spacing", "Vector Count Spacing",
              "Creates Rebar Group with vector and rebar count. Start begin where the rebar shape is.",
              "S3DX", "Rebar Spacing")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Id", "Id", "Id as an integer for Rebar Group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Rebar Shape to create Rebar Group",
                GH_ParamAccess.item);
            pManager.AddVectorParameter("Vector", "Vector", "Vector that defines direction and distance where all rebars will be created",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("Count", "Count", "How many rebars will be in a group", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh group representation", GH_ParamAccess.list);
            pManager.AddCurveParameter("Curve", "Curve", "Curves that represents reinforcement", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int id = 0;
            RebarShape rebarShape = null;
            Vector3d vector = new Vector3d();
            int count = 0;

            DA.GetData(0, ref id);
            DA.GetData(1, ref rebarShape);
            DA.GetData(2, ref vector);
            DA.GetData(3, ref count);

            RebarGroup rebarGroup = new RebarGroup(id, new RebarSpacing(rebarShape, vector, count));

            DA.SetData(0, rebarGroup);
            DA.SetDataList(1, rebarGroup.RebarGroupMesh);
            DA.SetDataList(2, rebarGroup.RebarGroupCurves);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.VectorCountSpacing;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("970859f2-e15e-45c0-99cd-402fa20847a5"); }
        }
    }
}