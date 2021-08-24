using FizzBuzz.Domain.Interfaces;
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
        private readonly IHealthCheckRepository _healthCheckRepository;
        public HealthCheckController(ILogger<HealthCheckController> logger, IHealthCheckRepository healthCheckRepository)
        {
            _logger = logger;
            _healthCheckRepository = healthCheckRepository;
        }

        [Route("_system/check/simple")]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                return "Api ok";
            }
            catch (Exception ex)
            {
                return BadRequest($"Improper API configuration {ex.Message}");
            }
        }

        [Route("_system/check/full")]
        public async Task<ActionResult<string>> GetFullInfrastructureInformation()
        {
            try
            {
                string result = "Api ok \nDatabase nok";
                var databaseIsUp = await _healthCheckRepository.CheckDatabaseUp();

                if (databaseIsUp)
                    result = "Api ok \nDatabase ok";

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest($"Improper API or DATABASE configuration {ex.Message}");
            }
        }
    }
}
