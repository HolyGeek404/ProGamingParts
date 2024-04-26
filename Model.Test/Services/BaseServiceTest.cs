using System.Collections.Generic;
using System.Linq;
using DataAccess.DTO;
using Model.Services;
using NUnit.Framework;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Model.Test.Services;

[TestFixture]
public class BaseServiceTest
{
    [SetUp]
    public void SetUp()
    {
        _baseService = new BaseService();
    }

    private BaseService _baseService;

    [Test]
    public void ShouldCreateProductsList()
    {
        //Arr
        var gpuProductList = new List<GpuProductDto>
        {
            new() { Name = "FirstGpu", Team = "AMD" },
            new() { Name = "SecondGpu", Team = "Nvidia" }
        }.Cast<object>().ToList();

        var expectedProductList = new List<Dictionary<string, string>>
        {
            new()
            {
                { "Name", "FirstGpu" },
                { "Team", "AMD" }
            },
            new()
            {
                { "Name", "SecondGpu" },
                { "Team", "Nvidia" }
            }
        };

        //Act
        var result = _baseService.CreateListOfProducts(gpuProductList);

        //Ass
        Assert.That(result, Is.EquivalentTo(expectedProductList));
    }

    [Test]
    public void ShouldCreateProduct()
    {
        //Arr
        var gpuProductDto = new GpuProductDto
        {
            Manufacturer = "AMD",
            Name = "RX 5600XT",
            Price = "2650"
        };
        var cpuProductDto = new CpuProductDto
        {
            Architecture = "Zen 2",
            Name = "AMD Ryzen 7 2700",
            Cores = "8"
        };

        //Act
        var gpuResultProduct = _baseService.CreateProduct(gpuProductDto);
        var cpuResultProduct = _baseService.CreateProduct(cpuProductDto);

        //Ass
        Assert.That(gpuResultProduct.AllParameters.ContainsKey("Manufacturer"), Is.True);
        Assert.That(gpuResultProduct.AllParameters.ContainsKey("Name"), Is.True);
        Assert.That(gpuResultProduct.AllParameters.ContainsKey("Price"), Is.True);

        Assert.That(gpuResultProduct.AllParameters.ContainsValue("AMD"), Is.True);
        Assert.That(gpuResultProduct.AllParameters.ContainsValue("RX 5600XT"), Is.True);
        Assert.That(gpuResultProduct.AllParameters.ContainsValue("2650"), Is.True);

        Assert.That(cpuResultProduct.AllParameters.ContainsKey("Architecture"), Is.True);
        Assert.That(cpuResultProduct.AllParameters.ContainsKey("Name"), Is.True);
        Assert.That(cpuResultProduct.AllParameters.ContainsKey("Cores"), Is.True);

        Assert.That(cpuResultProduct.AllParameters.ContainsValue("Zen 2"), Is.True);
        Assert.That(cpuResultProduct.AllParameters.ContainsValue("AMD Ryzen 7 2700"), Is.True);
        Assert.That(cpuResultProduct.AllParameters.ContainsValue("8"), Is.True);
    }
}