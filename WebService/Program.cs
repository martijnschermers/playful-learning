using System.Text;
using Core.DomainServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SqlServer.Infrastructure;
using WebService.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IGameNightRepository, GameNightEFRepository>();
builder.Services.AddScoped<IUserRepository, UserEFRepository>();
builder.Services.AddScoped<IGameRepository, GameEFRepository>();

builder.Services.AddScoped<IGameNightService, GameNightService>();

builder.Services.AddDbContext<DomainDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Domain")));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
        options.TokenValidationParameters.IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGraphQLServer()
    // .AddAuthorization()
    .RegisterDbContext<DomainDbContext>()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();
app.MapControllers();

app.Run();