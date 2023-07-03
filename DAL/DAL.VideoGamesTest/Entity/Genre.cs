using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.VideoGamesTest.Entity
{
    [Table("Genres", Schema = "VideoGame")]
    [Index(nameof(Name))]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Game Game { get; set; }
    }
}
