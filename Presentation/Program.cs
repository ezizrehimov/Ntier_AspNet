
using Business.Services.Abstract;
using Business.Services.Concrete;
using Common.Entities;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitofWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


var connectionSting = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionSting, x => x.MigrationsAssembly(nameof(DataAccess))));
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();










var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}"
    );

app.Run();