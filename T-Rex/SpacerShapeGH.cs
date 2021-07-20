using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class SpacerShapeGH : GH_Component
    {
        public SpacerShapeGH()
          : base("Spacer Shape", "Spacer Shape",
              "Create Spacer Shape",
              "S3DX", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Plane", "Plane", "Plane where the shape will be inserted",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "Height", "Height of a spacer", GH_ParamAccess.item);
            pManager.AddNumberParameter("Length", "Length", "Length of a spacer", GH_ParamAccess.item);
            pManager.AddNumberParameter("Width", "Width", "Width of a spacer", GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bending Roller", "Bending Roller", "Bending roller", GH_ParamAccess.item);
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
            double length = 0.0;
            double width = 0.0;
            RebarProperties properties = null;
            BendingRoller bendingRoller = null;

            DA.GetData(0, ref insertPlane);
            DA.GetData(1, ref height);
            DA.GetData(2, ref length);
            DA.GetData(3, ref width);
            DA.GetData(4, ref properties);
            DA.GetData(5, ref bendingRoller);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.BuildSpacerShape(insertPlane, height, length, width, bendingRoller);

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
                return Properties.Resources.SpacerShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("736c2d76-1d49-47cb-a3d5-e0f6582a500d"); }
        }
    }
}