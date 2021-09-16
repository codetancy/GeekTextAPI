using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Locals
{
    public class LocalTestRepository : ITestRepository
    {
        private readonly List<Test> _tests;
        
        public LocalTestRepository()
        {
            _tests = new List<Test>
            {
                new() { Id = 1, Text = "First" },
                new() { Id = 2, Text = "Second" },
                new() { Id = 3, Text = "Third" }
            };
        }
        
        public async Task<List<Test>> GetTestsAsync()
        {
            return await Task.FromResult(_tests);
        }

        public async Task<Test> GetTestByIdAsync(int testId)
        {
            var test = _tests.SingleOrDefault(p => p.Id == testId);
            return await Task.FromResult(test);
        }

        public async Task<int> CreateTestAsync(Test test)
        {
            if (test.Id <= 0)
                test.Id = _tests.Max(t => t.Id) + 1;
            
            var existingTest = await GetTestByIdAsync(test.Id);
            if (existingTest is not null)
                return await Task.FromResult(0);
            
            _tests.Add(test);
            return await Task.FromResult(test.Id);
        }

        public async Task<bool> UpdateTest(Test test)
        {
            var index = _tests.FindIndex(p => p.Id == test.Id);

            if (index < 0)
                return await Task.FromResult(false);

            _tests[index] = test;
            return true;
        }

        public async Task<bool> DeleteTest(int testId)
        {
            var index = _tests.FindIndex(p => p.Id == testId);

            if (index < 0)
                return await Task.FromResult(false);
            
            _tests.RemoveAt(index);
            return true;
        }
    }
}