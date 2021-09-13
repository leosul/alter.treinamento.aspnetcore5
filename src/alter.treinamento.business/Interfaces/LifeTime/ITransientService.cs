using System;

namespace alter.treinamento.business.Interfaces.LifeTime
{
    public interface ITransientService
    {
        Guid GetOperationId();
    }
}
