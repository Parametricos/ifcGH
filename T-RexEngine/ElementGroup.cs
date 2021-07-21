using System;
using System.Collections.Generic;
using Rhino.Geometry;
using T_RexEngine.Enums;
using Xbim.Ifc;
using Xbim.Ifc4.Kernel;
using Xbim.Ifc4.ProductExtension;
using Xbim.Ifc4.StructuralElementsDomain;
using Xbim.Ifc4.SharedComponentElements;

namespace T_RexEngine
{
    public abstract class ElementGroup
    {
        public Material Material { get; set; }
        public ElementType ElementType { get; set; }
        public abstract List<IfcBuildingElement> ToBuildingElementIfc(IfcStore model);
        public abstract List<IfcProxy> ToProxyIfc(IfcStore model);
        public abstract List<IfcReinforcingElement> ToReinforcingElementIfc(IfcStore model);
        public abstract List<IfcElementComponent> ToElementComponentIfc(IfcStore model);
        public abstract List<IfcDistributionElement> ToDistributionElementIfc(IfcStore model);
        public abstract List<IfcElement> ToElementIfc(IfcStore model);
        public int Amount { get; set; }
        public double Volume { get; set; }
        public double Mass { get; set; }
    }
}