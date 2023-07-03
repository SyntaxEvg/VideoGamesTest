using DAL.VideoGamesTest.Contexts;
using DAL.VideoGamesTest.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.MySLQ;
using VideoGamesTest.CRUD;

var builder = WebApplication.CreateBuilder(args);
var db = builder.Configuration["ConnectionStrings:db"];

#region Services
builder.Services.UseSqlServer(db);
builder.Services.AddDbContext<GamesContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opt =>
{
    opt.DefaultPolicyName = "default";
    opt.AddDefaultPolicy(b =>
    {
        b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

#endregion
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<GamesContext>(); //
    if (context.Database.EnsureCreated())
    {
        ///бла бла ..  
    }
    await context.Database.MigrateAsync().ConfigureAwait(false);
}


app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("default");

#region Api Microservice
app.CRUD();
app.Run();
#endregion

