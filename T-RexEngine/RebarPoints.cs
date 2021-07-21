using System;
using System.Collections.Generic;
using Rhino.Geometry;

namespace T_RexEngine
{
    public static class RebarPoints
    {
        public static List<Point3d> CreateForLineFromRectangle(Rectangle3d rectangle, int position, CoverDimensions coverDimensions, RebarProperties props)
        {
            Point3d startPoint;
            Point3d endPoint;
            
            if (position == 0)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left, rectangle.Y.Max - coverDimensions.Top - props.Radius, 0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right, rectangle.Y.Max - coverDimensions.Top - props.Radius, 0);
            }
            else if (position == 1)
            {
                startPoint = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Max - coverDimensions.Top,0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Min + coverDimensions.Bottom,0);
            }
            else if (position == 2)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
                endPoint = new Point3d(rectangle.X.Max - coverDimensions.Right, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
            }
            else if (position == 3)
            {
                startPoint = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Min + coverDimensions.Bottom,0);
                endPoint = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Max - coverDimensions.Top,0);
            }
            else
            {
                throw new ArgumentException("Position should be between 0 and 3");
            }
            
            return new List<Point3d>{startPoint, endPoint};
        }

        public static List<Point3d> CreateForUBarFromRectangle(Rectangle3d rectangle, 
            int position, CoverDimensions coverDimensions, double hookLength, RebarProperties props)
        {

            if (hookLength <= 0)
            {
                throw new ArgumentException("Hook Length should be > 0");
            }

            Point3d topLeft;
            Point3d bottomLeft;
            Point3d bottomRight;
            Point3d topRight;

            if (position == 0)
            {
                topLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Max - coverDimensions.Top - hookLength, 0);
                bottomLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Max - coverDimensions.Top - props.Radius, 0);
                bottomRight = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Max - coverDimensions.Top - props.Radius, 0);
                topRight = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Max - coverDimensions.Top - hookLength, 0);
            }
            else if (position == 1)
            {
                topLeft = new Point3d(rectangle.X.Max - coverDimensions.Right - hookLength, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
                bottomLeft = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
                bottomRight = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius,rectangle.Y.Max - coverDimensions.Top - props.Radius,0);
                topRight = new Point3d(rectangle.X.Max - coverDimensions.Right - hookLength, rectangle.Y.Max - coverDimensions.Top - props.Radius,0);
            }
            else if (position == 2)
            {
                topLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Min + coverDimensions.Bottom + hookLength, 0);
                bottomLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
                bottomRight = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
                topRight = new Point3d(rectangle.X.Max - coverDimensions.Right - props.Radius, rectangle.Y.Min + coverDimensions.Bottom + hookLength, 0);
            }
            else if (position == 3)
            {
                topLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + hookLength, rectangle.Y.Max - coverDimensions.Top - props.Radius, 0);
                bottomLeft = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Max - coverDimensions.Top - props.Radius, 0);
                bottomRight = new Point3d(rectangle.X.Min + coverDimensions.Left + props.Radius, rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
                topRight = new Point3d(rectangle.X.Min + coverDimensions.Left + hookLength,rectangle.Y.Min + coverDimensions.Bottom + props.Radius, 0);
            }
            else
            {
                throw new ArgumentException("Position should be between 0 and 3");
            }
            return new List<Point3d> {topLeft, bottomLeft, bottomRight, topRight};
        }

        public static List<Point3d> CreateForSpacerShape(double height, double length, double width,
            RebarProperties props)
        {
            if (height <= 0 || length <= 0 || width <= 0)
            {
                throw new ArgumentException("Input dimensions should be > 0");
            }
            
            List<Point3d> spacerPoints = new List<Point3d>
            {
                new Point3d(- length / 2.0, - width / 2.0 + props.Radius, props.Radius),
                new Point3d(0.0,- width / 2.0 + props.Radius,props.Radius),
                new Point3d(0.0,- width / 2.0 + props.Radius,height - props.Radius),
                new Point3d(0.0,width / 2.0 - props.Radius ,height - props.Radius),
                new Point3d(0.0,width / 2.0 - props.Radius,props.Radius),
                new Point3d(length / 2.0,width / 2.0 - props.Radius,props.Radius)
            };

            return spacerPoints;
        }
        
        public static List<Point3d> CreateForLBarShape(double height, double width, RebarProperties props)
        {
            if (height <= 0 || width <= 0)
            {
                throw new ArgumentException("Input dimensions should be > 0");
            }
            
            List<Point3d> lBarPoints = new List<Point3d>
            {
                new Point3d(0.0,props.Radius, 0.0),
                new Point3d(width - props.Radius, props.Radius, 0.0),
                new Point3d(width - props.Radius, height, 0.0)
            };

            return lBarPoints;
        }
        public static List<Point3d> CreateStirrupFromRectangleShape(Rectangle3d rectangle,
            int hooksType, BendingRoller bendingRoller, CoverDimensions coverDimensions, double hookLength,
            RebarProperties props)
        {
            if (hookLength <= 0)
            {
                throw new ArgumentException("Hook Length should be > 0");
            }
            
            List<Point3d> stirrupPoints = new List<Point3d>();

            double bendingRollerRadius = bendingRoller.Diameter / 2.0;

            double yBottom = rectangle.Y.Min + coverDimensions.Bottom + props.Radius;
            double yTop = rectangle.Y.Max - coverDimensions.Top - props.Radius;
            double xLeft = rectangle.X.Min + coverDimensions.Left + props.Radius;
            double xRight = rectangle.X.Max - coverDimensions.Right - props.Radius;

            if (hooksType == 0)
            {
                stirrupPoints.Add(new Point3d(xLeft + hookLength - props.Radius, yTop, - props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yTop, - props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yBottom, - props.Radius));
                stirrupPoints.Add(new Point3d(xRight, yBottom, - props.Radius));
                stirrupPoints.Add(new Point3d(xRight, yTop, props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yTop, props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yTop - hookLength + props.Radius, props.Radius));
            }
            else if (hooksType == 1)
            {
                double polylinePointOffsetForHook = (bendingRollerRadius + props.Radius) * Math.Sqrt(2);
                double hookEndPointOffset =
                    ((Math.Sqrt(2) - 1) * (bendingRollerRadius + props.Radius) - props.Radius + hookLength +
                     (bendingRollerRadius + props.Radius)) / Math.Sqrt(2);

                stirrupPoints.Add(new Point3d(xLeft + hookEndPointOffset, yTop + polylinePointOffsetForHook - hookEndPointOffset, -props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yTop + polylinePointOffsetForHook, -props.Radius));
                stirrupPoints.Add(new Point3d(xLeft, yBottom, -props.Radius));
                stirrupPoints.Add(new Point3d(xRight, yBottom, -props.Radius));
                stirrupPoints.Add(new Point3d(xRight, yTop, props.Radius));
                stirrupPoints.Add(new Point3d(xLeft - polylinePointOffsetForHook, yTop, props.Radius));
                stirrupPoints.Add(new Point3d(xLeft - polylinePointOffsetForHook + hookEndPointOffset,
                                                  yTop - hookEndPointOffset,
                                                  props.Radius));
            }
            else
            {
                throw new ArgumentException("Hooks Type should be between 0 and 1");
            }

            return stirrupPoints;
        }       
    }
}