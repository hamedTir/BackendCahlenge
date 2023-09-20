namespace ChalengeBackend.Controllers
{
    using ChalengeBackend.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/analyzer")]
    [ApiController]
    public class AnalyzerController : ControllerBase
    {
        [HttpPost("reverseNum")]
        public IActionResult ReverseNum([FromBody] uint number)
        {
            try
            {
                if (AnalyzerService.TryReverse(number, out uint reversedNumber))
                {
                    return Ok(reversedNumber);
                }

                return BadRequest("Invalid input");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}