using Microsoft.Extensions.FileProviders;
using System.IO;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// db
State state = new("uid=root;pwd=;database=homenet;server=localhost;port=3307");
builder.Services.AddSingleton(state);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// configure defaults
app.UseDefaultFiles(new DefaultFilesOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "frontend")),
    RequestPath = ""
});

// Configure custom static files
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "frontend")),
    RequestPath = ""
});

// routes

app.MapGet("/", (State state) =>
{
    using var reader = MySqlHelper.ExecuteReader(
    state.DB,
    "SELECT * FROM adresser"
    );
    var results = DataTableToList(reader);
    return Results.Json(results);
});


app.UseRouting();

app.Run();