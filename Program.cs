using Corporate.Components;
using Corporate.Data;
using Corporate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// ---------- БД ----------
builder.Services.AddDbContext<CorporateDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------- Identity ----------
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<CorporateDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanManageEvents", p => p.RequireRole("Admin", "Organizer"));
    options.AddPolicy("CanManageEmployees", p => p.RequireRole("Admin"));
    options.AddPolicy("CanManageVenues", p => p.RequireRole("Admin", "Organizer"));
    options.AddPolicy("Authenticated", p => p.RequireAuthenticatedUser());
});

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// ---------- Blazor ----------
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// ---------- HTTP pipeline ----------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.MapPost("/account/login", async (
    [FromForm] string email,
    [FromForm] string password,
    SignInManager<AppUser> signInManager) =>
{
    var result = await signInManager.PasswordSignInAsync(
        email, password,
        isPersistent: false,
        lockoutOnFailure: false);

    return result.Succeeded
        ? Results.Redirect("/")
        : Results.Redirect("/login?error=1");
})
.DisableAntiforgery();

app.MapPost("/account/logout", async (SignInManager<AppUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/");
})
.DisableAntiforgery();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    string[] roles = { "Admin", "Organizer", "Employee" };

    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    // admin user
    var adminEmail = "admin@corp.local";
    var admin = await userManager.FindByEmailAsync(adminEmail);
    if (admin == null)
    {
        admin = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullName = "Администратор",
            DepartmentId = 1, // важно: существующий DepartmentId
            Position = "Администратор"
        };

        var create = await userManager.CreateAsync(admin, "Admin123!");
        if (create.Succeeded)
            await userManager.AddToRoleAsync(admin, "Admin");
    }
}
app.Run();
