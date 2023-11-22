using BOOKS.Data;
using BOOKS.Services.Interfaces;
using BOOKS.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//��� ��������� ������-��-������
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddTransient<IBooksService, BooksService>();
//builder.Services.AddSingleton<MyDataContext>();   //��� ������ ��� ��

//�������� ������ ����������� � �� �� ����� ������������
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//��������� �������� MyDataContext � �������� ������� � ����������
builder.Services.AddDbContext<MyDataContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
