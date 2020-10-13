using System.ComponentModel.DataAnnotations.Schema;

namespace GpsAppDB.Entities
{
    [Table("Markers")]
    public class Marker
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Athlete Athlete { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}