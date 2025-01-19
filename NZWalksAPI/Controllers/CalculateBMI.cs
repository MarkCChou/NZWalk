using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CalculateBMI : ControllerBase
    {
        [HttpGet]

        public IActionResult GetBMIResult(double height, double weight)
        {
            if (height <= 0 || weight <= 0)
            {
                return BadRequest("Height and weight must be positive values.");
            }

            double BMIResult = weight / (height * height);

            var result = new BMIResult();
            result.BMI = BMIResult;
            result.Category = "FAT";

            return Ok(result);
        }


    }

    public class BMIResult
    {
        public double BMI { get; set; }
        public string Category { get; set; }
    }
}
