using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("UserDB"));

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

app.MapGet("/Users", async (UserDbContext db) => await db.Users.AsNoTracking().ToListAsync()).WithName("Users").WithOpenApi();

app.MapPost("/Users", async ([FromBody] User user, UserDbContext db) =>
{
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/Users/{user.id}", user);
}).WithName("addUser").WithOpenApi();

app.MapGet("Users/{id}", async(int id, UserDbContext db)=> 
    await db.Users.FindAsync(id) is User user?Results.Ok(user):Results.NotFound()).WithName("findUser").WithOpenApi();

app.MapDelete("/Users/{id}", async (int id, UserDbContext db) =>
{
    var user = await db.Users.FindAsync(id); 
    if(user != null)
    {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    else
    {
        return Results.NotFound();
    }
}).WithName("deleteUser").WithOpenApi();

app.Run();

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options){}
    public DbSet<User> Users => Set<User>();
}
public class User
{
    public int id { get; set; }
    public string? UserName { get; set; }
    public string?  Email { get; set; }
}
