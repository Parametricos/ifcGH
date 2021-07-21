using System;
using T_RexEngine;
using Xunit;

namespace T_RexEngine_Test
{
    public class TestRebarPropertiesHelper : TheoryData<double, Material>
    {
        public TestRebarPropertiesHelper()
        {
            Add(20.00, new Material("Name", "Grade", 10.0));
            Add(0.012, new Material("Name", "Grade", 10.0));
        }
    }

    public class TestRebarProperties
    {
        [Theory]
        [ClassData(typeof(TestRebarPropertiesHelper))]
        public void CorrectData(double diameter, Material material)
        {
            RebarProperties testObject = new RebarProperties(diameter, material);
            
            Assert.Equal(diameter, testObject.Diameter);
            Assert.Equal(material, testObject.Material);
        }
        [Fact]
        public void TestToString()
        {
            RebarProperties testObject = new RebarProperties(10.02, new Material("Name", "Grade", 10.0));

            string expectedToString = "Rebar Properties" + Environment.NewLine +
                                      "Diameter: 10,02" + Environment.NewLine +
                                      "Material: Name";

            Assert.Equal(expectedToString, testObject.ToString());
        }
        
        [Fact]
        public void CheckExceptions()
        {
            var exception = Assert.Throws<ArgumentException>(() => new RebarProperties(0, new Material("Material", "Grade", 0.10)));
            Assert.Equal("Diameter should be > 0", exception.Message);
        }

        [Fact]
        public void TestRadius_Diameter20()
        {
            RebarProperties testObject = new RebarProperties(20.00, new Material("", "", 10.0));
            Assert.Equal(10.00, testObject.Radius);
        }

        [Fact]
        public void TestRadius_Diameter0001()
        {
            RebarProperties testObject = new RebarProperties(0.001, new Material("", "", 10.0));
            Assert.Equal(0.0005, testObject.Radius);
        }
    }
}