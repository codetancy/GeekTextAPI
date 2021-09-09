using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _testRepository;

        public TestController(ITestRepository testRepository)
        { 
            _testRepository = testRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tests = await _testRepository.GetTestsAsync();
            return Ok(tests);
        }
        
        [HttpGet("{testId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int testId)
        {
            var test = await _testRepository.GetTestByIdAsync(testId);
            return test is null ? BadRequest() : Ok(test);
        }
    }
}