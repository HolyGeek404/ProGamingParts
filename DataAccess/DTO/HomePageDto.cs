namespace DataAccess.DTO;

public class HomePageDto
{
    public int? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string TitleContent { get; set; } = string.Empty;
    public string ImgTitle { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public int PromotionPrice { get; set; }
    public int? TimeIntervalId { get; set; }
    public string Month { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public int? NewProductId { get; set; }
    public string NewProductName { get; set; } = string.Empty;
    public string NewProductPrice { get; set; } = string.Empty;
}