using LineBotWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// กำหนด port ให้ชัดเจน
builder.WebHost.UseUrls("http://localhost:5000");

// Add services
builder.Services.AddControllers();
builder.Services.AddHttpClient<LineBotService>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

// เพิ่ม endpoint ทดสอบ
app.MapGet("/", () => "LINE Bot is running!");

app.Run();
