using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class PolylineToRebarShapeGH : GH_Component
    {
        public PolylineToRebarShapeGH()
          : base("Polyline To Rebar Shape", "Polyline To Rebar Shape",
              "Convert single polyline curve to reinforcement bar shape",
              "S3DX", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("Polyline", "Polyline", "Polyline needed to create a reinforcement bar shape",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bending Roller", "Bending Roller",
                "Bending roller", GH_ParamAccess.item);
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
            BendingRoller bendingRoller = null;

            DA.GetData(0, ref rebarCurve);
            DA.GetData(1, ref props);
            DA.GetData(2, ref bendingRoller);

            RebarShape rebarShape = new RebarShape(props);
            rebarShape.PolylineToRebarShape(rebarCurve, bendingRoller);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.PolylineToRebarShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("f3f24399-2626-42de-8555-c9e6a181bf6d"); }
        }
    }
}