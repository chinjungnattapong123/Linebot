using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using LineBotWebApi.Models;
using LineBotWebApi.Services;

namespace LineBotWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhookController : ControllerBase
{
    private readonly LineBotService _lineBotService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<WebhookController> _logger;

    public WebhookController(
        LineBotService lineBotService,
        IConfiguration configuration,
        ILogger<WebhookController> logger)
    {
        _lineBotService = lineBotService;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LineWebhookRequest request)
    {
        // ตรวจสอบ Signature (ควรทำผ่าน middleware ในระบบจริง)
        // สำหรับตัวอย่างนี้ข้ามการ validate signature

        if (request.Events == null || request.Events.Count == 0)
        {
            return Ok(); // LINE ส่ง verify webhook จะไม่มี events
        }

        foreach (var ev in request.Events)
        {
            if (ev.Type == "message" && ev.Message?.Type == "text")
            {
                var userMessage = ev.Message.Text ?? string.Empty;
                var replyText = ProcessMessage(userMessage);

                if (!string.IsNullOrEmpty(ev.ReplyToken))
                {
                    await _lineBotService.ReplyMessageAsync(ev.ReplyToken, replyText);
                }
            }
        }

        return Ok();
    }

    /// <summary>
    /// ประมวลผลข้อความจากผู้ใช้ แล้วตอบกลับ
    /// ปรับแต่ง logic ได้ตามต้องการ
    /// </summary>
    private string ProcessMessage(string userMessage)
    {
        // Echo bot: ตอบกลับข้อความเดิมที่ผู้ใช้ส่งมา
        return $"คุณพิมพ์ว่า: {userMessage}";
    }
}
