using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface ITestRepository
    {
        Task<List<Test>> GetTestsAsync();
        Task<Test> GetTestByIdAsync(int testId);
        Task<int> CreateTestAsync(Test test);
        Task<bool> UpdateTestAsync(Test test);
        Task<bool> DeleteTestAsync(int testId);
    }
}
