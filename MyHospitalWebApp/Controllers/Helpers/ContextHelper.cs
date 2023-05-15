using MyMyHospitalWebApp.Data;
using HospitalWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyHospitalWebApp.Models.BindingModels;

namespace HospitalWebApp.Controllers.Helpers
{
    public static class ContextHelper
    {
        public static SelectList toSelectList(this DbSet<Doctor> Doctors, int selectedId = 0)
        {
            IEnumerable<SelectListItem> selectList = from d in Doctors
                                                     select new SelectListItem
                                                     {
                                                         Value = d.Id.ToString(),
                                                         Text = d.firstName + " " + d.lastName
                                                     };
            return new SelectList(selectList, "Value", "Text", selectedId);
        }

        public static SelectList toSelectList(this DbSet<Speciality> Specialities, int selectedId = 0)
        {
            return new SelectList(Specialities, "Id", "Name", selectedId);
        }

        public static SelectList toSelectList(this DbSet<Patient> Patients, int selectedId = 0)
        {
            IEnumerable<SelectListItem> selectList = from d in Patients
                                                     select new SelectListItem
                                                     {
                                                         Value = d.Id.ToString(),
                                                         Text = d.firstName + " " + d.lastName
                                                     };
            return new SelectList(selectList, "Value", "Text", selectedId);
        }

        public static SelectList toSelectList(this DbSet<Symptom> Symptoms, int selectedId = 0)
        {
            return new SelectList(Symptoms, "Id", "Descritpion", selectedId);
        }

        public static SelectList toSelectList(this DbSet<Disease> Diseases, int selectedId = 0)
        {
            return new SelectList(Diseases, "Id", "Name", selectedId);
        }

        public static async Task<bool> HasIdAsync<T>(this DbSet<T> data, int? id)
            where T : class, Identified
        {

            if (id == null || data == null)
            {
                return false;
            }

            return await data.AnyAsync(m => m.Id == id);
        }

        public static IQueryable<Appointment> FilterAppointments(this DbSet<Appointment> appointments, AppointmentsFilter filter)
        {
            IQueryable<Appointment> query = appointments.Include(a => a.Doctor).Include(a => a.Patient).AsQueryable();


            if (filter.desc)
            {
                if (filter.orderByDoctorName)
                {
                    query = query.OrderByDescending(a => a.Doctor.firstName + a.Doctor.lastName).AsQueryable();
                }
                else if (filter.orderByPatientName)
                {
                    query = query.OrderByDescending(a => a.Patient.firstName + a.Patient.lastName).AsQueryable();
                }
                else if (filter.orderByAppointedTime)
                {
                    query = query.OrderByDescending(a => a.AppointedTime).AsQueryable();
                }
                else if (filter.orderRoomNr)
                {
                    query = query.OrderByDescending(a => a.RoomNr).AsQueryable();
                }
            }
            else
            {
                if (filter.orderByDoctorName)
                {
                    query = query.OrderBy(a => a.Doctor.firstName + a.Doctor.lastName).AsQueryable();
                }
                else if (filter.orderByPatientName)
                {
                    query = query.OrderBy(a => a.Patient.firstName + a.Patient.lastName).AsQueryable();
                }
                else if (filter.orderByAppointedTime)
                {
                    query = query.OrderBy(a => a.AppointedTime).AsQueryable();
                }
                else if (filter.orderRoomNr)
                {
                    query = query.OrderBy(a => a.RoomNr).AsQueryable();
                }
            }


            if (filter.doctorNameFilter != null)
            {
                query = query.Where(a => (a.Doctor.firstName + a.Doctor.lastName).Contains(filter.doctorNameFilter)).AsQueryable();
            }
            if (filter.patientNameFilter != null)
            {
                query = query.Where(a => (a.Patient.firstName + a.Patient.lastName).Contains(filter.patientNameFilter)).AsQueryable();
            }
            if (filter.dateFilter != null)
            {
                query = query.Where(a => a.AppointedTime.Date == filter.dateFilter.Value.Date).AsQueryable();
            }
            if (filter.roomNrFilter != null)
            {
                query = query.Where(a => a.RoomNr == filter.roomNrFilter).AsQueryable();
            }


            return query;
        }

        public static IQueryable<Patient> FilterPatients(this DbSet<Patient> patients, PatientsFilter filter)
        {
            IQueryable<Patient> query = patients.AsQueryable();


            if (filter.desc)
            {
                if (filter.orderByFullName)
                {
                    query = query.OrderByDescending(a => a.firstName + a.lastName).AsQueryable();
                }
                else if (filter.orderByDob)
                {
                    query = query.OrderByDescending(a => a.DOB).AsQueryable();
                }
            }
            else
            {
                if (filter.orderByFullName)
                {
                    query = query.OrderBy(a => a.firstName + a.lastName).AsQueryable();
                }
                else if (filter.orderByDob)
                {
                    query = query.OrderBy(a => a.DOB).AsQueryable();
                }
            }


            if (filter.fullNameFilter != null)
            {
                query = query.Where(a => (a.firstName + a.lastName).Contains(filter.fullNameFilter)).AsQueryable();
            }
            if (filter.dobFilter != null)
            {
                query = query.Where(a => a.DOB.Date == filter.dobFilter.Value.Date).AsQueryable();
            }
            return query;
        }
    }
}
