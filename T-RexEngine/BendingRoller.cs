using System;

namespace T_RexEngine
{
    public class BendingRoller
    {
        private double _diameter;
        private double _tolerance;
        private double _angleTolerance;
        
        public BendingRoller(double diameter, double tolerance, double angleTolerance)
        {
            Diameter = diameter;
            Tolerance = tolerance;
            AngleTolerance = angleTolerance;
        }

        public override string ToString()
        {
            return String.Format("Bending Roller{0}" +
                                 "Diameter: {1}{0}" +
                                 "Tolerance: {2}{0}" +
                                 "Angle Tolerance: {3}",
                Environment.NewLine, Diameter, Tolerance, AngleTolerance);
        }

        public double Diameter
        {
            get { return _diameter; }
            set
            {
                if (value > 0.0)
                {
                    _diameter = value;
                }
                else
                {
                    throw new ArgumentException("Diameter should be > 0");
                }
            }
        }
        public double Tolerance
        {
            get { return _tolerance; }
            set
            {
                if (value > 0.0)
                {
                    _tolerance = value;
                }
                else
                {
                    throw new ArgumentException("Tolerance should be > 0");
                }
            }
        }
        public double AngleTolerance
        {
            get { return _angleTolerance; }
            set
            {
                if (value > 0.0)
                {
                    _angleTolerance = value;
                }
                else
                {
                    throw new ArgumentException("Angle tolerance should be > 0");
                }
            }
        }
    }
}