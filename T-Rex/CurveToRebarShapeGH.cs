using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Display;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class CurveToRebarShapeGH : GH_Component
    {
        public CurveToRebarShapeGH()
          : base("Curve To Rebar Shape", "Curve To Rebar Shape",
              "Convert single curve to reinforcement bar shape",
              "S3DX", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "Curve", "Curve needed to create a reinforcement bar shape",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Reinforcement bar shape", GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh that represents reinforcement", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Curve rebarCurve = null;
            RebarProperties props = null;

            DA.GetData(0, ref rebarCurve);
            DA.GetData(1, ref props);

            RebarShape rebarShape = new RebarShape(props);
            rebarShape.CurveToRebarShape(rebarCurve);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.CurveToRebarShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("0495d754-e727-4eab-a915-a3e04608c7ca"); }
        }
    }
}
