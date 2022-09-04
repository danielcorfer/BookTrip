using System.Globalization;
using System.Text;
using BookTrip.Database;
using BookTrip.Interfaces;
using BookTrip.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IPropertyTypeService, PropertyTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IDbContext, AppDbContext>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRoomReservationService, RoomReservationService>();
builder.Services.AddScoped<ITaxiService, TaxiService>();

// Add service to the container

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddLocalization(s => s.ResourcesPath = "Resources");


// Add service to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = false;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(Environment.GetEnvironmentVariable("Booktrip_Token"))),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
builder.Services.AddHttpContextAccessor();



ConfigureDb(builder.Services);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();


app.MapControllers();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

//Configure Database
static void ConfigureDb(IServiceCollection services)
{
    var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
    var connectionString = Environment.GetEnvironmentVariable("ConnectionString_Default");
    services.AddDbContext<AppDbContext>(b => b.UseSqlServer(connectionString));
}

//Configure Localization
static void ConfigureServices(IServiceCollection services)
{
    services.AddLocalization(s => s.ResourcesPath = "Resources");
    var supportedCultures = new CultureInfo[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("cs"),
        new CultureInfo("sk"),
    };

    services.Configure<RequestLocalizationOptions>(s =>
    {
        s.SupportedCultures = supportedCultures;
        s.SupportedUICultures = supportedCultures;
        // DefaultRequestCulture is used if none of the built-in providers can determine the request culture!
        s.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-Us");
    });

    services.AddMvc()
        .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
        .AddDataAnnotationsLocalization();
}


static void Configure(IApplicationBuilder app, IHostEnvironment env, ILoggerFactory loggerFactory)
{
    app.UseStaticFiles();

    // Using localization 
    var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
    app.UseRequestLocalization(locOptions.Value);

    app.UseMvc();
}


public partial class Program { }