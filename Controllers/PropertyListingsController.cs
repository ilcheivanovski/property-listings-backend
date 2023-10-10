using Microsoft.AspNetCore.Mvc;
using PropertyListings.Entity;
using System.Text.Json;

namespace PropertyListings.Controllers
{
    [Route("api/property")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly string jsonFilePath = "./Data/PropertyListings.json"; // Specify your JSON file path

        [HttpGet("list")]
        public ActionResult<IEnumerable<Property>> GetProperties()
        {
            try
            {
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var properties = JsonSerializer.Deserialize<List<Property>>(jsonData);
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{id}/favorite")]
        public ActionResult ToggleFavorite(int id)
        {
            try
            {
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);
                var properties = JsonSerializer.Deserialize<List<Property>>(jsonData);

                // Find the property by ID and toggle its IsFavorite property
                var property = properties.Find(p => p.Id == id);
                if (property != null)
                {
                    property.IsFavorite = !property.IsFavorite;
                    var updatedJsonData = JsonSerializer.Serialize(properties);
                    System.IO.File.WriteAllText(jsonFilePath, updatedJsonData);
                    return Ok();
                }
                else
                {
                    return NotFound("Property not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
