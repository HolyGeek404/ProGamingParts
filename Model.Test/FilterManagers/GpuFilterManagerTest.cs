using System.Collections.Generic;
using System.Linq;
using Model.DataTransfer;
using Model.Services.Categories.FilterManagers.GPU;
using NUnit.Framework;

namespace Model.Test.FilterManagers;

[TestFixture]
public class GpuFilterManagerTest
{
    [Test]
    public void ShouldCreateListOfAvailableFilters()
    {
        //Arr
        var gpuFilterManager = new GpuFilterManager();
        var gpuFilters = new GpuFiltersDto
        {
            ManufactureList = ["man1", "man2", "man3"],
            AmdGpuProcessorNameList = ["amd1", "amd2", "amd3"],
            NvidiaGpuProcessorNameList = ["nvi1", "nvi2", "nvi3"],
            MemorySizeList = ["mems1", "mems2", "mems3"],
            MemoryTypeList = ["memt1", "memt2", "memt3"],
            TeamList = ["nvidia", "amd"]
        };

        var expectedListOfParameters = new List<Dictionary<string, List<string>>>
        {
            new() { { "Manufactures", ["man1", "man2", "man3"] } },
            new() { { "AmdGpuProcessorNames", ["amd1", "amd2", "amd3"] } },
            new() { { "NvidiaGpuProcessorNames", ["nvi1", "nvi2", "nvi3"] } },
            new() { { "MemorySizes", ["mems1", "mems2", "mems3"] } },
            new() { { "MemoryTypes", ["memt1", "memt2", "memt3"] } },
            new() { { "TeamList", ["nvidia", "amd"] } }
        };

        //Act
        var result = gpuFilterManager.CreateFilterParametersList(gpuFilters);

        //Ass
        Assert.That(result, Is.EquivalentTo(expectedListOfParameters));
    }

    [Test]
    public void ShouldFilterOutProduct()
    {
        //Arr
        var gpuFilterManager = new GpuFilterManager();

        var gpuFilterParams = new GpuFiltersDto
        {
            ManufactureList = ["man1"],
            GpuProcessorNameList = ["amd"],
            MemoryTypeList = ["memt3"]
        };
        var gpuProductDtos = new List<GpuDto>
        {
            new() { Manufacturer = "man1", GpuProcessorName = "amd", MemoryType = "memt2" },
            new() { Manufacturer = "man1", GpuProcessorName = "amd", MemoryType = "memt3" },
            new() { Manufacturer = "man3", GpuProcessorName = "nvidia", MemoryType = "memt1" }
        };
        var expectedCpuProductDtos = new GpuDto
        { Manufacturer = "man1", GpuProcessorName = "amd", MemoryType = "memt3" };

        //Act
        var result = gpuFilterManager.FilterOutProducts(gpuFilterParams, gpuProductDtos);

        //Ass
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().Manufacturer, Is.EqualTo(expectedCpuProductDtos.Manufacturer));
        Assert.That(result.First().GpuProcessorName, Is.EqualTo(expectedCpuProductDtos.GpuProcessorName));
        Assert.That(result.First().MemoryType, Is.EqualTo(expectedCpuProductDtos.MemoryType));
    }
}