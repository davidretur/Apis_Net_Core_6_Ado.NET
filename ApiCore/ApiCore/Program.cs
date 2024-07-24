var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// agregando mis reglas cors
var MisReglasCors = "ReglasCors";

builder.Services.AddCors(option =>
option.AddPolicy(name: MisReglasCors,
builder =>
{
    // permitir cualquier origen, permitir cualquier cabecera, y cualquier metodo -> put, get,post, delete
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader();
})
    
    );
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MisReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
