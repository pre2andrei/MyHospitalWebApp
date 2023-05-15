using HospitalWebApp.Models;

namespace MyHospitalWebApp.Models.BindingModels
{
    public class FilteredAppointmentsList
    {
        public AppointmentsFilter filter;
        public List<Appointment> oldAppointments;
        public List<Appointment> upcomingAppointments;
    }
}
