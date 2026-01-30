using System.Threading.Tasks;
using Hospital.Application.Commands.CreateAppointment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateAppointment), new { id = id }, new { AppointmentId = id });
        }
    }
}