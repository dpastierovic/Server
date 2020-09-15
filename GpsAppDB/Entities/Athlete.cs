using System.ComponentModel.DataAnnotations.Schema;

namespace GpsAppDB.Entities
{
    [Table("Athletes")]
    public class Athlete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AthleteId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}