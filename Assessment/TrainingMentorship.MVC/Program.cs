using Microsoft.AspNetCore.Mvc;
using TrainingMentorship.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});


builder.Services.AddHttpClient<AuthApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]); // your API base URL
});

builder.Services.AddHttpClient<TrainingProgramApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]); // your API base URL
});

builder.Services.AddHttpClient<TraineeTaskApiService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
});


builder.Services.AddHttpClient<ProgramMentorApiService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
});

builder.Services.AddSession();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();


app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value?.ToLower();

    // Allow anonymous pages
    var allowedPaths = new[] { "/account/login", "/css", "/js", "/lib" };

    if (allowedPaths.Any(a => path.StartsWith(a)))
    {
        await next();
        return;
    }

    // Session auth check
    var token = context.Session.GetString("JwtToken");

    if (string.IsNullOrEmpty(token))
    {
        context.Response.Redirect("/Account/Login");
        return;
    }

    await next();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
