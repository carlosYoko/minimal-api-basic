var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();

List<Property> properties = new List<Property>()
{
    new Property { Id = 1, Description = "Casa adosada", Rooms = 7 },
    new Property { Id = 2, Description = "Piso mediano", Rooms = 4 }
};

// Endpoints
app.MapGet("/api/properties", () =>
{
    return properties;
});

app.Run();

class Property
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Rooms { get; set; }
}