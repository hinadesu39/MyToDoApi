
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
    //注册全局的filter
    o.Filters.Add<UnitOfWorkFilter>();
});

//AutoMapper服务
var autoMapperConfigration = new MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperProfile());
});
builder.Services.AddSingleton(autoMapperConfigration.CreateMapper());

//基础设施注入

builder.Services.AddScoped<ToDoService>();
builder.Services.AddScoped<MemoService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IMemoRepository, MemoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IToDoRepository, TodoRepository>();

//数据库
builder.Services.AddDbContext<MyToDoDBContext>(ctx =>
{
    //连接字符串如果放到appsettings.json中，会有泄密的风险
    //如果放到UserSecrets中，每个项目都要配置，很麻烦
    //因此这里推荐放到环境变量中。
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
