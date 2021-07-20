namespace T_RexEngine.Enums
{
    public enum ElementType
    {
        PadFooting,                 // => IfcBuildingElement
        StripFooting,               // => IfcBuildingElement

        Beam,                       // => IfcBuildingElement
        Beam_Joist, Beam_Hollowcore, Beam_Lintel, Beam_Spandrel, Beam_T_Beam, Beam_Userdefined, Beam_Notdefined,

        Column,                     // => IfcBuildingElement
        Column_Pilaster, Column_Userdefined, Column_Notdefined,

        Rebar,                      // using ReinforcingBar instead -> but used in other non IFC files by original projects
        Proxy,                      // => IfcProxy

        //Covering,                   // => IfcBuildingElement
        Covering_Ceiling, Covering_Flooring, Covering_Cladding, Covering_Roofing, Covering_Molding, Covering_Skirtingboard, Covering_Insulation, Covering_Membrane, Covering_Sleeving, Covering_Wrapping, Covering_Userdefined, Covering_Notdefined,

        //CurtainWall,                // => IfcBuildingElement
        CurtainWall_Userdefined, CurtainWall_Notdefined,

        //Member,                     // => IfcBuildingElement
        Member_Brace, Member_Chord, Member_Collar, Member_Member, Member_Mullion, Member_Plate, Member_Post, Member_Purlin, Member_Rafter, Member_Stringer, Member_Strut, Member_Stud, Member_Userdefined, Member_Notdefined,

        //Footing,                    // => IfcBuildingElement
        Footing_Caisson_Foundation, Footing_Footing_Beam, Footing_Pad_Footing, Footing_Pile_Cap, Footing_Strip_Footing, Footing_Userdefined, Footing_Notdefined,

        //Pile,                       // => IfcBuildingElement
        Pile_Bored, Pile_Driven, Pile_Jetgrouting, Pile_Cohesion, Pile_Friction, Pile_Support, Pile_Userdefined, Pile_Notdefined,

        //Plate,                      // => IfcBuildingElement
        Plate_Curtain_Panel, Plate_Sheet, Plate_Userdefined, Plate_Notdefined,

        //Railing,                    // => IfcBuildingElement
        Railing_Handrail, Railing_Guardrail, Railing_Balustrade, Railing_Userdefined, Railing_Notdefined,

        //Ramp,                       // => IfcBuildingElement
        Ramp_StraightRunRamp, Ramp_TwoStraightRunRamp, Ramp_QuarterTurnRamp, Ramp_TwoQuarterTurnRamp, Ramp_HalfTurnRamp, Ramp_SpiralRamp, Ramp_Userdefined, Ramp_Notdefined,

        RampFlight,                 // => IfcBuildingElement
        RampFlight_Straight, RampFlight_Spiral, RampFlight_Userdefined, RampFlight_Notdefined,

        //Roof,                       // => IfcBuildingElement
        Roof_Flatroof, Roof_Shedroof, Roof_Gableroof, Roof_Hiproof, Roof_HippedGableRoof, Roof_GambrelRoof, Roof_MansardRoof, Roof_BarrelRoof, Roof_RainbowRoof, Roof_ButterflyRoof, Roof_PavilionRoof, Roof_DomeRoof, Roof_Freeform, Roof_Userdefined, Roof_Notdefined,

        Slab,                       // => IfcBuildingElement
        Slab_Floor, Slab_Roof, Slab_Landing, Slab_Baseslab, Slab_Userdefined, Slab_Notdefined,

        //StairFlight,                // => IfcBuildingElement
        StairFlight_Straight, StairFlight_Winder, StairFlight_Spiral, StairFlight_Curved, StairFlight_Freeform, StairFlight_Userdefined, StairFlight_Notdefined,

        Wall,                       // => IfcBuildingElement
        Wall_Movable, Wall_Parapet, Wall_Partitioning, Wall_Plumbingwall, Wall_Shear, Wall_Solidwall, Wall_Standard, Wall_Polygonal, Wall_Elementedwall, Wall_Userdefined, Wall_Notdefined,

        Window,                     // => IfcBuildingElement
        Window_Skylight, Window_Lightdome, Window_Userdefined, Window_Notdefined,

        Door,                       // => IfcBuildingElement
        Door_Gate, Door_Trapdoor, Door_Userdefined, Door_Notdefined,

        Stair,                      // => IfcBuildingElement
        Stair_StraightRunStair, Stair_TwoStraightRunStair, Stair_QuarterWindingStair, Stair_QuarterTurnStair, Stair_HalfWindingStair, Stair_HalfTurnStair, Stair_TwoQuarterWindingStair, Stair_TwoQuarterTurnStair, Stair_ThreeQuarterWindingStair, Stair_ThreeQuarterTurnStair, Stair_SpiralStair, Stair_DoubleReturnStair, Stair_CurvedRunStair, Stair_TwoCurvedRunStair, Stair_Userdefined, Stair_Notdefined,

        //BuildingElementProxy,       // => IfcBuildingElement
        BuildingElementProxy_Complex, BuildingElementProxy_Element, BuildingElementProxy_Partial, BuildingElementProxy_ProvisionForVoid, BuildingElementProxy_Userdefined, BuildingElementProxy_Notdefined,

        //BuildingElementPart,        // => IfcElementComponent
        BuildingElementPart_Insulation, BuildingElementPart_Precastpanel, BuildingElementPart_Userdefined, BuildingElementPart_Notdefined,

        //ReinforcingBar,             // => IfcReinforcingElement 
        ReinforcingBar_Anchoring, ReinforcingBar_Edge, ReinforcingBar_Ligature, ReinforcingBar_Main, ReinforcingBar_Punching, ReinforcingBar_Ring, ReinforcingBar_Shear, ReinforcingBar_Stud, ReinforcingBar_Userdefined, ReinforcingBar_Notdefined,

        //ReinforcingMesh,            // => IfcReinforcingElement
        Reinforcingmesh_Userdefined, Reinforcingmesh_Notdefined,

        //Tendon,                     // => IfcReinforcingElement
        Tendon_Bar, Tendon_Coated, Tendon_Strand, Tendon_Wire, Tendon_Userdefined, Tendon_Notdefined,

        //TendonAnchor,               // => IfcReinforcingElement
        TendonAnchor_Coupler, TendonAnchor_FixedEnd, TendonAnchor_TensioningEnd, TendonAnchor_Userdefined, TendonAnchor_Notdefined,

        DistributionElement,        // => IfcDistributionElement
        DistributionControlElement, // => IfcDistributionElement
        DistributionFlowElement,    // => IfcDistributionElement

        //DistributionChamberElement, // => IfcDistributionElement
        DistributionChamberElement_FormedDuct, DistributionChamberElement_InspectionChamber, DistributionChamberElement_InspectionPit, DistributionChamberElement_Manhole, DistributionChamberElement_MeterChamber, DistributionChamberElement_Sump, DistributionChamberElement_Trench, DistributionChamberElement_ValveChamber, DistributionChamberElement_Userdefined, DistributionChamberElement_Notdefined,

        EnergyConversionDevice,     // => IfcDistributionElement
        FlowController,             // => IfcDistributionElement
        FlowFitting,                // => IfcDistributionElement
        FlowMovingDevice,           // => IfcDistributionElement
        FlowSegment,                // => IfcDistributionElement
        FlowStorageDevice,          // => IfcDistributionElement
        FlowTerminal,               // => IfcDistributionElement
        FlowTreatmentDevice,        // => IfcDistributionElement

        //ElementAssembly,            // => IfcElement
        ElementAssembly_AccessoryAssembly, ElementAssembly_Arch, ElementAssembly_BeamGrid, ElementAssembly_BracedFrame, ElementAssembly_Girder, ElementAssembly_ReinforcementUnit, ElementAssembly_RigidFrame, ElementAssembly_SlabField, ElementAssembly_Truss, ElementAssembly_Userdefined, ElementAssembly_Notdefined,

        //DiscreteAccessory,          // => IfcElementComponent
        DiscreteAccessory_Anchorplate, DiscreteAccessory_Bracket, DiscreteAccessory_Shoe, DiscreteAccessory_Userdefined, DiscreteAccessory_Notdefined,

        //MechanicalFastener,         // => IfcElementComponent
        MechanicalFastener_AnchorBolt, MechanicalFastener_Bolt, MechanicalFastener_Dowel, MechanicalFastener_Nail, MechanicalFastener_NailPlate, MechanicalFastener_Rivet, MechanicalFastener_Screw, MechanicalFastener_ShearConnector, MechanicalFastener_Staple, MechanicalFastener_StudShearConnector, MechanicalFastener_Userdefined, MechanicalFastener_Notdefined,

        //Fastener,                   // => IfcElementComponent
        Fastener_Glue, Fastener_Mortar, Fastener_Weld, Fastener_Userdefined, Fastener_Notdefined,

        FurnishingElement,          // => IfcElement

        //TransportElement,           // => IfcElement
        TransportElement_Elevator, TransportElement_Escalator, TransportElement_MovingWalkWay, TransportElement_Craneway, TransportElement_LiftingGear, TransportElement_Userdefined, TransportElement_Notdefined,

        VirtualElement              // => IfcElement
    }
}
