using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;
using System;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino;
using Rhino.Geometry;
using T_RexEngine.Enums;

namespace T_Rex
{
    public class ProfileToElementsGH : GH_Component
    {
        private bool _useDegrees = false;
        public ProfileToElementsGH()
          : base("Profile To Elements", "Profile To Elements",
              "Create Elements from Profile",
              "S3DX", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Name of the elements", GH_ParamAccess.item);
            pManager.AddGenericParameter("Profile", "Profile", "Profile to create element from", GH_ParamAccess.item);
            pManager.AddAngleParameter("Rotation Angle", "Rotation Angle", "Set rotation angle for the profile",
                GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
            pManager.AddTextParameter("MainType", "MainType", "Main Element type as text", GH_ParamAccess.item);
            pManager.AddTextParameter("SubType", "SubType", "Sub Element type as text", GH_ParamAccess.item, "notdefined");
            pManager.AddLineParameter("Insert Lines", "Insert Lines", "Lines to specify the element length and position",
                GH_ParamAccess.list);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element Group", "Element Group", "Concrete elements", GH_ParamAccess.item);
            pManager.AddBrepParameter("Breps", "Breps", "Breps that represent concrete elements", GH_ParamAccess.list);
        }

        protected override void BeforeSolveInstance()
        {
            base.BeforeSolveInstance();
            _useDegrees = false;
            if (Params.Input[2] is Param_Number angleParameter)
                _useDegrees = angleParameter.UseDegrees;
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = String.Empty;
            Profile profile = null;
            List<Line> lines = new List<Line>();
            double angle = 0.0;
            Material material = null;
            string maintype = "door";
            string subtype = "notdefined";

            DA.GetData(0, ref name);
            DA.GetData(1, ref profile);
            if (!DA.GetData(2, ref angle)) return;
            if (_useDegrees)
                angle = RhinoMath.ToRadians(angle);
            DA.GetData(3, ref material);
            DA.GetData(4, ref maintype);
            DA.GetData(5, ref subtype);
            DA.GetDataList(6, lines);

            string maintype_small = maintype.ToLower();
            string subtype_small = subtype.ToLower();

            ProfileToElements profileToElements = new ProfileToElements(name, profile, lines, angle, material, maintype_small, subtype_small);

            DA.SetData(0, profileToElements);
            DA.SetDataList(1, profileToElements.Breps);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.ProfileElements;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("e2f01d42-0a76-4636-afa4-d31218aa0898"); }
        }
    }
}