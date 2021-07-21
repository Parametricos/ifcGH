using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class RebarPropertiesGH : GH_Component
    {
        public RebarPropertiesGH()
          : base("Rebar Properties", "Rebar Properties",
              "Properties for a reinforcement bar",
              "S3DX", "Properties")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Diameter", "Diameter", "Diameter of the bar", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Material of the rebar", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Properties", "Properties", "Properties of the bar", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            double diameter = 0.0;
            Material material = null;

            DA.GetData(0, ref diameter);
            DA.GetData(1, ref material);

            RebarProperties prop = new RebarProperties(diameter, material);

            DA.SetData(0, prop);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.RebarProp;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("8111f73d-c991-41e0-b338-f44cacaafc06"); }
        }
    }
}