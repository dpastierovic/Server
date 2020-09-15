using System.ComponentModel.DataAnnotations.Schema;

namespace GpsAppDB.Entities
{
    [Table("Activities")]
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Athlete))]
        public string AthleteId { get; set; }

        public string ActivityId { get; set; }

        public string Name { get; set; }

        public string Polyline { get; set; }

        public bool IsRenamed { get; set; }
    }
}