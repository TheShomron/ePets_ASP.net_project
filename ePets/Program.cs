using ePets.Data;
using ePets.Data.Cart;
using ePets.Models;
using ePets.Repositories;
using ePets.Repositories.Order_Repository;
using ePets.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IAnimalService, AnimalService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));
builder.Services.AddSession();

builder.Services.AddIdentity<ApplicationUser , IdentityRole>().AddEntityFrameworkStores<MyAppDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(opt=> { opt.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme; });
string connectionString = "Server=(localdb)\\PetDb;Database=PShop;Trusted_Connection=True;";
builder.Services.AddDbContext<MyAppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<MyAppDbContext>();
    //ctx.Database.EnsureDeleted();
    //ctx.Database.EnsureCreated();

    var role = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await role.RoleExistsAsync(UserRoles.Admin))
        await role.CreateAsync(new IdentityRole(UserRoles.Admin));
    if (!await role.RoleExistsAsync(UserRoles.User))
        await role.CreateAsync(new IdentityRole(UserRoles.User));
 

    //user
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string adminUserEmail = "admin@ePets.com";
    var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
    if(adminUser == null)
    {
        var newAdminUser = new ApplicationUser()
        {
            FullName = "Admin User",
            UserName= "Admin",
            Email = adminUserEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(newAdminUser, "Coding@1234?");
        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
    }

    string simpleUserEmail = "User@ePets.com";
    var simpleUser = await userManager.FindByEmailAsync(simpleUserEmail);
    if (simpleUser == null)
    {
        var newAppUser = new ApplicationUser()
        {
            FullName = "App User",
            UserName= "User",
            Email = simpleUserEmail,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(newAppUser, "Coding@1234?");
        await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
