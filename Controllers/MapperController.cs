using Microsoft.AspNetCore.Mvc;

namespace Integrador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapperController : ControllerBase
    {
        private readonly ILogger<MapperController> _logger;

        public MapperController(ILogger<MapperController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("procesar")]
        public async Task<IActionResult> MapRequest(object request)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return BadRequest();
            }
        }
    }
}