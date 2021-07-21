using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CurveSpacingGH : GH_Component
    {
        private bool _useDegrees = false;
        public CurveSpacingGH()
          : base("Curve Spacing", "Curve Spacing",
              "Creates Rebar Group with spacing along a curve",
              "S3DX", "Rebar Spacing")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Id", "Id", "Id as an integer for Rebar Group", GH_ParamAccess.item);
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Rebar Shape to create Rebar Group",
                GH_ParamAccess.item);
            pManager.AddCurveParameter("Curve", "Curve", "Curve to divide and create Rebar Group along this curve",
                GH_ParamAccess.item);
            pManager.AddIntegerParameter("Count", "Count", "Set how many bars should be in the group",
                GH_ParamAccess.item);
            pManager.AddAngleParameter("Rotation Angle", "Rotation Angle", "Set rotation angle for all of the bars",
                GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Group", "Rebar Group", "Group of reinforcement bars",
                GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh that represents reinforcement", GH_ParamAccess.list);
            pManager.AddCurveParameter("Curve", "Curve", "Curves that represents reinforcement", GH_ParamAccess.list);
        }

        protected override void BeforeSolveInstance()
        {
            base.BeforeSolveInstance();
            _useDegrees = false;
            if (Params.Input[4] is Param_Number angleParameter)
                _useDegrees = angleParameter.UseDegrees;
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            int id = 0;
            RebarShape rebarShape = null;
            int count = 0;
            Curve curve = null;
            double angle = 0.0;
            
            DA.GetData(0, ref id);
            DA.GetData(1, ref rebarShape);
            DA.GetData(2, ref curve);
            DA.GetData(3, ref count);
            if (!DA.GetData(4, ref angle)) return;
            if (_useDegrees)
                angle = RhinoMath.ToRadians(angle);

            RebarGroup rebarGroup = new RebarGroup(id, new RebarSpacing(rebarShape, count, curve, angle));

            DA.SetData(0, rebarGroup);
            DA.SetDataList(1, rebarGroup.RebarGroupMesh);
            DA.SetDataList(2, rebarGroup.RebarGroupCurves);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.CurveSpacingShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("494b1a47-7471-4fe4-b70c-27634a7b58a8"); }
        }
    }
}