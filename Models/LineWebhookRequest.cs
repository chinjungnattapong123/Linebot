namespace LineBotWebApi.Models;

public class LineWebhookRequest
{
    public string? Destination { get; set; }
    public List<WebhookEvent> Events { get; set; } = new();
}

public class WebhookEvent
{
    public string? ReplyToken { get; set; }
    public string? Type { get; set; }
    public EventSource? Source { get; set; }
    public MessageObject? Message { get; set; }
    public long Timestamp { get; set; }
}

public class EventSource
{
    public string? Type { get; set; }
    public string? UserId { get; set; }
    public string? GroupId { get; set; }
    public string? RoomId { get; set; }
}

public class MessageObject
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public string? Text { get; set; }
}
