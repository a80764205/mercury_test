using BackendExam.Models;
using BackendExam.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyOffice_ACPDController : ControllerBase
    {
        private readonly MyOffice_ACPDService _service;

        public MyOffice_ACPDController(MyOffice_ACPDService service)
        {
            _service = service;
        }

        [HttpPost("query")]
        public async Task<IActionResult> GetAllData([FromBody] MyOffice_ACPDFilter model)
        {
            var json = JsonSerializer.Serialize(model);
            var data = await _service.GetAllData(json);
            return Ok(data);
        }
    }
}
