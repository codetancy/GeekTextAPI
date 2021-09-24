using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.V1.Requests;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Controllers.V1
{
    [ApiController]
    [Route("api/v1/tests")]
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

        [Authorize]
        [HttpGet("authorize")]
        public async Task<IActionResult> IsAuthorized()
        {
            return await Task.FromResult(Ok("You are authorized!"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTestRequest testRequest)
        {
            var test = new Test { Text = testRequest.Text };
            int testId = await _testRepository.CreateTestAsync(test);
            return testId > 0
                ? CreatedAtAction(nameof(GetById), new { testid = testId }, testRequest)
                : BadRequest();
        }

        [HttpDelete("{testId:int}")]
        public async Task<IActionResult> Create([FromRoute] int testId)
        {
            bool deleted = await _testRepository.DeleteTestAsync(testId);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPut("{testId:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateTestRequest testRequest, [FromRoute] int testId)
        {
            var test = new Test { Id = testId, Text = testRequest.Text };
            bool updated = await _testRepository.UpdateTestAsync(test);
            return updated ? Ok() : NotFound();
        }
    }
}
