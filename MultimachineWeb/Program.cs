using MultimachineWeb.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection("ApplicationSettings"));
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
//ApplicationSettings appsettings = new ApplicationSettings();
var appSettings = new ApplicationSettings();
builder.Configuration.GetSection("ApplicationSettings").Bind(appSettings);
builder.Services.AddSingleton(appSettings);

var connStrings = new ConnectionStrings();
builder.Configuration.GetSection("ConnectionStrings").Bind(connStrings);
builder.Services.AddSingleton(connStrings); // Registers the instance directly

builder.Services.AddTransient<ClsUpdate>();


//builder.Services.AddTransient<Multimachine>

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    //pattern: "{controller=Machine}/{action=Index}/{id?}");
    ///Showit
    pattern: "{controller=Machine}/{action=Showit}/{id?}");
app.Run();
