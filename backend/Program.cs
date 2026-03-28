using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "NoteAPI";
    config.Title = "NoteAPI v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "NoteAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

app.MapGet("/", () => "Hello World!");

app.MapGet("/notes", async () =>
{
    using var conn = new NpgsqlConnection(connectionString);
    var sql = "SELECT * FROM Notes";

    var notes = await conn.QueryAsync<Note>(sql);
    return Results.Ok(notes);
});

app.MapGet("/notes/{id}", async (int id) =>
{
    using var conn = new NpgsqlConnection(connectionString);
    var sql = "SELECT * FROM Notes WHERE Id = @Id";

    var note = await conn.QueryAsync<Note>(sql, new {Id = id});

    return note is not null ? Results.Ok(note) : Results.NotFound();
});
    
app.MapPost("/notes", async (NoteDto dto) =>
{
    using var conn = new NpgsqlConnection(connectionString);
    var sql = "INSERT INTO Notes (Title, Content) VALUES (@Title, @Content) RETURNING id";

    var note = new Note
    {
        Title = dto.Title,
        Content = dto.Content,
    };

    var id = await conn.ExecuteScalarAsync<int>(sql, note);
    note.Id = id;

    return Results.Created($"/notes/{note.Id}", note);
});

app.MapPut("/notes/{id}", async (int id, NoteDto dto) =>
{
    using var conn = new NpgsqlConnection(connectionString);
    var sql = "UPDATE Notes SET Title = @Title, Content = @Content, UpdatedAt = now() WHERE Id = @Id";

    var rows = await conn.ExecuteAsync(sql, new {Id = id, dto.Title, dto.Content});

    return rows > 0 ? Results.NoContent(): Results.NotFound();
});

app.MapDelete("/notes/{id}", async (int id) =>
{
    using var conn = new NpgsqlConnection(connectionString);
    var sql = "DELETE FROM Notes WHERE Id = @Id";

    var rows = await conn.ExecuteAsync(sql, new {Id = id});

    return rows > 0 ? Results.NoContent() : Results.NotFound();
});

app.Run();
