using System;

namespace alter.treinamento.business.Interfaces.LifeTime
{
    public interface IScopedService
    {
        Guid GetOperationId();
    }
}
