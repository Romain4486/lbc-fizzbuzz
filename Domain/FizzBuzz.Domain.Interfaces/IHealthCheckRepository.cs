using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz.Domain.Interfaces
{
    public interface IHealthCheckRepository
    {
        Task<bool> CheckDatabaseUp();
    }
}
