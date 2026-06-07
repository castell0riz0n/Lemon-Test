using LifecycleFeatures.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// DEMO ONLY: Do not use in production
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/api/todos", async (AppDbContext db) =>
    await db.TodoItems.ToListAsync());

app.MapGet("/api/todos/{id:guid}", async (Guid id, AppDbContext db) =>
    await db.TodoItems.FindAsync(id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.MapPost("/api/todos", async (TodoItem todo, AppDbContext db) =>
{
    todo.Id = Guid.NewGuid();
    todo.CreatedAt = DateTime.UtcNow;
    db.TodoItems.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todos/{todo.Id}", todo);
});

// Project endpoints
app.MapGet("/api/projects", async (AppDbContext db) =>
    await db.Projects.ToListAsync());

app.MapGet("/api/projects/{id:guid}", async (Guid id, AppDbContext db) =>
    await db.Projects.FindAsync(id) is { } project
        ? Results.Ok(project)
        : Results.NotFound());

app.MapPost("/api/projects", async (Project project, AppDbContext db) =>
{
    project.Id = Guid.NewGuid();
    project.CreatedAt = DateTime.UtcNow;
    db.Projects.Add(project);
    await db.SaveChangesAsync();
    return Results.Created($"/api/projects/{project.Id}", project);
});

app.Run();