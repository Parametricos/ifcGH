using System;
using System.Collections.Generic;
using System.Linq;
using T_RexEngine.Enums;
using Xbim.Common;
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.Ifc4.GeometricConstraintResource;
using Xbim.Ifc4.GeometryResource;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.IO;
using Xbim.Ifc4.SharedComponentElements;

namespace T_RexEngine
{
    public class Ifc
    {
        public Ifc(List<ElementGroup> elementGroups, string projectName, string buildingName, string path)
        {
            using (IfcStore model = CreateAndInitModel(projectName))
            {
                if (model != null)
                {
                    IfcBuilding building = CreateBuilding(model, buildingName);

                    foreach (var elementGroup in elementGroups)
                    {
                        switch (elementGroup.ElementType)
                        {
                            case ElementType.PadFooting:
                            case ElementType.StripFooting:
                            case ElementType.Beam:
                            case ElementType.Beam_Joist:
                            case ElementType.Beam_Hollowcore:
                            case ElementType.Beam_Lintel:
                            case ElementType.Beam_Spandrel:
                            case ElementType.Beam_T_Beam:
                            case ElementType.Beam_Userdefined:
                            case ElementType.Beam_Notdefined:
                            case ElementType.Column:
                            case ElementType.Column_Pilaster:
                            case ElementType.Column_Userdefined:
                            case ElementType.Column_Notdefined:
                            case ElementType.Door:
                            case ElementType.Door_Gate:
                            case ElementType.Door_Trapdoor:
                            case ElementType.Door_Userdefined:
                            case ElementType.Door_Notdefined:
                            case ElementType.Stair:
                            case ElementType.Stair_StraightRunStair:
                            case ElementType.Stair_TwoStraightRunStair:
                            case ElementType.Stair_QuarterWindingStair:
                            case ElementType.Stair_QuarterTurnStair:
                            case ElementType.Stair_HalfWindingStair:
                            case ElementType.Stair_HalfTurnStair:
                            case ElementType.Stair_TwoQuarterWindingStair:
                            case ElementType.Stair_TwoQuarterTurnStair:
                            case ElementType.Stair_ThreeQuarterWindingStair:
                            case ElementType.Stair_ThreeQuarterTurnStair:
                            case ElementType.Stair_SpiralStair:
                            case ElementType.Stair_DoubleReturnStair:
                            case ElementType.Stair_CurvedRunStair:
                            case ElementType.Stair_TwoCurvedRunStair:
                            case ElementType.Stair_Userdefined:
                            case ElementType.Stair_Notdefined:
                            case ElementType.Covering_Ceiling:
                            case ElementType.Covering_Cladding:
                            case ElementType.Covering_Flooring:
                            case ElementType.Covering_Insulation:
                            case ElementType.Covering_Membrane:
                            case ElementType.Covering_Molding:
                            case ElementType.Covering_Notdefined:
                            case ElementType.Covering_Roofing:
                            case ElementType.Covering_Skirtingboard:
                            case ElementType.Covering_Sleeving:
                            case ElementType.Covering_Userdefined:
                            case ElementType.Covering_Wrapping:
                            case ElementType.CurtainWall_Userdefined:
                            case ElementType.CurtainWall_Notdefined:
                            case ElementType.Footing_Caisson_Foundation:
                            case ElementType.Footing_Footing_Beam:
                            case ElementType.Footing_Notdefined:
                            case ElementType.Footing_Pad_Footing:
                            case ElementType.Footing_Pile_Cap:
                            case ElementType.Footing_Strip_Footing:
                            case ElementType.Footing_Userdefined:
                            case ElementType.Member_Brace:
                            case ElementType.Member_Chord:
                            case ElementType.Member_Collar:
                            case ElementType.Member_Member:
                            case ElementType.Member_Mullion:
                            case ElementType.Member_Notdefined:
                            case ElementType.Member_Plate:
                            case ElementType.Member_Post:
                            case ElementType.Member_Purlin:
                            case ElementType.Member_Rafter:
                            case ElementType.Member_Stringer:
                            case ElementType.Member_Strut:
                            case ElementType.Member_Stud:
                            case ElementType.Member_Userdefined:
                            case ElementType.Pile_Bored:
                            case ElementType.Pile_Cohesion:
                            case ElementType.Pile_Driven:
                            case ElementType.Pile_Friction:
                            case ElementType.Pile_Jetgrouting:
                            case ElementType.Pile_Notdefined:
                            case ElementType.Pile_Support:
                            case ElementType.Pile_Userdefined:
                            case ElementType.Plate_Curtain_Panel:
                            case ElementType.Plate_Sheet:
                            case ElementType.Plate_Userdefined:
                            case ElementType.Plate_Notdefined:
                            case ElementType.Railing_Handrail:
                            case ElementType.Railing_Guardrail:
                            case ElementType.Railing_Balustrade:
                            case ElementType.Railing_Userdefined:
                            case ElementType.Railing_Notdefined:
                            case ElementType.Ramp_HalfTurnRamp:
                            case ElementType.Ramp_Notdefined:
                            case ElementType.Ramp_QuarterTurnRamp:
                            case ElementType.Ramp_SpiralRamp:
                            case ElementType.Ramp_StraightRunRamp:
                            case ElementType.Ramp_TwoQuarterTurnRamp:
                            case ElementType.Ramp_TwoStraightRunRamp:
                            case ElementType.Ramp_Userdefined:
                            case ElementType.RampFlight:
                            case ElementType.RampFlight_Straight:
                            case ElementType.RampFlight_Spiral:
                            case ElementType.RampFlight_Userdefined:
                            case ElementType.RampFlight_Notdefined:
                            case ElementType.Roof_BarrelRoof:
                            case ElementType.Roof_ButterflyRoof:
                            case ElementType.Roof_DomeRoof:
                            case ElementType.Roof_Flatroof:
                            case ElementType.Roof_Freeform:
                            case ElementType.Roof_Gableroof:
                            case ElementType.Roof_GambrelRoof:
                            case ElementType.Roof_HippedGableRoof:
                            case ElementType.Roof_Hiproof:
                            case ElementType.Roof_MansardRoof:
                            case ElementType.Roof_Notdefined:
                            case ElementType.Roof_PavilionRoof:
                            case ElementType.Roof_RainbowRoof:
                            case ElementType.Roof_Shedroof:
                            case ElementType.Roof_Userdefined:
                            case ElementType.Slab:
                            case ElementType.Slab_Floor:
                            case ElementType.Slab_Roof:
                            case ElementType.Slab_Landing:
                            case ElementType.Slab_Baseslab:
                            case ElementType.Slab_Userdefined:
                            case ElementType.Slab_Notdefined:
                            case ElementType.StairFlight_Curved:
                            case ElementType.StairFlight_Freeform:
                            case ElementType.StairFlight_Notdefined:
                            case ElementType.StairFlight_Spiral:
                            case ElementType.StairFlight_Straight:
                            case ElementType.StairFlight_Userdefined:
                            case ElementType.StairFlight_Winder:
                            case ElementType.Wall:
                            case ElementType.Wall_Elementedwall:
                            case ElementType.Wall_Movable:
                            case ElementType.Wall_Notdefined:
                            case ElementType.Wall_Parapet:
                            case ElementType.Wall_Partitioning:
                            case ElementType.Wall_Polygonal:
                            case ElementType.Wall_Shear:
                            case ElementType.Wall_Solidwall:
                            case ElementType.Wall_Standard:
                            case ElementType.Wall_Userdefined:
                            case ElementType.Wall_Plumbingwall:
                            case ElementType.Window:
                            case ElementType.Window_Skylight:
                            case ElementType.Window_Lightdome:
                            case ElementType.Window_Userdefined:
                            case ElementType.Window_Notdefined:
                            case ElementType.BuildingElementProxy_Complex:
                            case ElementType.BuildingElementProxy_Element:
                            case ElementType.BuildingElementProxy_Notdefined:
                            case ElementType.BuildingElementProxy_Partial:
                            case ElementType.BuildingElementProxy_ProvisionForVoid:
                            case ElementType.BuildingElementProxy_Userdefined:
                                {
                                    List<IfcBuildingElement> currentElementGroup = elementGroup.ToBuildingElementIfc(model);
                                    foreach (var buildingElement in currentElementGroup)
                                    {
                                        using (var transaction = model.BeginTransaction("Add element"))
                                        {
                                            building.AddElement(buildingElement);
                                            transaction.Commit();
                                        }
                                    }

                                    break;
                                }
                            case ElementType.Proxy:
                                {
                                    List<IfcProxy> currentElementGroup = elementGroup.ToProxyIfc(model);
                                    foreach (var proxy in currentElementGroup)
                                    {
                                        using (var transaction = model.BeginTransaction("Add element"))
                                        {
                                            building.AddElement(proxy);
                                            transaction.Commit();
                                        }
                                    }

                                    break;
                                }
                            case ElementType.ReinforcingBar_Anchoring:
                            case ElementType.ReinforcingBar_Edge:
                            case ElementType.ReinforcingBar_Ligature:
                            case ElementType.ReinforcingBar_Main:
                            case ElementType.ReinforcingBar_Notdefined:
                            case ElementType.ReinforcingBar_Punching:
                            case ElementType.ReinforcingBar_Ring:
                            case ElementType.ReinforcingBar_Shear:
                            case ElementType.ReinforcingBar_Stud:
                            case ElementType.ReinforcingBar_Userdefined:
                            case ElementType.Reinforcingmesh_Notdefined:
                            case ElementType.Reinforcingmesh_Userdefined:
                            case ElementType.Tendon_Bar:
                            case ElementType.Tendon_Coated:
                            case ElementType.Tendon_Notdefined:
                            case ElementType.Tendon_Strand:
                            case ElementType.Tendon_Userdefined:
                            case ElementType.Tendon_Wire:
                            case ElementType.TendonAnchor_Coupler:
                            case ElementType.TendonAnchor_FixedEnd:
                            case ElementType.TendonAnchor_Notdefined:
                            case ElementType.TendonAnchor_TensioningEnd:
                            case ElementType.TendonAnchor_Userdefined:
                                {
                                    List<IfcReinforcingElement> currentElementGroup = elementGroup.ToReinforcingElementIfc(model);
                                    foreach (var reinforcingElement in currentElementGroup)
                                    {
                                        using (var transaction = model.BeginTransaction("Add element"))
                                        {
                                            building.AddElement(reinforcingElement);
                                            transaction.Commit();
                                        }
                                    }

                                    break;
                                }
                            case ElementType.BuildingElementPart_Insulation:
                            case ElementType.BuildingElementPart_Notdefined:
                            case ElementType.BuildingElementPart_Precastpanel:
                            case ElementType.BuildingElementPart_Userdefined:
                            case ElementType.MechanicalFastener_AnchorBolt:
                            case ElementType.MechanicalFastener_Bolt:
                            case ElementType.MechanicalFastener_Dowel:
                            case ElementType.MechanicalFastener_Nail:
                            case ElementType.MechanicalFastener_NailPlate:
                            case ElementType.MechanicalFastener_Notdefined:
                            case ElementType.MechanicalFastener_Rivet:
                            case ElementType.MechanicalFastener_Screw:
                            case ElementType.MechanicalFastener_ShearConnector:
                            case ElementType.MechanicalFastener_Staple:
                            case ElementType.MechanicalFastener_StudShearConnector:
                            case ElementType.MechanicalFastener_Userdefined:
                            case ElementType.Fastener_Glue:
                            case ElementType.Fastener_Mortar:
                            case ElementType.Fastener_Notdefined:
                            case ElementType.Fastener_Userdefined:
                            case ElementType.Fastener_Weld:
                            case ElementType.DiscreteAccessory_Anchorplate:
                            case ElementType.DiscreteAccessory_Bracket:
                            case ElementType.DiscreteAccessory_Notdefined:
                            case ElementType.DiscreteAccessory_Shoe:
                            case ElementType.DiscreteAccessory_Userdefined:
                                {
                                    List<IfcElementComponent> currentElementGroup = elementGroup.ToElementComponentIfc(model);
                                    foreach (var elementComponent in currentElementGroup)
                                    {
                                        using (var transaction = model.BeginTransaction("Add element"))
                                        {
                                            building.AddElement(elementComponent);
                                            transaction.Commit();
                                        }
                                    }

                                    break;
                                }
                            case ElementType.DistributionElement:
                            case ElementType.DistributionControlElement:
                            case ElementType.DistributionFlowElement:
                            case ElementType.DistributionChamberElement_FormedDuct:
                            case ElementType.DistributionChamberElement_InspectionChamber:
                            case ElementType.DistributionChamberElement_InspectionPit:
                            case ElementType.DistributionChamberElement_Manhole:
                            case ElementType.DistributionChamberElement_MeterChamber:
                            case ElementType.DistributionChamberElement_Notdefined:
                            case ElementType.DistributionChamberElement_Sump:
                            case ElementType.DistributionChamberElement_Trench:
                            case ElementType.DistributionChamberElement_Userdefined:
                            case ElementType.DistributionChamberElement_ValveChamber:
                            case ElementType.EnergyConversionDevice:
                            case ElementType.FlowController:
                            case ElementType.FlowFitting:
                            case ElementType.FlowMovingDevice:
                            case ElementType.FlowSegment:
                            case ElementType.FlowStorageDevice:
                            case ElementType.FlowTerminal:
                            case ElementType.FlowTreatmentDevice:
                                {
                                    List<IfcDistributionElement> currentElementGroup = elementGroup.ToDistributionElementIfc(model);
                                    foreach (var distributionElement in currentElementGroup)
                                    {
                                        using (var transaction = model.BeginTransaction("Add element"))
                                        {
                                            building.AddElement(distributionElement);
                                            transaction.Commit();
                                        }
                                    }

                                    break;
                                }
                            case ElementType.ElementAssembly_AccessoryAssembly:
                            case ElementType.ElementAssembly_Arch:
                            case ElementType.ElementAssembly_BeamGrid:
                            case ElementType.ElementAssembly_BracedFrame:
                            case ElementType.ElementAssembly_Girder:
                            case ElementType.ElementAssembly_Notdefined:
                            case ElementType.ElementAssembly_ReinforcementUnit:
                            case ElementType.ElementAssembly_RigidFrame:
                            case ElementType.ElementAssembly_SlabField:
                            case ElementType.ElementAssembly_Truss:
                            case ElementType.ElementAssembly_Userdefined:
                            case ElementType.FurnishingElement:
                            case ElementType.TransportElement_Craneway:
                            case ElementType.TransportElement_Elevator:
                            case ElementType.TransportElement_Escalator:
                            case ElementType.TransportElement_LiftingGear:
                            case ElementType.TransportElement_MovingWalkWay:
                            case ElementType.TransportElement_Notdefined:
                            case ElementType.TransportElement_Userdefined:
                            case ElementType.VirtualElement:
                                {
                                    List<IfcElement> currentElementGroup = elementGroup.ToElementIfc(model);
                                    foreach (var distributionElement in currentElementGroup)
                                    {
                                        using (var transaction = model.BeginTransaction("Add element"))
                                        {
                                            building.AddElement(distributionElement);
                                            transaction.Commit();
                                        }
                                    }

                                    break;
                                }
                            default:
                                throw new ArgumentException("Unknown element type=>ifc.cs");
                        }
                    }

                    if (path.Substring(path.Length - 4) != ".ifc")
                    {
                        throw new ArgumentException("Path should end up with .ifc");
                    }

                    try
                    {
                        model.SaveAs(path, StorageType.Ifc);
                    }
                    catch (Exception e)
                    {
                        throw new ArgumentException("Couldn't save the file. " + e);
                    }
                }
            }
        }

