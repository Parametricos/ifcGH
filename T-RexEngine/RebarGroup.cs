using System;
using System.Collections.Generic;
using System.Globalization;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.GeometricModelResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.Ifc4.SharedComponentElements;

namespace T_RexEngine
{
    public class RebarGroup : ElementGroup
    {
        private int _id;

        public RebarGroup(int id, RebarSpacing rebarSpacing)
        {
            Id = id;
            OriginRebarShape = rebarSpacing.OriginRebarShape;
            Amount = rebarSpacing.Count;
            Volume = rebarSpacing.Volume;
            Mass = rebarSpacing.Weight;
            RebarGroupMesh = rebarSpacing.RebarGroupMesh;
            RebarGroupCurves = rebarSpacing.RebarGroupCurves;
            RebarInsertPlanes = rebarSpacing.RebarInsertPlanes;
            Diameter = rebarSpacing.OriginRebarShape.Props.Diameter;
            Material = rebarSpacing.OriginRebarShape.Props.Material;
            ElementType = ElementType.Rebar;
            SpacingType = RebarSpacingType.NormalSpacing;
        }
        public RebarGroup(int id, List<RebarShape> rebarShapes)
        {
            Id = id;
            Amount = rebarShapes.Count;
            RebarGroupMesh = new List<Mesh>();
            RebarGroupCurves = new List<Curve>();
            Volume = 0.0;
            Mass = 0.0;
            Diameter = rebarShapes[0].Props.Diameter;
            Material = rebarShapes[0].Props.Material;
            ElementType = ElementType.Rebar;
            SpacingType = RebarSpacingType.CustomSpacing;

            foreach (var rebarShape in rebarShapes)
            {
                if (Material.ToString() != rebarShape.Props.Material.ToString())
                {
                    throw new ArgumentException("You can't add bars with different materials to one group");
                }
                if (Diameter.ToString(CultureInfo.InvariantCulture) != rebarShape.Props.Diameter.ToString(CultureInfo.InvariantCulture))
                {
                    throw new ArgumentException("You can't add bars with different diameters to one group");
                }

                RebarGroupMesh.Add(rebarShape.RebarMesh);
                RebarGroupCurves.Add(rebarShape.RebarCurve);

                double currentRebarVolume = rebarShape.RebarCurve.GetLength() * Math.PI * Math.Pow(rebarShape.Props.Radius, 2.0);

                Volume += currentRebarVolume;
                Mass += currentRebarVolume * rebarShape.Props.Material.Density;
            }
        }
        public override string ToString()
        {
            return String.Format("Rebar Group{0}" +
                                 "Id: {1}{0}" +
                                 "Count: {2}",
                Environment.NewLine, Id, Amount);
        }
        public int Id
        {
            get { return _id; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id can't be < 0");
                }

                _id = value;
            }
        }
        public override List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model)
        {
            throw new ArgumentException("Rebars should be converted to IfcReinforcingElement");
        }

        public override List<IfcElementComponent> ToElementComponentIfc(IfcStore model)
        {
            throw new ArgumentException("Rebars should be converted to IfcReinforcingElement");
        }

        public override List<IfcDistributionElement> ToDistributionElementIfc(IfcStore model)
        {
            throw new ArgumentException("Rebars should be converted to IfcReinforcingElement");
        }

        public override List<IfcProxy> ToProxyIfc(IfcStore model)
        {
            throw new ArgumentException("Rebars should be converted to IfcReinforcingElement");
        }

        public override List<IfcElement> ToElementIfc(IfcStore model)
        {
            throw new ArgumentException("Rebars should be converted to IfcReinforcingElement");
        }

        public override List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model)
        {
            using (var transaction = model.BeginTransaction("Create Mesh Element"))
            {
                var rebars = new List<IfcReinforcingElement>();

                switch (SpacingType)
                {
                    case RebarSpacingType.NormalSpacing:
                        {
                            MeshFaceList faces = OriginRebarShape.RebarMesh.Faces;
                            MeshVertexList vertices = OriginRebarShape.RebarMesh.Vertices;
                            List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                            IfcFaceBasedSurfaceModel faceBasedSurfaceModel =
                                IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                            var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                            shape.Items.Add(faceBasedSurfaceModel);
                            var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);

                            foreach (var insertPlane in RebarInsertPlanes)
                            {
                                var rebar = model.Instances.New<IfcReinforcingBar>();
                                rebar.Name = "Rebar";
                                rebar.NominalDiameter = Diameter;
                                rebar.BarLength = (int)Math.Round(OriginRebarShape.RebarCurve.GetLength());
                                rebar.SteelGrade = Material.Grade;

                                IfcTools.ApplyRepresentationAndPlacement(model, rebar, shape, insertPlane);

                                ifcRelAssociatesMaterial.RelatedObjects.Add(rebar);

                                rebars.Add(rebar);
                            }

                            break;
                        }
                    case RebarSpacingType.CustomSpacing:

                        for (int i = 0; i < RebarGroupMesh.Count; i++)
                        {
                            MeshFaceList faces = RebarGroupMesh[i].Faces;
                            MeshVertexList vertices = RebarGroupMesh[i].Vertices;
                            List<IfcCartesianPoint> ifcVertices = IfcTools.VerticesToIfcCartesianPoints(model, vertices);
                            IfcFaceBasedSurfaceModel faceBasedSurfaceModel =
                                IfcTools.CreateIfcFaceBasedSurfaceModel(model, faces, ifcVertices);
                            var shape = IfcTools.CreateIfcShapeRepresentation(model, "Mesh");
                            shape.Items.Add(faceBasedSurfaceModel);
                            var ifcRelAssociatesMaterial = IfcTools.CreateIfcRelAssociatesMaterial(model, Material.Name, Material.Grade);

                            var rebar = model.Instances.New<IfcReinforcingBar>();
                            rebar.Name = "Rebar";
                            rebar.NominalDiameter = Diameter;
                            rebar.BarLength = (int)Math.Round(RebarGroupCurves[i].GetLength());
                            rebar.SteelGrade = Material.Grade;

                            IfcTools.ApplyRepresentationAndPlacement(model, rebar, shape, Plane.WorldXY);

                            ifcRelAssociatesMaterial.RelatedObjects.Add(rebar);

                            rebars.Add(rebar);
                        }

                        break;
                    default:
                        throw new ArgumentException("Spacing type not recognized");
                }


                transaction.Commit();

                return rebars;
            }
        }
        public List<Mesh> RebarGroupMesh { get; }
        public List<Curve> RebarGroupCurves { get; }
        private List<Plane> RebarInsertPlanes { get; }
        private RebarShape OriginRebarShape { get; }
        public double Diameter { get; }
        public RebarSpacingType SpacingType { get; }
    }
}
