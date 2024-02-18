using Queue.Models;

namespace WebApi.Client.Mvc.Models;

public class HomeSendMessageViewModel
{
    public string? Info { get; set; }
    public string? Error { get; set; }
    public ProductQueueMessage? Message { get; set; }
}
