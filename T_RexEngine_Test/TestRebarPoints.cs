using System;
using System.Collections.Generic;
using T_RexEngine;
using Xunit;
using Rhino.Geometry;

namespace T_RexEngine_Test
{
    public class TestRebarPoints
    {
        readonly Rectangle3d _rectangle = new Rectangle3d(Plane.WorldXY, Point3d.Origin, new Point3d(0.500, 0.400, 0));
        readonly CoverDimensions _coverDimensions = new CoverDimensions(0.015, 0.020, 0.025, 0.030);
        readonly RebarProperties _props = new RebarProperties(0.012, new Material("Name", "Grade", 7850));
        readonly BendingRoller _bendingRoller = new BendingRoller(0.060, 0.001, 0.0175);
        
        #region TestCreateForLineFromRectangle

        [Fact]
        public void TestCreateForLineFromRectangle_Position0()
        {
            int position = 0;
            List<Point3d> actualPoints = RebarPoints.CreateForLineFromRectangle(_rectangle, position, _coverDimensions, _props);
            List<Point3d> expectedPoints = new List<Point3d>{new Point3d(0.015,0.369,0), new Point3d(0.48,0.369,0)};
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        [Fact]
        public void TestCreateForLineFromRectangle_Position1()
        {
            int position = 1;
            List<Point3d> actualPoints = RebarPoints.CreateForLineFromRectangle(_rectangle, position, _coverDimensions, _props);
            List<Point3d> expectedPoints = new List<Point3d>{new Point3d(0.474,0.375,0), new Point3d(0.474,0.03,0)};
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        [Fact]
        public void TestCreateForLineFromRectangle_Position2()
        {
            int position = 2;
            List<Point3d> actualPoints = RebarPoints.CreateForLineFromRectangle(_rectangle, position, _coverDimensions, _props);
            List<Point3d> expectedPoints = new List<Point3d>{new Point3d(0.015,0.036,0), new Point3d(0.48,0.036,0)};
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        [Fact]
        public void TestCreateForULineFromRectangle_Position3()
        {
            int position = 3;
            List<Point3d> actualPoints = RebarPoints.CreateForLineFromRectangle(_rectangle, position, _coverDimensions, _props);
            List<Point3d> expectedPoints = new List<Point3d>{new Point3d(0.021,0.03,0), new Point3d(0.021,0.375,0)};
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        [Fact]
        public void CheckExceptions_CreateForULineFromRectangle_Position4()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForLineFromRectangle(_rectangle, 4, _coverDimensions, _props));
            Assert.Equal("Position should be between 0 and 3", exception.Message);
        }
        
        #endregion

        #region TestCreateForUBarFromRectangle

        [Fact]
        public void TestCreateForUBarFromRectangle_Position0()
        {
            int position = 0;
            List<Point3d> actualPoints = RebarPoints.CreateForUBarFromRectangle(_rectangle, position, _coverDimensions, 0.100, _props);
            List<Point3d> expectedPoints = new List<Point3d>
            {
                new Point3d(0.021,0.275,0),
                new Point3d(0.021,0.369,0),
                new Point3d(0.474,0.369,0),
                new Point3d(0.474,0.275,0)
            };
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        
        [Fact]
        public void TestCreateForUBarFromRectangle_Position1()
        {
            int position = 1;
            List<Point3d> actualPoints = RebarPoints.CreateForUBarFromRectangle(_rectangle, position, _coverDimensions, 0.100, _props);
            List<Point3d> expectedPoints = new List<Point3d>
            {
                new Point3d(0.38,0.036,0),
                new Point3d(0.474,0.036,0),
                new Point3d(0.474,0.369,0),
                new Point3d(0.38,0.369,0)
            };
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        
        [Fact]
        public void TestCreateForUBarFromRectangle_Position2()
        {
            int position = 2;
            List<Point3d> actualPoints = RebarPoints.CreateForUBarFromRectangle(_rectangle, position, _coverDimensions, 0.100, _props);
            List<Point3d> expectedPoints = new List<Point3d>
            {
                new Point3d(0.021,0.13,0),
                new Point3d(0.021,0.036,0),
                new Point3d(0.474,0.036,0),
                new Point3d(0.474,0.13,0)
            };
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        
        [Fact]
        public void TestCreateForUBarFromRectangle_Position3()
        {
            int position = 3;
            List<Point3d> actualPoints = RebarPoints.CreateForUBarFromRectangle(_rectangle, position, _coverDimensions, 0.100, _props);
            List<Point3d> expectedPoints = new List<Point3d>
            {
                new Point3d(0.115,0.369,0),
                new Point3d(0.021,0.369,0),
                new Point3d(0.021,0.036,0),
                new Point3d(0.115,0.036,0)
            };
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        
        [Fact]
        public void CheckExceptions_CreateForUBarFromRectangle_Position4()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForUBarFromRectangle(_rectangle, 4, _coverDimensions, 0.100, _props));
            Assert.Equal("Position should be between 0 and 3", exception.Message);
        }
        
        [Fact]
        public void CheckExceptions_CreateForUBarFromRectangle_Hook0()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForUBarFromRectangle(_rectangle, 3, _coverDimensions, 0, _props));
            Assert.Equal("Hook Length should be > 0", exception.Message);
        }

        #endregion

        #region TestCreateStirrupFromRectangleShape

        [Fact]
        public void TestCreateStirrupFromRectangleShape_HookType0()
        {
            int hookType = 0;
            List<Point3d> actualPoints = RebarPoints.CreateStirrupFromRectangleShape(_rectangle, hookType, _bendingRoller, _coverDimensions, 0.100, _props);
            List<Point3d> expectedPoints = new List<Point3d>
            {
                new Point3d(0.115,0.369,-0.006),
                new Point3d(0.021,0.369,-0.006),
                new Point3d(0.021,0.036,-0.006),
                new Point3d(0.474,0.036,-0.006),
                new Point3d(0.474,0.369,0.006),
                new Point3d(0.021,0.369,0.006),
                new Point3d(0.021,0.275,0.006)
            };
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        
        [Fact]
        public void TestCreateStirrupFromRectangleShape_HookType1()
        {
            int hookType = 1;
            List<Point3d> actualPoints = RebarPoints.CreateStirrupFromRectangleShape(_rectangle, hookType, _bendingRoller, _coverDimensions, 0.100, _props);
            List<Point3d> expectedPoints = new List<Point3d>
            {
                new Point3d(0.1235,0.3174,-0.006),
                new Point3d(0.021,0.4199,-0.006),
                new Point3d(0.021,0.036,-0.006),
                new Point3d(0.474,0.036,-0.006),
                new Point3d(0.474,0.369,0.006),
                new Point3d(-0.0299,0.369,0.006),
                new Point3d(0.0726,0.2665,0.006)
            };
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        
        [Fact]
        public void CheckExceptions_CreateStirrupFromRectangleShape_HookType2()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateStirrupFromRectangleShape(_rectangle, 2, _bendingRoller, _coverDimensions, 0.100, _props));
            Assert.Equal("Hooks Type should be between 0 and 1", exception.Message);
        }
        
        [Fact]
        public void CheckExceptions_CreateStirrupFromRectangleShape_Hook0()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateStirrupFromRectangleShape(_rectangle, 1, _bendingRoller, _coverDimensions, 0, _props));
            Assert.Equal("Hook Length should be > 0", exception.Message);
        }

