using e_commerce.helpers;

var builder = WebApplication.CreateBuilder(args);
//String ConnectionString = builder.Configuration.GetValue<String>("ConnectionStrings.db");
String ConnectionString = "Data Source=DESKTOP-5A0QPBF;Initial Catalog=ecommerce;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
// Add services to the container.
builder.Services.AddRazorPages();
Console.WriteLine("ConnectionString => " + ConnectionString);
db.setDb(ConnectionString);
var app = builder.Build();

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
    pattern: "admin/{controller}/{action}/{id?}",
    defaults: new { controller = "Welcome", action = "Index" });
    endpoints.MapControllers();
});

app.UseAuthorization();

app.MapRazorPages();

app.Run();
