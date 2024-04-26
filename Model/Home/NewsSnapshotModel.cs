namespace Model.Home;

public class NewsSnapshotModel
{
    public int? Id { get; set; }
    public string Title { get; init; } = string.Empty;
    public string TitleContent { get; init; } = string.Empty;
    public string ImgTitle { get; set; } = string.Empty;
}