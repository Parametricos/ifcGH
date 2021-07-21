using System;
using System.Collections.Generic;
using Rhino.Geometry;

namespace T_RexEngine
{
    public class RebarShape
    {
        private Curve _rebarCurve;
        public RebarShape(RebarProperties props)
        {
            Props = props;
        }
        private Mesh CreateRebarMesh(Curve rebarCurve, double radius)
        {
            return Mesh.CreateFromCurvePipe(rebarCurve, radius, 12, 70, MeshPipeCapStyle.Flat, false);
        }
        private Curve CreateFilletPolylineWithBendingRoller(Curve rebarCurve, BendingRoller bendingRoller)
        {
            Curve filletedCurve = Curve.CreateFilletCornersCurve(rebarCurve, bendingRoller.Diameter / 2.0 + Props.Radius, bendingRoller.Tolerance, bendingRoller.AngleTolerance);
            if (filletedCurve == null)
            {
                throw new Exception("Cannot create fillets for this parameters. Try to change Bending Roller or shape.");
            }
            if (filletedCurve.IsPolyline())
            {
                throw new Exception("Rebar curve cannot be a polyline - check if Bending Roller is set properly");
            }

            return filletedCurve;
        }
        public void CurveToRebarShape(Curve rebarCurve)
        {
            RebarCurve = rebarCurve;
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }
        public void PolylineToRebarShape(Curve rebarCurve, BendingRoller bendingRoller)
        {
            if (!rebarCurve.TryGetPolyline(out Polyline rebarPolyline))
            {
                throw new ArgumentException("Input curve is not a polyline");
            }
            if (rebarPolyline.Count < 3)
            {
                throw new ArgumentException("Polyline has to contain at least 3 points");
            }
            
            RebarCurve = CreateFilletPolylineWithBendingRoller(rebarCurve, bendingRoller);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }
        private Curve CreateRebarCurveInAnotherPlane(Curve curve, Plane originPlane, Plane destinationPlane)
        {
            Curve rebarCurve = curve.DuplicateCurve();
            Transform planeToPlane = Transform.PlaneToPlane(originPlane, destinationPlane);
            rebarCurve.Transform(planeToPlane);
            
            return rebarCurve;
        }

        public void BuildRectangleToLineBarShape(Rectangle3d rectangle, int position, CoverDimensions coverDimensions)
        {
            List<Point3d> shapePoints =
                RebarPoints.CreateForLineFromRectangle(rectangle, position, coverDimensions, Props);
            LineCurve line = new LineCurve(shapePoints[0], shapePoints[1]);
            
            RebarCurve = CreateRebarCurveInAnotherPlane(line, Plane.WorldXY, rectangle.Plane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void BuildRectangleToUBarShape(Rectangle3d rectangle, BendingRoller bendingRoller,
            int position, CoverDimensions coverDimensions, double hookLength)
        {
            List<Point3d> shapePoints =
                RebarPoints.CreateForUBarFromRectangle(rectangle, position, coverDimensions, hookLength, Props);
            PolylineCurve polyline = new PolylineCurve(shapePoints);
            Curve filletedPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRoller);
            
            RebarCurve = CreateRebarCurveInAnotherPlane(filletedPolyline, Plane.WorldXY, rectangle.Plane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void BuildSpacerShape(Plane insertPlane, double height, double length, double width, BendingRoller bendingRoller)
        {
            List<Point3d> shapePoints =
                RebarPoints.CreateForSpacerShape(height, length, width, Props);
            PolylineCurve polyline = new PolylineCurve(shapePoints);
            Curve filletedPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRoller);

            RebarCurve = CreateRebarCurveInAnotherPlane(filletedPolyline, Plane.WorldXY, insertPlane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }
        
        public void BuildLBarShape(Plane insertPlane, double height, double width, BendingRoller bendingRoller)
        {
            List<Point3d> shapePoints = RebarPoints.CreateForLBarShape(height, width, Props);
            
            PolylineCurve polyline = new PolylineCurve(shapePoints);
            Curve filletedPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRoller);

            RebarCurve = CreateRebarCurveInAnotherPlane(filletedPolyline, Plane.WorldXY, insertPlane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }

        public void BuildRectangleToStirrupShape(Rectangle3d rectangle, BendingRoller bendingRoller,
             int hooksType, CoverDimensions coverDimensions, double hookLength)
        {
            List<Point3d> shapePoints = RebarPoints.CreateStirrupFromRectangleShape(rectangle,
            hooksType, bendingRoller, coverDimensions, hookLength, Props);

            PolylineCurve polyline = new PolylineCurve(shapePoints);
            Curve filletedPolyline = CreateFilletPolylineWithBendingRoller(polyline, bendingRoller);

            RebarCurve = CreateRebarCurveInAnotherPlane(filletedPolyline, Plane.WorldXY, rectangle.Plane);
            RebarMesh = CreateRebarMesh(RebarCurve, Props.Radius);
        }
        public void BuildStirrupShape(Plane plane, double height, double width, BendingRoller bendingRoller, int hooksType, double hookLength)
        {
            Rectangle3d rectangle = new Rectangle3d(plane, width, height);
            CoverDimensions coverDimensions = new CoverDimensions(0.0, 0.0, 0.0, 0.0);
            
            BuildRectangleToStirrupShape(rectangle, bendingRoller, hooksType, coverDimensions, hookLength);
        }

        public override string ToString()
        {
            return String.Format("Rebar Shape{0}" +
                                 "Diameter: {1}{0}" +
                                 "Length: {2}",
                Environment.NewLine, Props.Diameter, RebarCurve.GetLength());
        }

        public Mesh RebarMesh { get; private set; }

        public Curve RebarCurve
        {
            get { return _rebarCurve; }
            set
            {
                if (value.GetLength() <= 0)
                {
                    throw new ArgumentException("Length of the Rebar Curve can't be 0");
                }
                _rebarCurve = value;
            }
        }
        public RebarProperties Props { get; }
    }
}
