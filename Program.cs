using DinkToPdf.Contracts;
using DinkToPdf;
using e_commerce.helpers;
using Microsoft.Extensions.FileProviders;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
//String ConnectionString = builder.Configuration.GetValue<String>("ConnectionStrings.db");
String ConnectionString = "Data Source=DESKTOP-5A0QPBF;Initial Catalog=ecommerce;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddRazorTemplating();
Console.WriteLine("ConnectionString => " + ConnectionString);
db.setDb(ConnectionString);
var app = builder.Build();
//var env = app.Environment;

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(env.ContentRootPath, "Exports")),
//    RequestPath = "/Files"
//});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new { controller = "Home", action = "Index" });
    endpoints.MapControllers();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "admin",
    pattern: "{subFolder}/{controller}/{action}/{id?}",
    defaults: new { subFolder="admin" , controller = "Welcome", action = "Index" });
    endpoints.MapControllers();
});


app.UseAuthorization();

app.MapRazorPages();

app.Run();
