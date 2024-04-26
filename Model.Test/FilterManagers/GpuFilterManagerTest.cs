using System.Collections.Generic;
using System.Linq;
using DataAccess.DTO;
using Model.Gpu;
using Model.Services.Categories.FilterManagers;
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
        var gpuFiltersDtos = new List<GpuFiltersDto>
        {
            new()
            {
                Manufacturer = "man1", MemorySize = "mems1", MemoryType = "memt1", AmdGpuProcessorName = "amd1",
                NvidiaGpuProcessorName = "nvi1", Team = "nvidia"
            },
            new()
            {
                Manufacturer = "man2", MemorySize = "mems2", MemoryType = "memt2", AmdGpuProcessorName = "amd2",
                NvidiaGpuProcessorName = "nvi2", Team = "amd"
            },
            new()
            {
                Manufacturer = "man3", MemorySize = "mems3", MemoryType = "memt3", AmdGpuProcessorName = "amd3",
                NvidiaGpuProcessorName = "nvi3", Team = "amd"
            }
        };
        var expectedListOfParameters = new List<Dictionary<string, List<string>>>
        {
            new() { { "Manufactures", ["man1", "man2", "man3"] } },
            new() { { "AmdGpuProcessorNames", ["amd1", "amd2", "amd3"] } },
            new() { { "NvidiaGpuProcessorNames", ["nvi1", "nvi2", "nvi3"] } },
            new() { { "MemorySizes", ["mems1", "mems2", "mems3"] } },
            new() { { "MemoryTypes", ["memt1", "memt2", "memt3"] } },
            new() { { "Teams", ["nvidia", "amd"] } }
        };

        //Act
        var result = gpuFilterManager.CreateFilterParametersList(gpuFiltersDtos);

        //Ass
        Assert.That(result, Is.EquivalentTo(expectedListOfParameters));
    }

    [Test]
    public void ShouldFilterOutProduct()
    {
        //Arr
        var gpuFilterManager = new GpuFilterManager();

        var gpuFilterParams = new GpuFilterParamsModel
        {
            Manufactures = ["man1"],
            GpuProcessorName = ["amd"],
            MemoryTypes = ["memt3"]
        };
        var gpuProductDtos = new List<GpuProductDto>
        {
            new()
            {
                Manufacturer = "man1", GpuProcessorName = "amd", MemoryType = "memt2"
            }, // shouldn't be in result list
            new() { Manufacturer = "man1", GpuProcessorName = "amd", MemoryType = "memt3" }, // should be 
            new() { Manufacturer = "man3", GpuProcessorName = "nvidia", MemoryType = "memt1" } // shouldn't be 
        };
        var expectedCpuProductDtos = new GpuProductDto
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