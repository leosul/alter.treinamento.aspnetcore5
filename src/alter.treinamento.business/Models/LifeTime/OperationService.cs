using alter.treinamento.business.Interfaces.LifeTime;
using System;

namespace alter.treinamento.business.Models.LifeTime
{
    public class OperationService: ITransientService, IScopedService, ISingletonService
    {
        Guid id;

        public OperationService()
        {
            id = Guid.NewGuid();
        }

        public Guid GetOperationId()
        {
            return id;
        }
    }
}
