using Admin.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
      policy =>
      {
        policy.WithOrigins("http://localhost:4200") // Allow Angular frontend
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
      });
});

// Register DbContext separately
builder.Services.AddDbContext<ApplicationdbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
  
//Adding Email Service 
builder.Services.AddSingleton<EmailService>();

var app = builder.Build();

// Apply CORS policy
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
