using FizzBuzz.Domain.Entities;
using System.Threading.Tasks;

namespace FizzBuzz.Domain.Interfaces
{
    public interface IFizzBuzzRepository
    {
        Task<bool> InsertFizzBuzz(FizzBuzzModel fizzBuzzModel);
        Task<FizzBuzzModel> GetFizzBuzzMaxCalls();
    }
}
