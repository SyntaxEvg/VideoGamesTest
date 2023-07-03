using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Services.VideoGamesTest.DTO
{
    public class GameDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public List<GenreDTO> Genres { get; set; }
    }
}
