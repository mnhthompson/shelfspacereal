

using ShelfSpace.Services;

var builder = WebApplication.CreateBuilder(args);

var dbconnectionstring = builder.Configuration.GetConnectionString("ShelfSpaceContext"); 
Console.WriteLine(dbconnectionstring);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<TestAppData>();


builder.Services.AddScoped<MediaState>();
builder.Services.AddScoped<UserState>();

builder.Services.AddDbContext<ShelfSpaceContext>(options =>
    options.UseSqlServer(dbconnectionstring));

var app = builder.Build();
    //Testing the Database Connection
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ShelfSpaceContext>();
            
            // Testing the connection:
            await dbContext.Database.CanConnectAsync();
            Console.WriteLine("Database connection successful.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Media}/{action=Index}/{id?}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
