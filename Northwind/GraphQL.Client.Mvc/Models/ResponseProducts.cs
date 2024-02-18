using EntityModels;

namespace GraphQL.Client.Mvc.Models;

public class ResponseProducts
{
    public class DataProducts
    {
        public Product[]? ProductsInCategory { get; set; }
    }

    public DataProducts? Data { get; set; }
}
