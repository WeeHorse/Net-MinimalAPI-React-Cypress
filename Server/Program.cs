using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

// db
string connectionString = "uid=root;pwd=;database=homenet;server=localhost;port=3307";

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

app.MapGet("/", async () =>
{
    using var data = MySqlHelper.ExecuteQueryAsync(
    connectionString,
    "SELECT * FROM adresser"
    );
    var results = DataTableToList(data);
    return Results.Json(results);
});


app.UseRouting();

app.Run();