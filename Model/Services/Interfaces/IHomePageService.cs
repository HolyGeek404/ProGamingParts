using Model.General;
using Model.Home;

namespace Model.Services.Interfaces;

public interface IHomePageService
{
    HomePageModel ReturnModel();
    List<SetsSnapshotModel> ReturnSnapshotModel(int timeIntervalId);
}