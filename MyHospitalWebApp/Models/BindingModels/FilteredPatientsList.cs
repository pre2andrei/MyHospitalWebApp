using HospitalWebApp.Models;

namespace MyHospitalWebApp.Models.BindingModels
{
    public class FilteredPatientsList
    {
        public PatientsFilter filter;
        public List<Patient> patients;
    }
}
