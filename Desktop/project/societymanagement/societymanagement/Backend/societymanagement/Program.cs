  using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

using societymanagement.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MemberRepository>();
builder.Services.AddScoped<LoginRepository>();
//builder.Services.AddScoped<ImageController>();


builder.Services.AddScoped<ComplainRepsitory>();
builder.Services.AddScoped<PaymentRepository>();

builder.Services.AddScoped<EventRepository>();
builder.Services.AddScoped<FlatTransferRepository>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});






//Add CORS services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Remove trailing slash
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Allow cookies/auth headers if needed
    });
});


      







var app = builder.Build();

app.UseCors("AllowAngularApp");


app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "imageupload")),
  RequestPath = "/imageupload"
});





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseAuthentication();  // Required for JWT to work


app.MapControllers();

app.Run();
