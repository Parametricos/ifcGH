using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using T_RexEngine;

namespace T_Rex
{
    public class MaterialGH : GH_Component
    {
        public MaterialGH()
          : base("Material", "Material",
              "Creates material of the element",
              "S3DX", "Properties")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Name of the material", GH_ParamAccess.item, "Name");
            pManager.AddTextParameter("Grade", "Grade", "Grade of the material", GH_ParamAccess.item, "Grade");
            pManager.AddNumberParameter("Density", "Density", "Density of the material, unit-less." +
                                        " This density will be multiplied by the volume of rebars to calculate the weight.", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Material", "Material", "Created material", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = string.Empty;
            string grade = string.Empty;
            double density = double.NaN;

            DA.GetData(0, ref name);
            DA.GetData(1, ref grade);
            DA.GetData(2, ref density);

            Material material = new Material(name, grade, density);

            DA.SetData(0, material);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Material;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("a0297085-65eb-4e61-8eec-046d101cc4c5"); }
        }
    }
}