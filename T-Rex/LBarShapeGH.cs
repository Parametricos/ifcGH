using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class LBarShapeGH : GH_Component
    {
        public LBarShapeGH()
          : base("L-Bar Shape", "L-Bar Shape",
              "Create L-Bar Shape",
              "S3DX", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Plane", "Plane", "Plane where the shape will be inserted",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "Height", "Height of a spacer", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "Width", "Width of a spacer", GH_ParamAccess.item);
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
            Plane insertPlane = Plane.Unset;
            double height = 0.0;
            double width = 0.0;
            RebarProperties properties = null;
            BendingRoller bendingRoller = null;

            DA.GetData(0, ref insertPlane);
            DA.GetData(1, ref height);
            DA.GetData(2, ref width);
            DA.GetData(3, ref properties);
            DA.GetData(4, ref bendingRoller);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.BuildLBarShape(insertPlane, height, width, bendingRoller);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.LBarShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("e27d808b-6340-4aa5-966d-f6aeb574580a"); }
        }
    }
}