using Model.DataTransfer;

namespace Model.DataAccess.Interfaces;

public interface IHomeDao
{
    List<MatchedProductDto> GetMatchingProducts(string input);
}