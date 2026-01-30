using System.Threading.Tasks;
using Hospital.Application.Commands.CreatePatient;
using Hospital.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientCommand command)
        {
            var patientId = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreatePatient), new { id = patientId }, patientId);
        }
        
        
    
    }
};

