using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Rainbowhut1._0.Domain;
using Rainbowhut1._0.Persistances;
using Rainbowhut1._0.Persistances.Repositories.Common;
using Rainbowhut1._0.Persistances.Repository;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(o => o.AddPolicy("RainbowhutPolicy", builder =>
{
    builder.WithOrigins("http://rainbowhut.in/").WithOrigins("http://dev_rainbowhut.in/")
   // builder.WithOrigins("http://localhost:4200/")
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(
//        builder =>
//        {

//            //you can configure your custom policy
//            builder.AllowAnyOrigin()
//                                .AllowAnyHeader()
//                                .AllowAnyMethod();
//        });
//});
builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/build";
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rainbowhut API", Version = "v1" });
});
builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(DbContext), typeof(ApplicationDbContext));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<IUploadDataRepository, UploadDataRepository>();
builder.Services.AddTransient<UploadDataDomain>();
builder.Services.AddTransient<MailDomain>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("RainbowhutPolicy");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//app.UseCors(x => x
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader());
app.UseStaticFiles();
app.UseSpaStaticFiles();

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseReactDevelopmentServer(npmScript: "start");
    }
});

app.Run();
