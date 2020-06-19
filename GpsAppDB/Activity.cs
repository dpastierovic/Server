using System;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace GpsAppDB
{
    [Table("Activities", Schema = "Application")]
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gear { get; set; }

        public Point StartingPoint { get; set; }

        public DateTime Timestamp { get; set; }
    }
}