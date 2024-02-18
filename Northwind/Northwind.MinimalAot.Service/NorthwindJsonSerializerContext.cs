using System.Text.Json.Serialization;
using Models;

[JsonSerializable(typeof(Product))]
[JsonSerializable(typeof(List<Product>))]
internal partial class NorthwindJsonSerializerContext : JsonSerializerContext { }
