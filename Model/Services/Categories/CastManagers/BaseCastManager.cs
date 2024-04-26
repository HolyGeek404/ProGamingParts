using Newtonsoft.Json;

namespace Model.Services.Categories.CastManagers;

public abstract class BaseCastManager
{
    public string CastToJsonFormat(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
}