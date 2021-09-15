using alter.treinamento.business.Interfaces.LifeTime;
using Microsoft.AspNetCore.Mvc;
using System;

namespace alter.treinamento.api.V1.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Obsolete]
    [Route("api/v{version:apiVersion}/life-time")]
    public class LifeTimeController : ControllerBase
    {
        private readonly ITransientService _transientService1;
        private readonly ITransientService _transientService2;

        private readonly ISingletonService _singletonService1;
        private readonly ISingletonService _singletonService2;

        private readonly IScopedService _scopedService1;
        private readonly IScopedService _scopedService2;

        public LifeTimeController(ITransientService transientService1, ISingletonService singletonService1, IScopedService scopedService1,
            ITransientService transientService2, ISingletonService singletonService2, IScopedService scopedService2)
        {
            _transientService1 = transientService1;
            _transientService2 = transientService2;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _singletonService1 = singletonService1;
            _singletonService2 = singletonService2;
        }

        [HttpGet]
        public IActionResult GetLifeTime()
        {
            var lifetime = @$"Transient1: {_transientService1.GetOperationId()}
                            Transient2: { _transientService2.GetOperationId()}
                            Scoped1 : {_scopedService1.GetOperationId()}
                            Scoped2 : {_scopedService2.GetOperationId()}
                            Singleton1 : {_singletonService1.GetOperationId()}
                            Singleton2 : {_singletonService2.GetOperationId()}";

            return Ok(lifetime);
        }
    }
}
