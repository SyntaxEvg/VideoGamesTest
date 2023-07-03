using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Services.VideoGamesTest.DTO
{
    public class GenreDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

       // public GameDTO Game { get; set; }
    }
}
