using System;

namespace T_RexEngine
{
    public class CoverDimensions
    {
        public CoverDimensions(double left, double right, double top, double bottom)
        {
            if (left < 0 || right < 0 || top < 0 || bottom < 0)
            {
                throw new ArgumentException("Dimensions can't be < 0");
            }
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public override string ToString()
        {
            return String.Format("Cover Dimensions{0}" +
                                 "Left: {1}{0}" +
                                 "Right: {2}{0}" +
                                 "Top: {3}{0}" +
                                 "Bottom: {4}",
                Environment.NewLine, Left, Right, Top, Bottom);
        }

        public double Left { get; }
        public double Right { get; }
        public double Top { get; }
        public double Bottom { get; }
    }
}
