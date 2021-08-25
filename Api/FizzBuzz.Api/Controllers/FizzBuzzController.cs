using FizzBuzz.Domain.Entities;
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
    public class FizzBuzzController : ControllerBase
    {
        private readonly ILogger<FizzBuzzController> _logger;
        private readonly IFizzBuzzService _fizzBuzzService;
        private readonly IFizzBuzzRepository _fizzBuzzRepository;

        public FizzBuzzController(ILogger<FizzBuzzController> logger, IFizzBuzzService fizzBuzzService, IFizzBuzzRepository fizzBuzzRepository)
        {
            _logger = logger;
            _fizzBuzzService = fizzBuzzService;
            _fizzBuzzRepository = fizzBuzzRepository;
        }

        /// <summary>
        /// Allow to compute fizzbuzz sequence
        /// </summary>
        /// <param name="fizzNumber">all multiple number replace by the fizzlabel by default 3</param>
        /// <param name="buzzNumber">all multiple number replace by the buzzlabel by default 5</param>
        /// <param name="limit">limit of the sequence by default 100</param>
        /// <param name="fizzLabel">label value to display by default Fizz</param>
        /// <param name="buzzLabel">label value to display by default Buzz</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("v1/fizzbuzz/{fizzNumber}/{buzzNumber}/{limit}/{fizzLabel}/{buzzLabel}")]
        public async Task<ActionResult<List<string>>> FizzBuzzComputing(int fizzNumber = 3, int buzzNumber = 5, int limit = 100, string fizzLabel = "Fizz", string buzzLabel = "Buzz")
        {
            try
            {
                FizzBuzzModel fizzBuzzModel = new FizzBuzzModel() { FizzNumber = fizzNumber, BuzzNumber = buzzNumber, Limit = limit, FizzLabel = fizzLabel, BuzzLabel = buzzLabel };
                return await _fizzBuzzService.ComputeSequence(fizzBuzzModel);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Unable to compute FizzBuzz for those parameters (fizzNumber : {fizzNumber} buzzNumber : {buzzNumber} limit : {limit} fizzLabel : {fizzLabel} buzzLabel : {buzzLabel}", ex);
                return StatusCode(StatusCodes.Status400BadRequest, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to compute FizzBuzz for those parameters (fizzNumber : {fizzNumber} buzzNumber : {buzzNumber} limit : {limit} fizzLabel : {fizzLabel} buzzLabel : {buzzLabel}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, string.Empty);
            }
        }

        /// <summary>
        /// Get the parameters corresponding to the most used request, as well as the number of hits for this request
        /// </summary>
        /// <returns>FizzBuzz sequence with the number of hits</returns>
        [AllowAnonymous]
        [HttpGet("v1/fizzbuzz/stats")]
        public async Task<ActionResult<FizzBuzzModel>> GetMaxCallsOfFizzBuzz()
        {
            try
            {
                return await _fizzBuzzRepository.GetFizzBuzzMaxCalls();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to get stats", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, string.Empty);
            }
        }
    }
}
