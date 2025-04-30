using Microsoft.EntityFrameworkCore;
using Model.Contexts;
using Model.DataAccess.Interfaces;
using Model.DataTransfer;

namespace Model.DataAccess;

public class HomeDao(DbContextOptions<PGPContext> options) : BaseDao(options), IHomeDao
{
    public List<MatchedProductDto> GetMatchingProducts(string input)
    {
        var matchedCpus = PgpContext.Cpus.Where(x => x.Name.Contains(input) || x.Architecture.Contains(input) || x.Familiy.Contains(input) || x.Series.Contains(input) || x.Team.Contains(input))
            .Select(x => new MatchedProductDto
            {
                Name = x.Name,
                ProductId = x.ProductId,
                Team = x.Team,
                TableName = "CPU"
            }).ToList();

        var matchedGpus = PgpContext.Gpus.Where(x => x.Name.Contains(input) || x.GpuProcessorName.Contains(input) || x.GpuProcessorLine.Contains(input) || x.Manufacturer.Contains(input) || x.Team.Contains(input))
            .Select(x => new MatchedProductDto
            {
                Name = x.Name,
                ProductId = x.ProductId,
                Team = x.Team,
                TableName = "GPU"
            }).ToList();

        var matchedCoolers = PgpContext.Coolers.Where(x => x.Name.Contains(input) || x.Compatibility.Contains(input) || x.BearingType.Contains(input) || x.Team.Contains(input))
            .Select(x => new MatchedProductDto
            {
                Name = x.Name,
                ProductId = x.ProductId,
                Team = x.Team,
                TableName = "GPU"
            }).ToList();

        var result = new List<MatchedProductDto>();
        result.AddRange(matchedCoolers);
        result.AddRange(matchedCpus);
        result.AddRange(matchedGpus);

        return result;
    }
}