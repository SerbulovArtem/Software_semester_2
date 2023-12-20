using ActionManager.BLL.Repositories.Concreate.DataBaseMCSQLActionManager;
using ActionManager.DAL.Data;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using ActionManager.DAL.Repositories.Concreate.DataBaseMCSQLActionManager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WEB_MVC.App;
using WEB_MVC.Areas.Identity.Data;
using WEB_MVC.Data;
using WEB_MVC.Static;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("WEB_MVCContextConnection") ?? throw new InvalidOperationException("Connection string 'WEB_MVCContextConnection' not found.");

builder.Services.AddDbContext<WEB_MVCContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ActionManagerContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<WEB_MVCUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<WEB_MVCContext>();
builder.Services.AddScoped<IActionsRepository, ActionManagerActionsRepository>();
builder.Services.AddScoped<IProductsRepository, ActionManagerProductsRepository>();
builder.Services.AddScoped<ITypeActionsRepository, ActionManagerTypeActionsRepository>();

builder.Services.AddScoped<IUsersRepository, ActionManagerUserRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.MapControllerRoute(
    name: "cart",
    pattern: "{controller=Cart}/{action=Index}/{id?}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// ...

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = Enum.GetNames(typeof(Roles));

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}



app.Run();
