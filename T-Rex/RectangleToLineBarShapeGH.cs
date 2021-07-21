using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class RectangleToLineBarShapeGH : GH_Component
    {
        public RectangleToLineBarShapeGH()
          : base("Rectangle To Line Bar Shape", "Rectangle To Line Bar Shape",
              "Convert rectangle to Line Bar Shape",
              "S3DX", "Rebar Shape")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddRectangleParameter("Rectangle", "Rectangle", "Boundary rectangle where rebar will be placed",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Properties", "Properties", "Reinforcement properties", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Position", "Position", "0 = top, 1 = right, 2 = bottom, 3 = left",
                GH_ParamAccess.item);
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
            int position = 0;
            CoverDimensions coverDimensions = null;

            DA.GetData(0, ref rectangle);
            DA.GetData(1, ref properties);
            DA.GetData(2, ref position);
            DA.GetData(3, ref coverDimensions);

            RebarShape rebarShape = new RebarShape(properties);
            rebarShape.BuildRectangleToLineBarShape(rectangle, position, coverDimensions);

            DA.SetData(0, rebarShape);
            DA.SetData(1, rebarShape.RebarMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.RectangleToBarLineShape;
            }
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("6739e03c-4809-4745-9b84-61a3d76416ad"); }
        }
    }
}