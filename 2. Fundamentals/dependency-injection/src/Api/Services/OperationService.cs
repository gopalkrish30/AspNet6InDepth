using Api.Interfaces;

namespace Api.Services
{
    public class OperationService : IOperation, ISingletonOperation, IScopedOperation, ITransitiveOperation
    {
        private Guid _operationid;
        public OperationService(Guid operationid)
        {
            _operationid = operationid;
        }
        public OperationService() : this(Guid.NewGuid()) { }

        public Guid OperationId
        {
            get
            {
                return _operationid;
            }
            set
            {
                _operationid= value;
            }
        }
    }
}
