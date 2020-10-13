using GpsAppDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Controllers.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MarkerController : ControllerBase
    {
        private readonly IMarkerRepository _markerRepository;

        public MarkerController(IMarkerRepository markerRepository)
        {
            _markerRepository = markerRepository;
        }

        [HttpGet]
        [Route("/api/[controller]/markers/{athleteId}")]
        public IActionResult GetAll(string athleteId)
        {
            return Ok(JsonConvert.SerializeObject(_markerRepository.GetAll(athleteId), Formatting.Indented));
        }
    }
}
