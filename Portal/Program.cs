using ApplicationServices;
using Core.Domain;
using Core.DomainServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SqlServer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//TODO: Specify bootstrap icon files to download, for load times

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddScoped<IGameNightRepository, GameNightEFRepository>();
builder.Services.AddScoped<IGameRepository, GameEFRepository>();
builder.Services.AddScoped<IUserRepository, UserEFRepository>();
builder.Services.AddScoped<IAllergyRepository, AllergyEFRepository>();

builder.Services.AddScoped<IGameNightService, GameNightService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHelperService, HelperService>();

builder.Services.AddDbContext<DomainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Domain"))
        //TODO: remove in Production 
        .EnableSensitiveDataLogging());

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Identity"))
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyOrganizers", policy => policy
        .RequireAuthenticatedUser()
        .RequireClaim("UserType", UserType.Organizer.ToString()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();