using Model.DataTransfer;

namespace Model.Services.ApiServices.Interfaces;

public interface IHomeApiService
{
    List<MatchedProductDto> GetMatchingProducts(string input);
}