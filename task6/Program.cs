using Microsoft.EntityFrameworkCore;
using task6.Controllers;
using task6.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .Options;

using (var context = new AppDbContext(options))
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<DrawingHub>("/drawinghub");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=DrawTable}/{id?}");
});

app.UseAuthorization();


app.Run();