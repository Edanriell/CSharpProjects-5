namespace EntityModels;

public interface IHasLastRefreshed
{
    DateTimeOffset LastRefreshed { get; set; }
}
