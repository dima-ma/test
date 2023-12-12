using Exchanger.Application.Common.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

builder.Services.AddProblemDetails(
    x =>
    {
        x.ShouldLogUnhandledException = (httpContext, exception, problemDetails) =>
        {
            var env = httpContext.RequestServices.GetRequiredService<IHostEnvironment>();
            return env.IsDevelopment() || env.IsStaging();
        };

        x.IncludeExceptionDetails = (ctx, _) =>
        {
            var env = ctx.RequestServices.GetRequiredService<IHostEnvironment>();
            return env.IsDevelopment() || env.IsStaging();
        };
        
        x.Map<ValidationException>(ex => new ProblemDetails()
        {
            Title = ex.GetType().Name,
            Status = StatusCodes.Status400BadRequest,
            Detail = ex.Message,
            Type = "https://somedomain/application-rule-validation-error"
        });
        
        x.Map<KeyNotFoundException>(ex => new ProblemDetails()
        {
            Title = ex.GetType().Name,
            Status = StatusCodes.Status500InternalServerError,
            Detail = ex.Message,
            Type = "https://somedomain/application-rule-validation-error"
        });
        
        x.Map<Exception>(ex => new ProblemDetails()
        {
            Title = ex.GetType().Name,
            Status = StatusCodes.Status400BadRequest,
            Detail = ex.Message,
            Type = "https://somedomain/application-rule-validation-error"
        });
        
        x.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
        x.MapStatusCode = context => new StatusCodeProblemDetails(context.Response.StatusCode);
    }
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseProblemDetails();

app.Run();