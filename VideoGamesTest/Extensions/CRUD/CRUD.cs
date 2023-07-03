using DAL.VideoGamesTest.Contexts;
using DAL.VideoGamesTest.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.VideoGamesTest.DTO;
using Services.VideoGamesTest.ManualMapping;

namespace VideoGamesTest.CRUD
{
    public static class CRUDExtension
    {
        public static void CRUD(this WebApplication? app)
        {
            app.MapGet("/api/game", ([FromServices] GamesContext context, string genre) =>
            {
                IQueryable<Game> query = context.Games;

                if (!string.IsNullOrEmpty(genre))
                {
                    query = query.Where(game => game.Genres.Any(g => g.Name == genre));
                }
                query.Include(x => x.Genres);
                List<Game> games = query.ToList();
                return Results.Ok(games);
            });

            app.MapPost("/api/game", async ([FromServices] GamesContext context, GameDTO gamedto) =>
            {

                if (await context.Games.AnyAsync(x => x.Name.ToUpper() == gamedto.Name.ToUpper()))
                {
                    return Results.NotFound("Существует...");
                }

                var game =gamedto.AsDB();

                await context.Games.AddAsync(game);
                await context.SaveChangesAsync();
               
                return Results.Ok(game.Id);
            });

            app.MapPut("/api/game/update", async ([FromServices] GamesContext context, GameDTO gamedto) =>
            {
                var game = gamedto.AsDB();
                //Game existingGame = await context.Games.Include(x=> x.Genres).FirstOrDefaultAsync(x => x.Name.ToUpper() == gamedto.Name.ToUpper());
                Game? existingGame = await context.Games.Where(x => x.Name.ToUpper() == gamedto.Name.ToUpper()).Select(x => new Game
                {
                    Id = x.Id,
                    Name = x.Name,
                    Developer = x.Developer,
                    Genres = x.Genres.Select(y=> new Genre
                    {
                        Id = y.Id,
                        Name = y.Name,
                    }).ToList()
                }).AsNoTracking().AsQueryable().FirstOrDefaultAsync();

                if (existingGame is null)
                {
                    return Results.NotFound("Не существует...");
                }
                await context.SaveChangesAsync();

                return Results.Ok(existingGame);
            });

            app.MapDelete("/api/game/{id}", async ([FromServices] GamesContext context, int id) =>
            {
                Game existingGame = await context.Games.FindAsync(id)!;

                if (existingGame == null)
                {
                    return Results.NotFound();
                }

                context.Games.Remove(existingGame);
                await context.SaveChangesAsync();

                return Results.NoContent();
            });
            app.MapGet("/api/games",async  ([FromServices] GamesContext context) =>
            {
                IQueryable<Game> query = context.Games;
                var existingGames = await query.Select(x =>x.AsDTO()).AsNoTracking().AsQueryable().ToListAsync();               
                return Results.Ok(existingGames);
            });
        }
    }
}
