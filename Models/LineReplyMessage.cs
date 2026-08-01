namespace LineBotWebApi.Models;

public class LineReplyMessage
{
    public string ReplyToken { get; set; } = string.Empty;
    public List<ReplyMessageBody> Messages { get; set; } = new();
}

public class ReplyMessageBody
{
    public string Type { get; set; } = "text";
    public string Text { get; set; } = string.Empty;
}
