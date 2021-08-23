﻿using FizzBuzz.Domain.Entities;
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
    }
}
