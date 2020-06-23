using LightShotProject;
using System;
using Xunit;

namespace LighshotProject.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void InputParamStartId()
        {
            var ex = Assert.Throws<ArgumentException>(()=> GetId.StartId = "aaa");
            Assert.Equal(expected: "Неккоректный ввод", actual:ex.Message);

        }

        [Fact]
        public void InputParamCount()
        {
            var ex = Assert.Throws<ArgumentException>(() => GetId.Count = -3);
            Assert.Equal(expected: "Неккоректный ввод", actual: ex.Message);

        }
    }
}
