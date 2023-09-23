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

        [HttpPost("getLongestSubstring")]
        public IActionResult GetLongestSubstring([FromBody] string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return BadRequest("Input string cannot be empty.");
            }

            int maxLength = 0;
            int startIndex = 0;
            int currentStart = 0;
            int[] lastIndex = new int[256]; // Assuming ASCII characters

            for (int i = 0; i < lastIndex.Length; i++)
            {
                lastIndex[i] = -1;
            }

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (lastIndex[c] >= currentStart)
                {
                    currentStart = lastIndex[c] + 1;
                }

                lastIndex[c] = i;

                if (i - currentStart + 1 > maxLength)
                {
                    maxLength = i - currentStart + 1;
                    startIndex = currentStart;
                }
            }

            string longestSubstring = input.Substring(startIndex, maxLength);
            return Ok(longestSubstring);
        }
    }
}