        #endregion

        #region TestCreateForSpacerShape

        [Fact]
        public void TestCreateForSpacerShape()
        {
            List<Point3d> actualPoints = RebarPoints.CreateForSpacerShape(0.500, 0.300, 0.400, _props);
            List<Point3d> expectedPoints = new List<Point3d>
            {
                new Point3d(-0.15,-0.194,0.006),
                new Point3d(0,-0.194,0.006),
                new Point3d(0,-0.194,0.494),
                new Point3d(0,0.194,0.494),
                new Point3d(0,0.194,0.006),
                new Point3d(0.15,0.194,0.006)
            };
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }
        
        [Fact]
        public void CheckExceptions_CreateForSpacerShape_Length0()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForSpacerShape(0.500, 0, 0.400, _props));
            Assert.Equal("Input dimensions should be > 0", exception.Message);
        }
        
        [Fact]
        public void CheckExceptions_CreateForSpacerShape_Height0()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForSpacerShape(0, 0.300, 0.400, _props));
            Assert.Equal("Input dimensions should be > 0", exception.Message);
        }
        
        [Fact]
        public void CheckExceptions_CreateForSpacerShape_Width0()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForSpacerShape(0.500, 0.300, 0, _props));
            Assert.Equal("Input dimensions should be > 0", exception.Message);
        }

        #endregion
        
        #region TestCreateForLBarShape

        [Fact]
        public void TestCreateForLBarShape()
        {
            List<Point3d> actualPoints = RebarPoints.CreateForLBarShape(0.500, 0.300, _props);
            List<Point3d> expectedPoints = new List<Point3d>
            {
                new Point3d(0,0.006,0),
                new Point3d(0.294,0.006,0),
                new Point3d(0.294,0.5,0)
            };
            
            Assert.Equal(expectedPoints.Count, actualPoints.Count);

            for (int i = 0; i < actualPoints.Count; i++)
            {
                Assert.Equal(expectedPoints[i].X, actualPoints[i].X, 4);
                Assert.Equal(expectedPoints[i].Y, actualPoints[i].Y, 4);
                Assert.Equal(expectedPoints[i].Z, actualPoints[i].Z, 4);
            }
        }

        [Fact]
        public void CheckExceptions_CreateForLBarShape_Height0()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForLBarShape(0, 0.300, _props));
            Assert.Equal("Input dimensions should be > 0", exception.Message);
        }
        
        [Fact]
        public void CheckExceptions_CreateForLBarShape_Width0()
        {
            var exception = Assert.Throws<ArgumentException>(() => RebarPoints.CreateForLBarShape(0.500, 0, _props));
            Assert.Equal("Input dimensions should be > 0", exception.Message);
        }

        #endregion
    }
}