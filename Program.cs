using entityFramework;
using entityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    await dbContext.Database.EnsureCreatedAsync();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(
        await dbContext.Tareas.Include(p => p.Categoria).ToListAsync()
    );
});

app.MapGet("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, Guid id) =>
{
    return Results.Ok(
        await dbContext.Tareas.Include(p => p.Categoria).Where(t => t.TareaId == id).FirstOrDefaultAsync()
    );
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
        tarea.TareaId = Guid.NewGuid();
        tarea.FechaCreacion = DateTime.Now;
        await dbContext.Tareas.AddAsync(tarea);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
        tarea.TareaId = Guid.NewGuid();
        tarea.FechaCreacion = DateTime.Now;
        await dbContext.Tareas.AddAsync(tarea);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
});

app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{

    var tareaActual = await dbContext.Tareas.FindAsync(id);
    if(tareaActual != null)
    {

        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) =>
{
    var tareaActual = await dbContext.Tareas.FindAsync(id);
    if(tareaActual != null)
    {
        dbContext.Tareas.Remove(tareaActual);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();
