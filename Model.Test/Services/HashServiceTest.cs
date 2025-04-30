using Model.Services.General;
using NUnit.Framework;

namespace Model.Test.Services;

[TestFixture]
public class HashServiceTest
{
    private HashService HashService { get; set; }

    [SetUp]
    public void SetUp()
    {
        HashService = new HashService();
    }

    [Test]
    public void ShouldVerifyPassword()
    {
        const string inputPassword = "Wiktor1@34";
        
        var hashedPassword = HashService.Hash("Wiktor1@34");
        var result = HashService.VerifyPassword(inputPassword, hashedPassword);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void ShouldNotVerifyPassword()
    {
        const string inputPassword = "Rychu1@34";
        
        var hashedPassword = HashService.Hash("Wiktor1@34");
        var result = HashService.VerifyPassword(inputPassword, hashedPassword);
        
        Assert.That(result, Is.False);
    }
}