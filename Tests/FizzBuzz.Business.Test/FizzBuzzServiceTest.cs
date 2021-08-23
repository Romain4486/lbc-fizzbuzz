using FizzBuzz.Domain.Entities;
using FizzBuzz.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace FizzBuzz.Business.Test
{
    [TestClass]
    public class FizzBuzzServiceTest
    {
        private readonly IFizzBuzzService _fizzBuzzService;

        public FizzBuzzServiceTest()
        {
            _fizzBuzzService = new Application.Services.FizzBuzzService();
        }


        [TestMethod]
        public async Task GoodScenario()
        {
            var goodRequest = new FizzBuzzModel() { FizzNumber = 2, BuzzNumber = 5, FizzLabel = "fizz", BuzzLabel = "buzz", Limit = 10 };
            var actualResult = await _fizzBuzzService.ComputeSequence(goodRequest);
            string expectedResult = "1,fizz,3,fizz,buzz,fizz,7,fizz,9,fizzbuzz";
            Assert.AreEqual(expectedResult, string.Join(',', actualResult));
        }

        [TestMethod]
        public async Task GoodScenario_SameNumberForFizzAndBuzz()
        {
            var goodRequest = new FizzBuzzModel() { FizzNumber = 3, BuzzNumber = 3, FizzLabel = "fizz", BuzzLabel = "buzz", Limit = 10 };
            var actualResult = await _fizzBuzzService.ComputeSequence(goodRequest);
            string expectedResult = "1,2,fizzbuzz,4,5,fizzbuzz,7,8,fizzbuzz,10";
            Assert.AreEqual(expectedResult, string.Join(',', actualResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The BuzzLabel field is required.")]

        public async Task BadScenario_WithEmptyBuzz()
        {
            var badRequest = new FizzBuzzModel() { FizzNumber = 2, BuzzNumber = 5, FizzLabel = "fizz", BuzzLabel = "", Limit = 10 };
            await _fizzBuzzService.ComputeSequence(badRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The FizzLabel field is required.")]

        public async Task BadScenario_WithEmptyFizz()
        {
            var badRequest = new FizzBuzzModel() { FizzNumber = 2, BuzzNumber = 5, FizzLabel = "", BuzzLabel = "buzz", Limit = 10 };
            await _fizzBuzzService.ComputeSequence(badRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The field FizzNumber must be between 1 and 100.")]

        public async Task BadScenario_ZeroForFizzNumber()
        {
            var badRequest = new FizzBuzzModel() { FizzNumber = 0, BuzzNumber = 5, FizzLabel = "fizz", BuzzLabel = "buzz", Limit = 10 };
            await _fizzBuzzService.ComputeSequence(badRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The field BuzzNumber must be between 1 and 100.")]

        public async Task BadScenario_ZeroForBuzzNumber()
        {
            var badRequest = new FizzBuzzModel() { FizzNumber = 0, BuzzNumber = 5, FizzLabel = "fizz", BuzzLabel = "buzz", Limit = 10 };
            await _fizzBuzzService.ComputeSequence(badRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The field Limit must be between 1 and 100.")]

        public async Task BadScenario_LimitRequired()
        {
            var badRequest = new FizzBuzzModel() { FizzNumber = 2, BuzzNumber = 5, FizzLabel = "fizz", BuzzLabel = "buzz" };
            await _fizzBuzzService.ComputeSequence(badRequest);
        }
    }
}
