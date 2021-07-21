using System;
using T_RexEngine;
using Xunit;

namespace T_RexEngine_Test
{
    public class TestCoverDimensionsHelper : TheoryData<double, double, double, double>
    {
        public TestCoverDimensionsHelper()
        {
            Add(20.00, 21.00, 22.02, 23.00);
            Add(0.020, 0.021, 0.022, 0.023);
        }
    }

    public class TestCoverDimensions
    {
        [Theory]
        [ClassData(typeof(TestCoverDimensionsHelper))]
        public void CorrectData(double left, double right, double top, double bottom)
        {
            CoverDimensions testObject = new CoverDimensions(left, right, top, bottom);
            
            Assert.Equal(left, testObject.Left);
            Assert.Equal(right, testObject.Right);
            Assert.Equal(top, testObject.Top);
            Assert.Equal(bottom, testObject.Bottom);
        }

        [Fact]
        public void TestToString()
        {
            CoverDimensions testObject = new CoverDimensions(10.00, 11.01, 0.001, 0);
            
            string expectedToString = "Cover Dimensions" + Environment.NewLine + 
                                      "Left: 10" + Environment.NewLine +
                                      "Right: 11,01" + Environment.NewLine + 
                                      "Top: 0,001" + Environment.NewLine + 
                                      "Bottom: 0";
            
            Assert.Equal(expectedToString, testObject.ToString());
        }
        
        [Fact]
        public void CheckExceptions()
        {
            var exception = Assert.Throws<ArgumentException>(() => new CoverDimensions(10.00, 11.01, -0.001, 0));
            Assert.Equal("Dimensions can't be < 0", exception.Message);
        }
    }
}