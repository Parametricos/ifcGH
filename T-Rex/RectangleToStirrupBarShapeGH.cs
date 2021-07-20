using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class RectangleToStirrupBarShapeGH : GH_Component
    {
        public RectangleToStirrupBarShapeGH()
          : base("Rectangle To Stirrup Shape", "Rectangle To Stirrup Shape",
              "Convert rectangle to Stirrup Shape",
              "S3DX", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddRectangleParameter("Rectangle", "Rectangle", "Boundary rectangle where rebar will be placed",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
            pManager.AddGenericParameter("Bending Roller", "Bending Roller", "Bending roller", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Hooks Type", "Hooks Type", "0 = 90-angle, 1 = 135-angle", GH_ParamAccess.item, 0);
            pManager.AddNumberParameter("Hook Length", "Hook Length", "Length of a hook", GH_ParamAccess.item);
            pManager.AddGenericParameter("Cover Dimensions", "Cover Dimensions", "Dimensions of a concrete cover",
                GH_ParamAccess.item);
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
            int hooksType = 0;
            CoverDimensions coverDimensions = null;
            double hookLength = 0.0;

            DA.GetData(0, ref rectangle);
            DA.GetData(1, ref properties);
            DA.GetData(2, ref bendingRoller);
            DA.GetData(3, ref hooksType);
            DA.GetData(4, ref hookLength);
            DA.GetData(5, ref coverDimensions);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.BuildRectangleToStirrupShape(rectangle, bendingRoller, hooksType, coverDimensions, hookLength);

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
                return Properties.Resources.RectangleToStirrupBarShape;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("be55eb7e-f884-41bc-afa7-780178e623ea"); }
        }
    }
}