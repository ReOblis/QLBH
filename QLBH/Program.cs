using Microsoft.EntityFrameworkCore;
using QLBH.Data;
using Microsoft.Extensions.DependencyInjection;
using QLBH.Services;
using Microsoft.AspNetCore.Builder;
using OfficeOpenXml;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'QLNSContext' not found.")));

ExcelPackage.LicenseContext = LicenseContext.NonCommercial; 

var app = builder.Build();
var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
var excelService = new ExcelService(dbContext);
var excelFilePath = Path.Combine("ExcelFiles", "excel.xlsx");
excelService.ImportDataFromExcel(excelFilePath);

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
    pattern: "{controller=Novel}/{action=Index}/{id?}");

app.Run();
