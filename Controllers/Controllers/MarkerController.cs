using Controllers.Controllers.Responses;
using GpsAppDB.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("/api/[controller]/add/{athleteId}")]
        public async Task<IActionResult> AddMarker(string athleteId)
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var jsonData = await reader.ReadToEndAsync();
            var marker = JsonConvert.DeserializeObject<Marker>(jsonData);
            var response = _markerRepository.Add(athleteId, marker.Name, marker.Latitude, marker.Longitude);
            return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        }

        [HttpDelete]
        [Route("/api/[controller]/delete/{athleteId}/{id}")]
        public IActionResult RemoveMarker(string athleteId, int id)
        {
            _markerRepository.Delete(athleteId, id);
            return Ok();
        }

        [HttpGet]
        [Route("/api/[controller]/markers/{athleteId}")]
        public IActionResult GetAll(string athleteId)
        {
            return Ok(JsonConvert.SerializeObject(_markerRepository.GetAll(athleteId), Formatting.Indented));
        }
    }
}