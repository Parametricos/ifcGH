using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.GUI.Gradient;

namespace T_RexEngine
{
    public class Material
    {
        private double _density;
        public Material(string name, string grade, double density)
        {
            Name = name;
            Grade = grade;
            Density = density;
        }

        public override string ToString()
        {
            return String.Format("Material{0}" +
                                 "Name: {1}{0}" +
                                 "Grade: {2}{0}" +
                                 "Density: {3}",
                Environment.NewLine, Name, Grade, Density);
        }
        public string Name { get; }
        public string Grade { get; }

        public double Density
        {
            get
            {
                return _density;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Density can't be < 0");
                }

                _density = value;
            }
        }
    }
}
