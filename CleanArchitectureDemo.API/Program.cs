using MediatR;
using CleanArchitectureDemo.Infrastructure;
using CleanArchitectureDemo.Application;

/*
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddMediatR(typeof(CleanArchitectureDemo.Application.AssemblyReference).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
*/

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("PostgressConnection"));
//builder.Services.AddMediatR(typeof(CleanArchitectureDemo.Application.AssemblyReference).Assembly); // Cleaner
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CleanArchitectureDemo.Application.AssemblyReference>();
});

// Swagger (only for dev)
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<GlobalExceptionHandler>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