        private static IfcStore CreateAndInitModel(string projectName)
        {
            var credentials = new XbimEditorCredentials
            {
                ApplicationDevelopersName = "xbim developer",
                ApplicationFullName = "T-Rex",
                ApplicationIdentifier = "T-Rex",
                ApplicationVersion = "0.2",
                EditorsFamilyName = "Team",
                EditorsGivenName = "xbim",
                EditorsOrganisationName = "xbim developer"
            };

            var model = IfcStore.Create(credentials, XbimSchemaVersion.Ifc4x1, XbimStoreType.InMemoryModel);

            using (ITransaction transaction = model.BeginTransaction("Initialise Model"))
            {
                IfcProject project = model.Instances.New<IfcProject>();
                project.Initialize(ProjectUnits.SIUnitsUK);
                project.Name = projectName;
                transaction.Commit();
            }

            return model;
        }

        private static IfcBuilding CreateBuilding(IfcStore model, string buildingName)
        {
            using (ITransaction transaction = model.BeginTransaction("Create Building"))
            {
                IfcBuilding building = model.Instances.New<IfcBuilding>();
                building.Name = buildingName;
                building.CompositionType = IfcElementCompositionEnum.ELEMENT;

                IfcLocalPlacement localPlacement = model.Instances.New<IfcLocalPlacement>();
                IfcAxis2Placement3D placement = model.Instances.New<IfcAxis2Placement3D>();

                localPlacement.RelativePlacement = placement;
                placement.Location = model.Instances.New<IfcCartesianPoint>(p => p.SetXYZ(0, 0, 0));

                IfcProject project = model.Instances.OfType<IfcProject>().FirstOrDefault();
                project?.AddBuilding(building);
                transaction.Commit();

                return building;
            }
        }
    }
}