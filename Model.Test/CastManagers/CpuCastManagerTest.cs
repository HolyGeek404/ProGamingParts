using System;
using System.Collections.Generic;
using Model.DataTransfer;
using Model.Models.General;
using Model.Services.Categories.CastManagers;
using NUnit.Framework;

namespace Model.Test.CastManagers
{
    [TestFixture]
    public class CpuCastManagerTest
    {
        private CpuCastManager _cpuCastManager;

        [SetUp]
        public void SetUp()
        {
            _cpuCastManager = new CpuCastManager();
        }

        [Test]
        public void CastToCpuFilterParams_ShouldMapAllPropertiesCorrectly()
        {
            // Arrange
            var param = new List<ParamBaseModel>
            {
                new() { Name = "SocketList", Value = "AM4" },
                new() { Name = "ArchitectureList", Value = "x86" },
                new() { Name = "UnlockedMultiplier", Value = "Yes" },
                new() { Name = "TeamList", Value = "TeamA" },
                new() { Name = "PriceMin", Value = "100" },
                new() { Name = "PriceMax", Value = "500" }
            };
            var expected = new CpuFiltersDto
            {
                SocketList = ["AM4"],
                ArchitectureList = ["x86"],
                UnlockedMultiplerList = ["Yes"],
                TeamList = ["TeamA"],
                PriceMin = 100,
                PriceMax = 500
            };

            // Act
            var result = _cpuCastManager.CastToCpuFilterParams(param);

            // Assert
            Assert.That(result.SocketList, Is.EqualTo(expected.SocketList));
            Assert.That(result.ArchitectureList, Is.EqualTo(expected.ArchitectureList));
            Assert.That(result.UnlockedMultiplerList, Is.EqualTo(expected.UnlockedMultiplerList));
            Assert.That(result.TeamList, Is.EqualTo(expected.TeamList));
            Assert.That(result.PriceMin, Is.EqualTo(expected.PriceMin));
            Assert.That(result.PriceMax, Is.EqualTo(expected.PriceMax));
        }

        [Test]
        public void CastToCpuFilterParams_ShouldThrowFormatExceptionForInvalidPriceMin()
        {
            // Arrange
            var param = new List<ParamBaseModel>
            {
                new() { Name = "PriceMin", Value = "invalid" }
            };

            // Act & Assert
            Assert.That(() => _cpuCastManager.CastToCpuFilterParams(param), Throws.TypeOf<FormatException>());
        }

        [Test]
        public void CastToCpuFilterParams_ShouldThrowFormatExceptionForInvalidPriceMax()
        {
            // Arrange
            var param = new List<ParamBaseModel>
            {
                new() { Name = "PriceMax", Value = "invalid" }
            };

            // Act & Assert
            Assert.That(() => _cpuCastManager.CastToCpuFilterParams(param), Throws.TypeOf<FormatException>());
        }
    }
}
