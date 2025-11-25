
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data;
using SampleWebAPI.services;
using SampleWebAPI.Services;
using SampleWebAPI.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .Select(e => new
            {
                Field = e.Key,
                Messages = e.Value.Errors.Select(x => x.ErrorMessage).ToArray()
            });

        var response = new
        {
            Message = "Validation Failed",
            Errors = errors
        };

        return new BadRequestObjectResult(response);
    };
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ITimeService,TimeService>();
builder.Services.AddSingleton<ISingletonGuidInterface, SingletonGuidService>();
builder.Services.AddScoped<IScopedGuidInterface, ScopedGuidService>();
builder.Services.AddTransient<ITransientGuidInterface, TransientGuidService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Use(async (context, next) =>
{
    Console.WriteLine($"The Incoming URL is {context.Request.Method} and {context.Request.Path}");
    await next();
    Console.WriteLine($"The Response has {context.Response.StatusCode} code");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Map("/admin", async adminApp => {

adminApp.Use(async (context, next) =>
{
    Console.WriteLine("🔒 Entered the /admin branch");
    await next();
});


    adminApp.Run(async context =>
    {
        await context.Response.WriteAsync("Welcome to the Admin area!");
    });

});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); // Applies any pending migrations automatically
}

app.Run();

