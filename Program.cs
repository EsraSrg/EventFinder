using EventFinder.Data;
using EventFinder.Helpers;
using EventFinder.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEventRepository,EventRepository>();
builder.Services.AddScoped<IDashboardRepository,DashboardRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IPhotoService,PhotoService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));



builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection"));
});

//  Identity servislerini ekle

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie();

var app = builder.Build();


if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    await Seed.SeedUsersAndRolesAsync(app);
    // Seed.SeedData(app);
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Önce kimlik doğrulama
app.UseAuthorization();  // Sonra yetkilendirme
app.UseSession();        // Session kullanılıyorsa ekle


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); // Geliştirme modunda hata detayları görmek için
}




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
