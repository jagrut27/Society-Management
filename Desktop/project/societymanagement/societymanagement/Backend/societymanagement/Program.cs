using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using societymanagement.Controllers;
using societymanagement.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MemberRepository>();
builder.Services.AddScoped<LoginRepository>();
//builder.Services.AddScoped<ImageRepository>();

//builder.Services.AddScoped<LoginController>();





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




//var key = Encoding.UTF8.GetBytes("ThisIsASecretKeyForJWTAuthentication"); // Use a secure key




var app = builder.Build();

app.UseCors("AllowAngularApp");



//app.UseCors("AllowAngularApp");


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
