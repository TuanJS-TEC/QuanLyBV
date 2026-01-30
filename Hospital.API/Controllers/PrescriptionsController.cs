using System.Threading.Tasks;
using Hospital.Application.Commands.CreatePrescription;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrescriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreatePrescription), new { id = id }, new { PrescriptionID = id });
        }
    }
}