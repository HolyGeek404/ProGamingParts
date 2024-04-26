using DataAccess.DTO;
using Model.General;

namespace Model.Home;

public class HomePageModel
{
    public NewProductDto NewProductDto { get; set; } = new();
    public PromotionDto PromotionDto { get; set; } = new();
    public List<SetsSnapshotModel> SetsSnapshotDtos { get; set; } = new();
    public List<NewsSnapshotModel> NewsSnapshotDtos { get; set; } = new();
}