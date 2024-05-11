using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PassIn.Api.Filters;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Interfaces.Attendees;
using PassIn.Infrastructure.Interfaces.Events;
using PassIn.Infrastructure.Repositories.Attendees;
using PassIn.Infrastructure.Repositories.Events;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IAttendeesRepository, AttendeeRepository>();

builder.Services.AddMvc(opt => opt.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddDbContext<PassInDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
