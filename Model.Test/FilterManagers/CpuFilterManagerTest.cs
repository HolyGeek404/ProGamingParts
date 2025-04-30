using System.Collections.Generic;
using System.Linq;
using Model.DataTransfer;
using Model.Services.Categories.FilterManagers.CPU;
using NUnit.Framework;

namespace Model.Test.FilterManagers;

[TestFixture]
public class CpuFilterManagerTest
{
    [Test]
    public void ShouldCreateListOfAvailableFilters()
    {
        //Arr
        var cpuFilterManager = new CpuFilterManager();
        var cpuFilters = new CpuFiltersDto
        {
            ArchitectureList = ["ar1", "ar2", "ar3"],
            SocketList = ["soc1", "soc2", "soc3"],
            UnlockedMultiplerList = ["yes", "no", "maybe"],
            TeamList = ["amd", "intel"]
        };

        var expectedListOfParameters = new List<Dictionary<string, List<string>>>
        {
            new() { { "SocketList", ["soc1", "soc2", "soc3"] } },
            new() { { "ArchitectureList", ["ar1", "ar2", "ar3"] } },
            new() { { "UnlockedMultiplier", ["yes", "no", "maybe"] } },
            new() { { "TeamList", ["amd", "intel"] } }
        };

        //Act
        var result = cpuFilterManager.CreateFilterParametersList(cpuFilters);

        //Ass
        Assert.That(result, Is.EquivalentTo(expectedListOfParameters));
    }

    [Test]
    public void ShouldFilterOutProduct()
    {
        //Arr
        var cpuFilterManager = new CpuFilterManager();

        var cpuFilterParams = new CpuFiltersDto
        {
            ArchitectureList = ["ar1"],
            SocketList = ["soc2"],
            UnlockedMultiplerList = ["maybe"],
            TeamList = ["amd"]
        };
        var cpuProductDtoList = new List<CpuDto>
        {
            new() { Architecture = "ar2", Socket = "soc2", UnlockedMultipler = "maybe", Team = "intel"},
            new() { Architecture = "ar1", Socket = "soc2", UnlockedMultipler = "maybe", Team = "amd"},
            new() { Architecture = "ar3", Socket = "soc3", UnlockedMultipler = "no",  Team = "intel"}
        };
        var expectedCpuProductDtoList = new CpuDto
            { Architecture = "ar1", Socket = "soc2", UnlockedMultipler = "maybe", Team = "amd"};

        //Act
        var result = cpuFilterManager.FilterOutProducts(cpuFilterParams, cpuProductDtoList);

        //Ass
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().Architecture, Is.EqualTo(expectedCpuProductDtoList.Architecture));
        Assert.That(result.First().Socket, Is.EqualTo(expectedCpuProductDtoList.Socket));
        Assert.That(result.First().UnlockedMultipler, Is.EqualTo(expectedCpuProductDtoList.UnlockedMultipler));
    }
}