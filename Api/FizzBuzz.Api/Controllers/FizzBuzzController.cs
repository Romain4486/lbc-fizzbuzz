using FizzBuzz.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizzBuzz.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FizzBuzzController : ControllerBase
    {
        private readonly ILogger<FizzBuzzController> _logger;
        private readonly IFizzBuzzService _fizzBuzzService;

        public FizzBuzzController(ILogger<FizzBuzzController> logger,IFizzBuzzService fizzBuzzService)
        {
            _logger = logger;
            _fizzBuzzService = fizzBuzzService;
        }

        [AllowAnonymous]
        [HttpGet("v1/fizzbuzz/{fizzNumber}/{buzzNumber}/{limit}/{fizzLabel}/{buzzLabel}")]
        public ActionResult<List<string>> FizzBuzzComputing(int fizzNumber, int buzzNumber, int limit, string fizzLabel, string buzzLabel)
        {
            try
            {
                return new List<string>();
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to compute FizzBuzz", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, string.Empty);
            }
        }
    }
}
