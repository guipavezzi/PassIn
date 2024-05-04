using PassIn.Api.Filters;
using PassIn.Infrastructure.Interfaces.Events;
using PassIn.Infrastructure.Repositories.Events;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddMvc(opt => opt.Filters.Add(typeof(ExceptionFilter)));

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
