using sistema.CustomPolicy;
using sistema.IdentityPolicy;
using sistema.Models;
using sistema.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sistema.Procedure;
using sistema.Repository;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:ContextConnection"]));

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

/////////////BUILD SERVICES
builder.Services.AddScoped<dispositivoProcedure, dispositivoRepository>();
builder.Services.AddScoped<logProcedure, logsRepository>();
builder.Services.AddScoped<mapaProcedure, mapaRepository>();
builder.Services.AddScoped<descontoProcedure, descontoRepository>();
/////////////BUILD SERVICES

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("AspManager", policy =>
    {
        policy.RequireRole("Manager");
        policy.RequireClaim("Coding-Skill", "ASP.NET Core MVC");
    });
});

builder.Services.AddTransient<IAuthorizationHandler, AllowUsersHandler>();
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("AllowTom", policy =>
    {
        policy.AddRequirements(new AllowUserPolicy("tom"));
    });
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    // PASSAGEM DO ID PARA REQUISITAR DO USUARIO PERMISSÕES
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
