using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T_RexEngine
{
    public class RebarProperties
    {
        private double _diameter;
        public RebarProperties(double diameter, Material material)
        {
            Diameter = diameter;
            Radius = diameter / 2.0;

            Material = material;
        }

        public override string ToString()
        {
            return String.Format("Rebar Properties{0}" +
                                 "Diameter: {1}{0}" +
                                 "Material: {2}",
                Environment.NewLine, Diameter, Material.Name);
        }

        public double Radius { get; }

        public double Diameter
        {
            get
            {
                return _diameter;
            }
            set
            {
                if (value > 0)
                {
                    _diameter = value;
                }
                else
                {
                    throw new ArgumentException("Diameter should be > 0");
                }
            }
        }
        public Material Material { get; }
    }
}
