using alter.treinamento.business.Interfaces;
using alter.treinamento.business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace alter.treinamento.api.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificator _notificator;

        public MainController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidOperation()
        {
            return !_notificator.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if(ValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                data = _notificator.GetNotifications().Select(s => s.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErrorInvalidModel(modelState);

            return CustomResponse();
        }

        protected void NotifyErrorInvalidModel(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(s => s.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMessage);
            }
        }

        protected void NotifyError(string message)
        {
            _notificator.Handle(new Notification(message));
        }
    }
}
