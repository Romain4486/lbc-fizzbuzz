using FizzBuzz.Domain.Entities;
using FizzBuzz.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace FizzBuzz.Application.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        private readonly ILogger<FizzBuzzService> _logger;
        private readonly IFizzBuzzRepository _fizzBuzzRepository;
        public FizzBuzzService(ILogger<FizzBuzzService> logger, IFizzBuzzRepository fizzBuzzRepository)
        {
            _logger = logger;
            _fizzBuzzRepository = fizzBuzzRepository;
        }

        /// <summary>
        /// Compute the sequence of fizzbuzz from input parameters
        /// </summary>
        /// <param name="fizzBuzzModel">model of fizzbuzz built</param>
        /// <returns>list of fizzbuzz sequence</returns>
        public async Task<List<string>> ComputeSequence(FizzBuzzModel fizzBuzzModel)
        {
            var context = new ValidationContext(fizzBuzzModel, serviceProvider: null, items: null);
            var errorResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(fizzBuzzModel, context, errorResults, true);

            if (!isValid)
                throw new ArgumentException($"{errorResults.First().ErrorMessage}");

            List<string> lst = new List<string>();
            for (int i = 1; i <= fizzBuzzModel.Limit; i++)
            {
                var numberComputed = await ComputeNumber(i, fizzBuzzModel);
                lst.Add(numberComputed);
            }

            var insertIsOK = await _fizzBuzzRepository.InsertFizzBuzz(fizzBuzzModel);
            if (!insertIsOK)
                _logger.LogError($"Unable to insert the fizzBuzz metric for those parameters (fizzNumber : {fizzBuzzModel.FizzNumber} buzzNumber : {fizzBuzzModel.BuzzNumber} limit : {fizzBuzzModel.Limit} fizzLabel : {fizzBuzzModel.FizzLabel} buzzLabel : {fizzBuzzModel.BuzzLabel}");

            return lst;
        }

        private async Task<string> ComputeNumber(int number, FizzBuzzModel fizzBuzzModel)
        {
            string numberComputing = "";

            if (number % fizzBuzzModel.FizzNumber == 0)
                numberComputing += $"{fizzBuzzModel.FizzLabel}";

            if (number % fizzBuzzModel.BuzzNumber == 0)
                numberComputing += $"{fizzBuzzModel.BuzzLabel}";

            if (string.IsNullOrEmpty(numberComputing))
                numberComputing = $"{number}";

            return await Task.FromResult(numberComputing);
        }
    }
}
