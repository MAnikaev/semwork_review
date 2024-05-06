using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ZooHelp;
using ZooHelp.Services;
using ZooHelp.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddSignalR();
builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = "MyAuthIssuer",
            ValidAudience = "MyAuthAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authorization:JWTKey"]!))
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpClient<ImageService>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.MapControllers();
app.UseSerilogRequestLogging();
app.MapHub<ChatHub>("/chatHub");
app.UseAuthentication();
app.UseAuthorization();
app.Run();

