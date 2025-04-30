using Model.DataAccess.Interfaces;
using Model.DataTransfer;
using Model.Services.ApiServices.Interfaces;

namespace Model.Services.ApiServices
{
    public class HomeApiService(IHomeDao homeDao) : IHomeApiService
    {
        private IHomeDao HomeDao { get; set; } = homeDao;

        public List<MatchedProductDto> GetMatchingProducts(string input)
        {
            return HomeDao.GetMatchingProducts(input);
        }
    }
}
