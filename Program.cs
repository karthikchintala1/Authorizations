using AuthorizationsTest.Authorization.ProductAccess;
using AuthorizationsTest.Core;
using AuthorizationsTest.Core.UserAccess;
using AuthorizationsTest.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PremiumOnly", policy => policy.AddRequirements(new ProductAccessRequirement(new string[]
    {
        Constants.ProductCodes.PremiumUser
    })));
    
    options.AddPolicy("StandardOnly", policy => policy.AddRequirements(new ProductAccessRequirement(new string[]
    {
        Constants.ProductCodes.StandardUser
    })));

    options.AddPolicy("LimitedOnly", policy => policy.AddRequirements(new ProductAccessRequirement(new string[]
    {
        Constants.ProductCodes.LimitedUser
    })));

    options.AddPolicy("PremiumAndStandard", policy => policy.AddRequirements(new ProductAccessRequirement(new string[]
    {
        Constants.ProductCodes.StandardUser,
        Constants.ProductCodes.PremiumUser
    })));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

// DI
builder.Services.AddSingleton<IUserAccessRepository, UserAccessRepository>();
builder.Services.AddSingleton<IAuthorizationHandler, ProductAccessHandler>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapRazorPages();

app.Run();
