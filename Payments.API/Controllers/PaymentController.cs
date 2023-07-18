using Microsoft.AspNetCore.Mvc;
using Payments.API.Models;
using Payments.API.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace Payments.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMessageBusService _messageBusService;

        public PaymentController(IMessageBusService MessageBusService)
        {
            _messageBusService = MessageBusService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PaymentInfoModel model)
        {
            if (!ModelState.IsValid || model is null)
                return BadRequest(ModelState);

            var modelInfosJson = JsonSerializer.Serialize(model);
            var modelInfosBytes = Encoding.UTF8.GetBytes(modelInfosJson);

            _messageBusService.Publish("Payments", modelInfosBytes);

            return Accepted();
        }
    }
}
