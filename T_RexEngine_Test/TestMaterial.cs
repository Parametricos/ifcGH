using System;
using T_RexEngine;
using Xunit;

namespace T_RexEngine_Test
{
    public class TestMaterialHelper : TheoryData<string, string, double>
    {
        public TestMaterialHelper()
        {
            Add("Name", "Grade", 10);
            Add("Name of material", "Grade of material", 0.001);
        }
    }

    public class TestMaterial
    {
        [Theory]
        [ClassData(typeof(TestMaterialHelper))]
        public void CorrectData(string name, string grade, double density)
        {
            Material testObject = new Material(name, grade, density);
            
            Assert.Equal(name, testObject.Name);
            Assert.Equal(grade, testObject.Grade);
            Assert.Equal(density, testObject.Density);
        }
        [Fact]
        public void TestToString()
        {
            Material testObject = new Material("Test name", "Test grade", 100.01);
            
            string expectedToString = "Material" + Environment.NewLine + 
                                      "Name: Test name" + Environment.NewLine +
                                      "Grade: Test grade" + Environment.NewLine + 
                                      "Density: 100,01";
            
            Assert.Equal(expectedToString, testObject.ToString());
        }
        
        [Fact]
        public void CheckExceptions()
        {
            var exception = Assert.Throws<ArgumentException>(() => new Material("Test", "Test", -0.01));
            Assert.Equal("Density can't be < 0", exception.Message);
        }
    }
}