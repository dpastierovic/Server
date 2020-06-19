using System;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace GpsAppDB
{
    public class ActivityRepository
    {
        private readonly ActivityContext _context;

        public ActivityRepository(ActivityContext context)
        {
            _context = context;
        }

        public Activity Add(string name)
        {
            var activity = new Activity
            {
                Name = name,
                Timestamp = DateTime.Now,
                Gear = "CUBE Agree C62 Race Disc",
                StartingPoint = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326).CreatePoint(new Coordinate(48, 18))
            };

            _context.Activities.Add(activity);
            _context.SaveChanges();

            return activity;
        }
    }
}