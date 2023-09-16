
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyToDoApi.Context;
using MyToDoApi.Extensions;
using MyToDoApi.Repository;
using MyToDoApi.Sevice;
using System.Diagnostics;
using UserMgrWebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IStorageClient, MockCloudStorageClient>();

builder.Services.Configure<MvcOptions>(o =>
{
    //ע��ȫ�ֵ�filter
    o.Filters.Add<UnitOfWorkFilter>();
});

//AutoMapper����
var autoMapperConfigration = new MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperProfile());
});
builder.Services.AddSingleton(autoMapperConfigration.CreateMapper());

//������ʩע��

builder.Services.AddScoped<ToDoService>();
builder.Services.AddScoped<MemoService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IMemoRepository, MemoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IToDoRepository, TodoRepository>();

//���ݿ�
builder.Services.AddDbContext<MyToDoDBContext>(ctx =>
{
    //�����ַ�������ŵ�appsettings.json�У�����й�ܵķ���
    //����ŵ�UserSecrets�У�ÿ����Ŀ��Ҫ���ã����鷳
    //��������Ƽ��ŵ����������С�
    string connStr = "Data Source=LAPTOP-I5RIPDJK\\SQLEXPRESS;Database=MyToDo;Trusted_Connection=True";
    ctx.UseSqlServer(connStr);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
