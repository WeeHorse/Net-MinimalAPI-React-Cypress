using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text.Json;
using System.Dynamic;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// db
string connectionString = "uid=root;pwd=Mysql@123;database=homenet;server=localhost;port=3306";


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
app.UseRouting();

app.MapGet("/api/adresser", async () =>
{
    var results = new List<dynamic>();
    using (var connection = new MySqlConnection(connectionString))
    {
        await connection.OpenAsync();
        using (var command = new MySqlCommand("SELECT * FROM adresser", connection))
        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                var data = new ExpandoObject() as IDictionary<string, Object>;
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    data.Add(reader.GetName(i), reader[i]);
                }
                results.Add(data);
            }
        }
    }
    return Results.Json(results);
});

app.Run();