using System;
using System.Collections.Generic;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using Xbim.Ifc;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.Ifc4.SharedComponentElements;


namespace T_RexEngine.ElementLibrary
{
    public class MeshToElements : ElementGroup
    {
        private Mesh _mesh;
        public MeshToElements(string name, Mesh mesh, Material material, string mainType, string subType, List<Plane> insertPlanes)
        {
            Name = name;
            Mesh = mesh;
            Material = material;
            Amount = insertPlanes.Count;
            Volume = VolumeMassProperties.Compute(mesh).Volume * Amount;
            Mass = Volume * material.Density;
            InsertPlanes = insertPlanes;
            ElementType = IfcTools.StringToType(mainType, subType);
            ResultMesh = new List<Mesh>();

            foreach (var plane in InsertPlanes)
            {
                Mesh duplicateMesh = Mesh.DuplicateMesh();
                Transform planeToPlane = Transform.PlaneToPlane(Plane.WorldXY, plane);
                duplicateMesh.Transform(planeToPlane);
                ResultMesh.Add(duplicateMesh);
            }
        }

        public override string ToString()
        {
            return $"Element Group{Environment.NewLine}" + $"Count: {Amount}";
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = Mesh.Faces;
                MeshVertexList vertices = Mesh.Vertices;
                List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                IfcFaceBasedSurfaceModel faceBasedSurfaceModel = IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                shape.Items.Add(faceBasedSurfaceModel);
                var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);
                var reinforcingElements = IfcTools.CreateReinforcingElements(model, ElementType, Name, shape, InsertPlanes,
                    ifcRelAssociatesMaterial);

                transaction.Commit();

                return reinforcingElements;
            }
        }

        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = Mesh.Faces;
                MeshVertexList vertices = Mesh.Vertices;
                List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                IfcFaceBasedSurfaceModel faceBasedSurfaceModel = IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                shape.Items.Add(faceBasedSurfaceModel);
                var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);
                var buildingElements = IfcTools.CreateBuildingElements(model, ElementType, Name, shape, InsertPlanes,
                    ifcRelAssociatesMaterial);

                transaction.Commit();

                return buildingElements;
            }
        }

        public override List<IfcProxy> ToProxyIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = Mesh.Faces;
                MeshVertexList vertices = Mesh.Vertices;
                List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                IfcFaceBasedSurfaceModel faceBasedSurfaceModel = IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                shape.Items.Add(faceBasedSurfaceModel);
                var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);
                var proxy = IfcTools.CreateProxy(model, ElementType, Name, shape, InsertPlanes,
                    ifcRelAssociatesMaterial);

                transaction.Commit();

                return proxy;
            }
        }

        public override List<IfcElement> ToElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = Mesh.Faces;
                MeshVertexList vertices = Mesh.Vertices;
                List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                IfcFaceBasedSurfaceModel faceBasedSurfaceModel = IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                shape.Items.Add(faceBasedSurfaceModel);
                var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);
                var element = IfcTools.CreateElement(model, ElementType, Name, shape, InsertPlanes,
                    ifcRelAssociatesMaterial);

                transaction.Commit();

                return element;
            }
        }

        public override List<IfcElementComponent> ToElementComponentIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = Mesh.Faces;
                MeshVertexList vertices = Mesh.Vertices;
                List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                IfcFaceBasedSurfaceModel faceBasedSurfaceModel = IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                shape.Items.Add(faceBasedSurfaceModel);
                var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);
                var elementComponents = IfcTools.CreateElementComponent(model, ElementType, Name, shape, InsertPlanes,
                    ifcRelAssociatesMaterial);

                transaction.Commit();

                return elementComponents;
            }
        }

        public override List<IfcDistributionElement> ToDistributionElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                MeshFaceList faces = Mesh.Faces;
                MeshVertexList vertices = Mesh.Vertices;
                List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                IfcFaceBasedSurfaceModel faceBasedSurfaceModel = IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                shape.Items.Add(faceBasedSurfaceModel);
                var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);
                var elementComponents = IfcTools.CreateDistributionElement(model, ElementType, Name, shape, InsertPlanes,
                    ifcRelAssociatesMaterial);

                transaction.Commit();

                return elementComponents;
            }
        }

        public string Name { get; }

        public Mesh Mesh
        {
            get { return _mesh; }
            private set
            {
                if (!value.IsClosed)
                {
                    throw new ArgumentException("Mesh should be closed");
                }
                _mesh = value;
            }
        }

        public List<Mesh> ResultMesh { get; }
        public List<Plane> InsertPlanes { get; }

    }
}
