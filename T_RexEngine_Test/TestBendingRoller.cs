using System;
using T_RexEngine;
using Xunit;

namespace T_RexEngine_Test
{
    public class TestBendingRollerHelper : TheoryData<double, double, double>
    {
        public TestBendingRollerHelper()
        {
            Add(60.8, 0.001, 0.01);
        }
    }

    public class TestBendingRoller
    {
        [Theory]
        [ClassData(typeof(TestBendingRollerHelper))]
        public void CorrectData(double diameter, double tolerance, double angleTolerance)
        {
            BendingRoller testObject = new BendingRoller(diameter, tolerance, angleTolerance);
            
            Assert.Equal(diameter, testObject.Diameter);
            Assert.Equal(tolerance, testObject.Tolerance);
            Assert.Equal(angleTolerance, testObject.AngleTolerance);
        }
        [Fact]
        public void TestToString()
        {
            BendingRoller testObject = new BendingRoller(60.8, 0.001, 0.01);

            string expectedToString = "Bending Roller" + Environment.NewLine +
                                      "Diameter: 60,8" + Environment.NewLine +
                                      "Tolerance: 0,001" + Environment.NewLine + 
                                      "Angle Tolerance: 0,01";

            Assert.Equal(expectedToString, testObject.ToString());
        }
        
        [Fact]
        public void CheckExceptions_Diameter()
        {
            var exception = Assert.Throws<ArgumentException>(() => new BendingRoller(-0.01, 0.01, 0.01));
            Assert.Equal("Diameter should be > 0", exception.Message);
        }
        
        [Fact]
        public void CheckExceptions_Tolerance()
        {
            var exception = Assert.Throws<ArgumentException>(() => new BendingRoller(0.01, -0.01, 0.01));
            Assert.Equal("Tolerance should be > 0", exception.Message);
        }
        
        [Fact]
        public void CheckExceptions_AngleTolerance()
        {
            var exception = Assert.Throws<ArgumentException>(() => new BendingRoller(0.01, 0.01, -0.01));
            Assert.Equal("Angle tolerance should be > 0", exception.Message);
        }
    }
}