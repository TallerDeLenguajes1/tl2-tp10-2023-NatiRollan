using EspacioKanban;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

var CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();//el signo de exclamacion es para decir que no es null
builder.Services.AddSingleton<string>(CadenaDeConexion);

// Inyeccion de dependencias
builder.Services.AddScoped<IUsuarioRepository,UsuarioRepository>();
builder.Services.AddScoped<ITableroRepository,TableroRepository>();
builder.Services.AddScoped<ITareaRepository,TareaRepository>();

builder.Services.AddSession(options =>  //// se agrega para las variables de session
{
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Las inyecciones de dependencia tiene que ir antes de esto 
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
app.UseSession();   // se le agrega para las variables de sesion
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
