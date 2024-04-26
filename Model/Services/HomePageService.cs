using DataAccess.DAO.Interfaces;
using DataAccess.DTO;
using Model.General;
using Model.Home;
using Model.Services.Interfaces;

namespace Model.Services;

public class HomePageService(IHomeDao homeDao) : IHomePageService
{
    private List<HomePageDto> HomePageDtoList { get; set; } = [];
    private IHomeDao HomeDao { get; } = homeDao;

    public HomePageModel ReturnModel()
    {
        HomePageDtoList = HomeDao.GetHomePageData();

        return new HomePageModel
        {
            SetsSnapshotDtos = ExtractSetsSnapshotsDto(),
            PromotionDto = ExtractPromotionDto(),
            NewsSnapshotDtos = ExtractNewsSnapshotsDto(),
            NewProductDto = ExtractNewProductDto()
        };
    }

    public List<SetsSnapshotModel> ReturnSnapshotModel(int timeIntervalId)
    {
        return HomeDao.SetsPriceRanges(timeIntervalId).Cast<SetsSnapshotModel>().ToList();
    }

    private List<SetsSnapshotModel> ExtractSetsSnapshotsDto()
    {
        var x = HomePageDtoList.Where(dto => dto.TimeIntervalId != null).ToList();

        return x.Select(homePageDto => new SetsSnapshotModel
        {
            TimeIntervalId = homePageDto.TimeIntervalId,
            Month = homePageDto.Month,
            Year = homePageDto.Year
        }).ToList();
    }

    private PromotionDto ExtractPromotionDto()
    {
        var x = HomePageDtoList.Where(dto => !string.IsNullOrEmpty(dto.Name)).ToList();

        return x.Select(dto => new PromotionDto
        {
            Name = dto.Name,
            Price = dto.Price,
            PromotionPrice = dto.PromotionPrice
        }).First();
    }

    private NewProductDto ExtractNewProductDto()
    {
        var x = HomePageDtoList.Where(dto => dto.NewProductId != null).ToList();

        return x.Select(dto => new NewProductDto
        {
            NewProductName = dto.NewProductName,
            NewProductPrice = dto.NewProductPrice,
            NewProductId = dto.NewProductId
        }).First();
    }

    private List<NewsSnapshotModel> ExtractNewsSnapshotsDto()
    {
        var x = HomePageDtoList.Where(dto => !string.IsNullOrEmpty(dto.Title)).ToList();

        return x.Select(homePageDto => new NewsSnapshotModel
        {
            Id = homePageDto.Id,
            Title = homePageDto.Title,
            TitleContent = homePageDto.TitleContent,
            ImgTitle = homePageDto.ImgTitle
        }).ToList();
    }
}