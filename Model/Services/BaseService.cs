using Model.General;

namespace Model.Services;

public class BaseService
{
    public List<Dictionary<string, string>> CreateListOfProducts(List<object> productList)
    {
        var products = new List<Dictionary<string, string>>();

        foreach (var product in productList)
        {
            var productProperties = product.GetType().GetProperties();
            var productDictionary = new Dictionary<string, string>();

            foreach (var property in productProperties)
            {
                if (property.Name is "AllParameters" or "Products" or "FilterParametersList")
                {
                    continue;
                }
                var parameterValue = property.GetValue(product)?.ToString();
                var parameterName = property.Name;

                if (!string.IsNullOrEmpty(parameterValue)) productDictionary.Add(parameterName, parameterValue);
            }

            products.Add(productDictionary);
        }

        return products;
    }

    public ProductModel CreateProduct(object product)
    {
        var allParametersDictionary = new Dictionary<string, string>();

        var productProperties = product.GetType().GetProperties();
        foreach (var productPropertyInfo in productProperties)
        {
            var parameterValue = productPropertyInfo.GetValue(product)?.ToString();
            if (productPropertyInfo.Name is "AllParameters" or "Products" or "FilterParametersList")
            {
                continue;
            }
            var parameterName = productPropertyInfo.Name;

            if (!string.IsNullOrEmpty(parameterValue)) allParametersDictionary.Add(parameterName, parameterValue);
        }

        return new ProductModel
        {
            AllParameters = allParametersDictionary
        };
    }
}