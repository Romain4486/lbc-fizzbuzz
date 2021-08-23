using FizzBuzz.Domain.Entities;
using FizzBuzz.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FizzBuzz.Application.Services
{
    public class FizzBuzzService : IFizzBuzzService
    {
        public Task<List<string>> ComputeSequence(FizzBuzzModel fizzBuzzModel)
        {
            throw new NotImplementedException();
        }
    }
}
