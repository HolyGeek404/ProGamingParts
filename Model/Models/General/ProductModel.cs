namespace Model.Models.General;

public class ProductModel
{
    public Dictionary<string, string> AllParameters { get; set; } = [];
    public List<Dictionary<string, string>> Products { get; set; } = [];
    public List<Dictionary<string, List<string>>> FilterParametersList { get; init; } = [];
    public string FilterParameterListTemplate { get; init; } = string.Empty;
    public string Controller { get; init; } = string.Empty;
    public string? QuickFilter { get; set; }
    public string? RedirectUrl { get; set; }
}