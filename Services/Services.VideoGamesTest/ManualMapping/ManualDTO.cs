using DAL.VideoGamesTest.Entity;
using Services.VideoGamesTest.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.VideoGamesTest.ManualMapping
{
    public static class ManualDTO
    {
        public static Game AsDB (this GameDTO gameDTO) 
        {
            return new Game
            {
                Name = gameDTO.Name,
                Developer = gameDTO.Developer,
                Genres = gameDTO.Genres.Select(existingGameGenre => new Genre
                {
                    Name = existingGameGenre.Name
                }).ToList()
            };

        }
        public static GameDTO AsDTO(this Game game) 
        {
            return new GameDTO
            {
                Id = game.Id,
                Name = game.Name,
                Developer = game.Developer,
                Genres = game.Genres.Select(gameGenre => new GenreDTO
                {
                    Id = gameGenre.Id,
                    Name = gameGenre.Name,
                   
                }).ToList()
            };
        }
    }
}
