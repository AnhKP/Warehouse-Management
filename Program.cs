using PRN221_Project_02;
using PRN221_Project_02.Models;
var builder = WebApplication.CreateBuilder(args);

// Thêm Razor Pages và các dịch vụ khác
builder.Services.AddRazorPages();


// Thêm dịch vụ cho Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn của session (30 phút)
    options.Cookie.HttpOnly = true; // Chỉ cho phép session qua HTTP, không qua JavaScript
    options.Cookie.IsEssential = true; // Đảm bảo session được sử dụng ngay cả khi cookie là không cần thiết
});

// Thêm DbContext cho cơ sở dữ liệu
builder.Services.AddDbContext<PRN221_Project_02Context>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Thêm middleware cho session
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

// Đặt trang mặc định khi truy cập "/"
app.MapGet("/", context =>
{
    context.Response.Redirect("/Common_Page/SignUp");
    return Task.CompletedTask;
});

app.Run();
