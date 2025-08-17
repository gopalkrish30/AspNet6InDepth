using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : Controller
    {
        private readonly ISingletonOperation _singletonOperation;
        private readonly IScopedOperation _scopedOperation;
        private readonly ITransitiveOperation _transitiveOperation;
        private readonly IDummyService _dummyService;

        public OperationController(
            ISingletonOperation singletonOperation,
            IScopedOperation scopedOperation,
            ITransitiveOperation transitiveOperation,
            IDummyService dummyService)
        {
            _singletonOperation = singletonOperation;
            _scopedOperation = scopedOperation;
            _transitiveOperation = transitiveOperation;
            _dummyService = dummyService;
        }

        [HttpGet]
        public IActionResult GetOperation()
        {
            Console.WriteLine(_scopedOperation.OperationId);
            Console.WriteLine(_transitiveOperation.OperationId);
            _dummyService.PrintOperationId();
            return Ok(new
            {
                singleton = _singletonOperation.OperationId,
                scoped = _scopedOperation.OperationId,
                transient = _transitiveOperation.OperationId
            });
        }
    }
}
