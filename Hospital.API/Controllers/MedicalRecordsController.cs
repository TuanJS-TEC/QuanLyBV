using System.Threading.Tasks;
using Hospital.Application.Commands.CreateMedicalRecord;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.API.Controllers
{
    [Route("api/v1/medical-records")] 
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicalRecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] CreateMedicalRecordCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateMedicalRecord), new { id = id }, new { RecordID = id });
        }
    }
}