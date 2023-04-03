using HRManagement;
using HRManagement.Models.Mapper;
using HRManagement.Services;
using HRManagement.Services.Interfaces;
using HRManagement.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<HRManagementContext>(builder.Configuration.GetConnectionString("connectionHRManagement"));

builder.Services.AddScoped<IPersonService, PersonServiceImpl>();
builder.Services.AddScoped<IWorkerService, WorkerServiceImpl>();
builder.Services.AddScoped<ISalaryService, SalaryServiceImpl>();

builder.Services.AddScoped<PersonMapper>();
builder.Services.AddScoped<WorkerMapper>();
builder.Services.AddScoped<SalaryMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
