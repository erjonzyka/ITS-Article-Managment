using Microsoft.EntityFrameworkCore;
using ITS_Library;
using ITS_Library.Interfaces;
using ITS_Library.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<WebScrappingService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

var httpClient = app.Services.GetRequiredService<HttpClient>();

httpClient.Timeout = TimeSpan.FromSeconds(30);



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
