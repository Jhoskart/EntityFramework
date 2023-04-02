using entityFramework.Models;
using Microsoft.EntityFrameworkCore;


namespace entityFramework;

public class TareasContext : DbContext
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() {    
            CategoriaId = Guid.Parse("4ed10ffb-a8cf-4360-953f-1173f0a1c2e0"), 
            Nombre = "Actividades pendientes", 
            Descripcion = "Descripción de la categoría 1", 
            Peso = 20
        });

        categoriasInit.Add(new Categoria() {    
            CategoriaId = Guid.Parse("4ed10ffb-a8cf-4360-953f-1173f0a1c202"), 
            Nombre = "Actividades personales", 
            Descripcion = "Descripción de la categoría 1", 
            Peso = 50
        });

        modelBuilder.Entity<Categoria>(categoria => {
            categoria.ToTable("Categoria");
            categoria.HasKey(c => c.CategoriaId);
            categoria.Property(c => c.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(c => c.Descripcion).IsRequired(false);
            categoria.Property(c => c.Peso);
            categoria.HasData(categoriasInit);
            // categoria.HasMany(c => c.Tareas).WithOne(t => t.Categoria).HasForeignKey(t => t.CategoriaId);
        });

        modelBuilder.Entity<Tarea>(tarea => {

            List<Tarea> tareasInit = new List<Tarea>();

            tareasInit.Add(new Tarea() {    
            TareaId = Guid.Parse("4ed10ffb-a8cf-4360-953f-1173f0a1c210"),
            CategoriaId = Guid.Parse("4ed10ffb-a8cf-4360-953f-1173f0a1c2e0"),
            Titulo = "Revisar pago de servicios publicos",
            PrioridadTarea = Prioridad.Media,
            FechaCreacion = DateTime.Now
            });

            tareasInit.Add(new Tarea() {    
            TareaId = Guid.Parse("4ed10ffb-a8cf-4360-953f-1173f0a1c211"),
            CategoriaId = Guid.Parse("4ed10ffb-a8cf-4360-953f-1173f0a1c202"),
            Titulo = "Terminar de ver peliculas de Marvel",
            PrioridadTarea = Prioridad.Baja,
            FechaCreacion = DateTime.Now
            });

            tarea.ToTable("Tarea");
            tarea.HasKey(t => t.TareaId);
            tarea.Property(t => t.Titulo).IsRequired().HasMaxLength(200);
            tarea.Property(t => t.Descripcion).IsRequired(false);
            tarea.Property(t => t.PrioridadTarea);
            tarea.Property(t => t.FechaCreacion);
            tarea.HasOne(t => t.Categoria).WithMany(c => c.Tareas).HasForeignKey(t => t.CategoriaId);
            tarea.HasData(tareasInit);
            tarea.Ignore(t => t.Resumen);
        });
    }

}