using MediatR;

namespace Hospital.Application.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<int>
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; } 
        public TimeSpan AppointmentTime { get; set; } 
        public string? Reason { get; set; }
        public string? AppointmentType { get; set; } = "Consultation";
    }
};

