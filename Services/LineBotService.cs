using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using LineBotWebApi.Models;

namespace LineBotWebApi.Services;

public class LineBotService
{
    private readonly HttpClient _httpClient;
    private readonly string _channelAccessToken;
    private const string ReplyEndpoint = "https://api.line.me/v2/bot/message/reply";

    public LineBotService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _channelAccessToken = configuration["LineBot:ChannelAccessToken"]
            ?? throw new InvalidOperationException("ChannelAccessToken is not configured.");
    }

    public async Task ReplyMessageAsync(string replyToken, string message)
    {
        var reply = new LineReplyMessage
        {
            ReplyToken = replyToken,
            Messages = new List<ReplyMessageBody>
            {
                new ReplyMessageBody { Type = "text", Text = message }
            }
        };

        var json = JsonSerializer.Serialize(reply, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var request = new HttpRequestMessage(HttpMethod.Post, ReplyEndpoint)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _channelAccessToken);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}
