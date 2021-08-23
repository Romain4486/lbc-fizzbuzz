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
        public FizzBuzzService()
        {
        }
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
