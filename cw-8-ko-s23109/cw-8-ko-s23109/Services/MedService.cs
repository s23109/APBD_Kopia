using cw_8_ko_s23109.DTOs;
using cw_8_ko_s23109.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace cw_8_ko_s23109.Services
{
    public class MedService : IMedService
    {

        private readonly MedDbContext _context;

        public MedService(MedDbContext context)
        {
            _context = context;
        }

        public async Task AddDoctor(DoctorPost doctorpost)
        {

            try { 

            _context.Doctors.Add(new Doctor
            {
                Email = doctorpost.Email,
                FirstName = doctorpost.FirstName,
                LastName = doctorpost.LastName
            });
            await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<Doctor> GetDoctor(int idDoctor)
        {
            var doctor =  _context.Doctors.Where(e => e.IdDoctor.Equals(idDoctor));

            if (!doctor.Any())
            {
                throw new Exception("Brak doktora o podanym ID");
            }

            return (Doctor)doctor;
            

        }

        public async Task RemoveDoctor(int idDoctor)
        {
            var doctor = _context.Doctors.Where(e => e.IdDoctor.Equals(idDoctor));

            if (!doctor.Any())
            {
                throw new Exception("Brak doktora o podanym ID");
            }

            _context.Doctors.Remove((Doctor)doctor);
            _context.SaveChangesAsync();

        }

        public Task<Doctor> UpdateDoctor(int idDoctor, DoctorPost doctorpost)
        {
            var doctor = _context.Doctors.Where(e => e.IdDoctor.Equals(idDoctor));

            if (!doctor.Any())
            {
                throw new Exception("Brak doktora o podanym ID");
            }

            _context.Doctors.Where(e => e.IdDoctor == idDoctor).ToList().ForEach(e => { e.Email = doctorpost.Email; e.FirstName = doctorpost.FirstName; e.LastName = doctorpost.LastName; });

            return (Task<Doctor>)_context.Doctors.Where(e => e.IdDoctor == idDoctor);

        }

        
    }
}
