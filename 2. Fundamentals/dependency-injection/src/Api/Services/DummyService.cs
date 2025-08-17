using Api.Interfaces;

namespace Api.Services
{
    public class DummyService : IDummyService
    {
        private readonly IScopedOperation _scopedOperation;
        private readonly ITransitiveOperation _transitiveOperation;
        private readonly ILogger<DummyService> _logger;

        public DummyService(IScopedOperation scopedOperation, ITransitiveOperation transitiveOperation, ILogger<DummyService> logger)
        {
            _scopedOperation = scopedOperation;
            _transitiveOperation = transitiveOperation;
            _logger = logger;
        }
        public void PrintOperationId()
        {
            _logger.LogInformation("Scoped Transition Id" + _scopedOperation.OperationId.ToString());
            _logger.LogInformation("Transitive Transition Id" + _transitiveOperation.OperationId.ToString());
            
        }
    }
}
