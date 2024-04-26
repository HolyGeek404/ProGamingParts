using System.Collections.Generic;
using System.Linq;
using DataAccess.DTO;
using Model.Cpu;
using Model.Services.Categories.FilterManagers;
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
        var cpuFiltersDtoList = new List<CpuFiltersDto>
        {
            new() { Architecture = "ar1", Socket = "soc1", UnlockedMultipler = "yes", Team = "amd"},
            new() { Architecture = "ar2", Socket = "soc2", UnlockedMultipler = "no", Team = "intel" },
            new() { Architecture = "ar3", Socket = "soc3", UnlockedMultipler = "maybe", Team = "amd" }
        };
        var expectedListOfParameters = new List<Dictionary<string, List<string>>>
        {
            new() { { "Sockets", ["soc1", "soc2", "soc3"] } },
            new() { { "Architectures", ["ar1", "ar2", "ar3"] } },
            new() { { "UnlockedMultiplier", ["yes", "no", "maybe"] } },
            new() { { "Teams", ["amd", "intel"] } }
        };

        //Act
        var result = cpuFilterManager.CreateFilterParametersList(cpuFiltersDtoList);

        //Ass
        Assert.That(result, Is.EquivalentTo(expectedListOfParameters));
    }

    [Test]
    public void ShouldFilterOutProduct()
    {
        //Arr
        var cpuFilterManager = new CpuFilterManager();

        var cpuFilterParams = new CpuFilterParams
        {
            Architectures = ["ar1"],
            Sockets = ["soc2"],
            UnlockedMultiplers = ["maybe"],
            Teams = ["amd"]
        };
        var cpuProductDtoList = new List<CpuProductDto>
        {
            new() { Architecture = "ar2", Socket = "soc2", UnlockedMultipler = "maybe", Team = "intel"}, // shouldn't be in result list
            new() { Architecture = "ar1", Socket = "soc2", UnlockedMultipler = "maybe", Team = "amd"}, // should be 
            new() { Architecture = "ar3", Socket = "soc3", UnlockedMultipler = "no",  Team = "intel"} // shouldn't be in result list
        };
        var expectedCpuProductDtoList = new CpuProductDto
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