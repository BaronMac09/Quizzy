using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quizzy.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<QuizzyContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("QuizzyContext") ?? throw new InvalidOperationException("Connection string 'QuizzyContext' not found.")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<QuizzyContext>();
    context.Database.EnsureCreated();
    DbInitialiser.Initialise(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();