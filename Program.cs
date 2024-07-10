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
    new Property { Id = Guid.NewGuid(), Description = "Casa adosada", Rooms = 7 },
    new Property { Id = Guid.NewGuid(), Description = "Piso mediano", Rooms = 4 }
};

// Endpoints
app.MapGet("/api/properties", () =>
{
    return properties;
});

app.MapPost("/api/properties", (Property newProperty) =>
{
    if (newProperty.Description == "" || newProperty.Rooms < 0) return Results.BadRequest("Todos los campos son obligatorios");

    newProperty.Id = Guid.NewGuid();
    properties.Add(newProperty);
    return Results.Created($"/api/properties/{newProperty.Id}", newProperty);
});

app.MapPut("/api/properties/{id}", (Guid id, PropertyDto updateProperty) =>
{
    var propertyToUpdate = properties.FirstOrDefault(p => p.Id == id);

    if (propertyToUpdate == null) return Results.BadRequest("No se encuentra la propiedad");

    propertyToUpdate.Description = updateProperty.Description;
    propertyToUpdate.Rooms = updateProperty.Rooms;

    return Results.Ok($"Se ha modificado la propiedad: {updateProperty.Description}");
});

app.MapDelete("/api/properties/{id}", (Guid id) =>
{
    var propertyToDelete = properties.FirstOrDefault(p => p.Id == id);

    if (propertyToDelete == null) return Results.BadRequest("No se encuentra la propiedad");

    properties.Remove(propertyToDelete);

    return Results.Ok($"Se ha eliminado la propiedad: {propertyToDelete.Description}");
});

app.Run();

class Property
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Rooms { get; set; }
}

class PropertyDto
{
    public string Description { get; set; } = string.Empty;
    public int Rooms { get; set; }
}