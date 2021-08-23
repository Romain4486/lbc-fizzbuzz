using FizzBuzz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FizzBuzz.Domain.Interfaces
{
    public interface IFizzBuzzService
    {
        Task<List<string>> ComputeSequence(FizzBuzzModel fizzBuzzModel);
    }
}
