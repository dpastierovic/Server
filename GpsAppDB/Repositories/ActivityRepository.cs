using GpsAppDB.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GpsAppDB.Repositories
{
    public class ActivityRepository
    {
        private readonly ExploViewer _context;

        public ActivityRepository(ExploViewer context)
        {
            _context = context;
        }

        public Activity Add(string athleteId, string activityId, string name, string polyline)
        {
            var activity = new Activity
            {
                AthleteId = athleteId,
                ActivityId = activityId,
                Name = name,
                Polyline = polyline,
                IsRenamed = false
            };

            _context.Activities.Add(activity);
            _context.SaveChanges();

            return activity;
        }

        public bool IsPresent(string activityId)
        {
            return _context.Activities.Any(activity => activity.ActivityId == activityId);
        }

        public Activity Get(string activityId)
        {
            return _context.Activities.FirstOrDefault(activity => activity.ActivityId == activityId);
        }

        public IEnumerable<Activity> Get(IEnumerable<string> activityIds)
        {
            return _context.Activities.Where(activity => activityIds.Contains(activity.ActivityId));
        }
    }
}