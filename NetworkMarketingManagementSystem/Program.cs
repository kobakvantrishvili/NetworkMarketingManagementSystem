using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using NetworkMarketingManagementSystem.Infrastructure.Extensions;
using NetworkMarketingManagementSystem.Infrastructure.Mapping;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Models;
using NetworkMarketingManagementSystem.Persistence.MSSQL;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddOData(options =>
{
    options.Select().Filter().OrderBy();
});
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.RegisterMapsterMappings();
builder.Services.Configure<BonusStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(BonusStoreDatabaseSettings)));
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("BonusStoreDatabaseSettings:ConnectionString")));
builder.Services.AddServiceExtensions();
builder.Services.AddSwaggerGen(options =>
            {
                // defining docs to be created by swagger generator
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "NMMS API", Version = "v1" });

                // For XML comments
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NMMS API"));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
