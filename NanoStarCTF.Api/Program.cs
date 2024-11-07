using NanoStarCTF.Api.Extensions;
using NanoStarCTF.Api.Middlewares;
using NanoStarCTF.Service.Exceptions;
using NanoStarCTF.Service.Helpers;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

builder.Services.AddSignalR();

// Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


//------------- Functions from Middleware ------------------//

builder.Services.AddDBConTextes(builder.Configuration);
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddServices();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();


//--------------------------------------------------




// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();

// Set helpers
EnvironmentHelper.WebRootPath = app.Services.GetRequiredService<IWebHostEnvironment>().WebRootPath;

if (app.Services.GetService<IHttpContextAccessor>() != null)
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();

app.UseMiddleware<NanoStarCTFExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();
app.Run();
