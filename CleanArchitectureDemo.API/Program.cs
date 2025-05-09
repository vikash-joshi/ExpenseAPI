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
/*builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80); // <--- Important for Docker
});
*/
builder.Services.AddInfrastructure("Host=aws-0-us-east-2.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.pdhaciivnsycejerdumq;Password=xq#N?7dqkjQ7H.f;Ssl Mode=Require;Trust Server Certificate=true;");
builder.Services.AddMediatR(typeof(CleanArchitectureDemo.Application.AssemblyReference).Assembly); // Cleaner


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
