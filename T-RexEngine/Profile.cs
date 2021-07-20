using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class Profile
    {
        private List<Point3d> _profilePoints;
        private double _tolerance;
        private Brep[] _breps;
        
        public Profile(string name, List<Point3d> points, double tolerance)
        {
            Tolerance = tolerance;
            ProfilePoints = points;

            List<Point3d> pointsForPolyline = points;
            pointsForPolyline.Add(points[0]);
            
            Polyline polyline = new Polyline(pointsForPolyline);
            
            if (!polyline.IsClosed)
            {
                throw new ArgumentException("Polyline should be closed");
            }

            Line[] lines = polyline.GetSegments();
            List<Curve> curves = lines.Select(line => line.ToNurbsCurve()).Cast<Curve>().ToList();

            BoundarySurfaces = Brep.CreatePlanarBreps(curves, tolerance);
            ProfileCurve = polyline.ToNurbsCurve();
            Name = name;
        }

        public override string ToString()
        {
            return $"Profile{Environment.NewLine}" + $"Name: {Name}";
        }

        public double Tolerance
        {
            get { return _tolerance; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Tolerance should be > 0");
                }
                _tolerance = value;
            }

        }
        public string Name { get; }

        public List<Point3d> ProfilePoints
        {
            get { return _profilePoints; }
            private set 
            {
                if (value.Count <= 2)
                {
                    throw new ArgumentException("There should be more than 2 points as input");
                }
                
                foreach (var point3d in value)
                {
                    if (point3d.Z > Tolerance)
                    {
                        throw new ArgumentException("Points of a profile should be defined on XY Plane. Each point should have Z = 0");
                    }
                }
                _profilePoints = value;
            }
        }

        public Curve ProfileCurve { get; }

        public Brep[] BoundarySurfaces
        {
            get { return _breps; }
            private set
            {
                if (value.Length != 1)
                {
                    throw new ArgumentException("There is more than 1 brep as a result of profile creation. Check if points are correct and if the order of points is correct.");
                }

                _breps = value;
            }
        }
    }
}