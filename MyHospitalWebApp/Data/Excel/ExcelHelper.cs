using HospitalWebApp.Models;
using MyHospitalWebApp.Models.BindingModels;

namespace MyHospitalWebApp.Data.Excel
{
    public static class ExcelHelper
    {
        public static AppointmentForExcel toAppointmentForExcel(this Appointment appointment)
        {
            return new AppointmentForExcel()
            {
                Id = appointment.Id,
                patientId = appointment.patientId,
                Patient = appointment.Patient.firstName + " " + appointment.Patient.lastName,
                doctorId = appointment.doctorId,
                Doctor = appointment.Doctor.firstName + " " + appointment.Doctor.lastName,
                AppointedTime = appointment.AppointedTime.ToString("dd-MM-yyy HH:mm"),
                RoomNr = appointment.RoomNr,
            };
        }
        public static PatientForExcel toPatientForExcel(this Patient patient)
        {
            return new PatientForExcel()
            {
                Id = patient.Id,
                firstName = patient.firstName,
                lastName = patient.lastName,
                DOB = patient.DOB.ToString("dd-MM-yyy HH:mm"),
                PIC = patient.PIC,
                userId= patient.userId,
            };
        }
    }
}
