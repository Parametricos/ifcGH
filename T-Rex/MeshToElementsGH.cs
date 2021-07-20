using System.Collections.Generic;
using T_RexEngine;
using T_RexEngine.ElementLibrary;
using System;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace T_Rex
{
    public class MeshToElementsGH : GH_Component
    {
        public MeshToElementsGH()
          : base("Mesh To Elements", "Mesh To Elements",
              "Create Elements from Mesh",
              "S3DX", "Concrete")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Name of the elements", GH_ParamAccess.item);
            pManager.AddMeshParameter("Mesh", "Mesh", "Mesh representation of model", GH_ParamAccess.item);
            pManager.AddGenericParameter("Material", "Material", "Concrete element material", GH_ParamAccess.item);
            pManager.AddTextParameter("MainType", "MainType", "Main Element type as text", GH_ParamAccess.item);
            pManager.AddTextParameter("SubType", "SubType", "Sub Element type as text", GH_ParamAccess.item, "notdefined");
            pManager.AddPlaneParameter("Insert Planes", "Insert Planes", "Destination planes of an element",
                GH_ParamAccess.list);

        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element Group", "Element Group", "Concrete elements", GH_ParamAccess.item);
            pManager.AddMeshParameter("Meshes", "Meshes", "Meshes that represent concrete elements", GH_ParamAccess.list);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            string name = String.Empty;
            Mesh mesh = null;
            Material material = null;
            List<Plane> insertPlanes = new List<Plane>();
            string maintype = "door";
            string subtype = "notdefined";

            DA.GetData(0, ref name);
            DA.GetData(1, ref mesh);
            DA.GetData(2, ref material);
            DA.GetData(3, ref maintype);
            DA.GetData(4, ref subtype);
            DA.GetDataList(5, insertPlanes);

            string maintype_small = maintype.ToLower();
            string subtype_small = subtype.ToLower();

            MeshToElements customElements = new MeshToElements(name, mesh, material, maintype_small, subtype_small, insertPlanes);

            DA.SetData(0, customElements);
            DA.SetDataList(1, customElements.ResultMesh);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.Mesh;
            }
        }
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("28fa0471-d268-4d92-80f1-f19daf1ee718"); }
        }
    }
}