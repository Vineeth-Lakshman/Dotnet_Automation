using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register an In-Memory Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TodoListDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- ENDPOINTS ---

// GET: Fetch all todos
app.MapGet("/api/todos", async (AppDbContext db) =>
    await db.Todos.ToListAsync());

// GET: Fetch single todo by ID
app.MapGet("/api/todos/{id}", async (int id, AppDbContext db) =>
    await db.Todos.FindAsync(id) is TodoItem item ? Results.Ok(item) : Results.NotFound());

// POST: Create a new todo
app.MapPost("/api/todos", async (TodoItem item, AppDbContext db) =>
{
    db.Todos.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todos/{item.Id}", item);
});

// PUT: Update a todo
app.MapPut("/api/todos/{id}", async (int id, TodoItem inputItem, AppDbContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Title = inputItem.Title;
    todo.IsCompleted = inputItem.IsCompleted;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// DELETE: Remove a todo
app.MapDelete("/api/todos/{id}", async (int id, AppDbContext db) =>
{
    if (await db.Todos.FindAsync(id) is TodoItem item)
    {
        db.Todos.Remove(item);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();

// --- DATA MODELS & DB CONTEXT ---

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<TodoItem> Todos => Set<TodoItem>();
}