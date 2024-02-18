using System.ComponentModel.DataAnnotations.Schema;

namespace EntityModels;

public partial class Employee : IHasLastRefreshed
{
    [NotMapped]
    public DateTimeOffset LastRefreshed { get; set; }
}
