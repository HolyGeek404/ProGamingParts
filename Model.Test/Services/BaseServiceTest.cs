using System.Collections.Generic;
using System.Linq;
using Model.DataTransfer;
using Model.Services.General;
using NUnit.Framework;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Model.Test.Services;

[TestFixture]
public class BaseServiceTest
{
    private BaseCategoryService _baseService;

    [SetUp]
    public void SetUp()
    {
        _baseService = new BaseCategoryService();
    }

    [Test]
    public void ShouldCreateProductsList()
    {
        //Arr
        var gpuProductList = new List<GpuDto>
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
        var gpuProductDto = new GpuDto
        {
            Manufacturer = "AMD",
            Name = "RX 5600XT",
            Price = "2650"
        };
        var cpuProductDto = new CpuDto
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