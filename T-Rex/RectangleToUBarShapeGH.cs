using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class RectangleToUBarShapeGH : GH_Component
    {
        public RectangleToUBarShapeGH()
          : base("Rectangle To U-Bar Shape", "Rectangle To U-Bar Shape",
              "Convert rectangle to U-Bar Shape",
              "S3DX", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddRectangleParameter("Rectangle", "Rectangle", "Boundary rectangle where rebar will be placed",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bending Roller", "Bending Roller", "Bending roller", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Position", "Position", "0 = top, 1 = right, 2 = bottom, 3 = left",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Cover Dimensions", "Cover Dimensions", "Dimensions of a concrete cover",
                GH_ParamAccess.item);
            pManager.AddNumberParameter("Hook Length", "Hook Length", "Length of a hook", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Rebar Shape", "Rebar Shape", "Reinforcement bar shape", GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh that represents reinforcement", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Rectangle3d rectangle = Rectangle3d.Unset;
            RebarProperties properties = null;
            BendingRoller bendingRoller = null;
            int position = 0;
            CoverDimensions coverDimensions = null;
            double hookLength = 0.0;

            DA.GetData(0, ref rectangle);
            DA.GetData(1, ref properties);
            DA.GetData(2, ref bendingRoller);
            DA.GetData(3, ref position);
            DA.GetData(4, ref coverDimensions);
            DA.GetData(5, ref hookLength);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.BuildRectangleToUBarShape(rectangle, bendingRoller, position, coverDimensions, hookLength);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.RectangleToUBarShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("c6b580c3-f8e2-4819-9863-40e8f5382881"); }
        }
    }
}