using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizzBuzz.Api.Controllers
{
    
    [ApiController]
    public class HealthCheckController : Controller
    {
        private readonly ILogger<HealthCheckController> _logger;
        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }
        [Route("_system/check/simple")]
        public IActionResult Get()
        {
            try
            {
                return Ok("ok");
            }
            catch (Exception ex)
            {
                return BadRequest($"Improper API configuration {ex.Message}");
            }
        }
    }
}
