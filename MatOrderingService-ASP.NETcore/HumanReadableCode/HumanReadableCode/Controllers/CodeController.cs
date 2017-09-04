using HumanReadableCode.Services;
using Microsoft.AspNetCore.Mvc;

namespace HumanReadableCode.Controllers
{
    [Route("api/code/")]
    public class CodeController : Controller
    {
        private readonly CodeService _service;

        public CodeController(CodeService service)
        {
            _service = service;
        }

        //GET api/code/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _service.GetCodeForId(id);
        }
    }
}
