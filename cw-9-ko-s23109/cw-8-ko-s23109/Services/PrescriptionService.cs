using cw_8_ko_s23109.DTOs;
using cw_8_ko_s23109.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw_8_ko_s23109.Services
{
    public class PrescriptionService : IPrescriptionService
    {

        private readonly MedDbContext _context;

        public PrescriptionService(MedDbContext context)
        {
            _context = context;
        }
        public async Task<PrescriptionGet> GetPrescription(int IdPrescription)
        {
            var prescription =  _context.Prescriptions.Where(e => e.IdPrescription == IdPrescription);

            if (!prescription.Any())
            {
                throw new Exception("Brak Istniejącej recepty");
            };

            
            
            // czemu nie chce tutaj awaita ?
            return (PrescriptionGet) _context.Prescriptions.Select(final => new PrescriptionGet
            {
                IdPrescription = final.IdPrescription,
                Date = final.Date,
                DueDate = final.Duedate,
                Patient = (Patient)_context.Patients.Where(pat => pat.IdPatient.Equals(final.IdPatient)),
                Doctor = (Doctor)_context.Doctors.Where(doc => doc.IdDoctor.Equals(final.IdDoctor)),
                Medicaments = _context.Medicaments
                .Where(e => 
                    _context.Prescription_Medicaments
                    .Where(ee => ee.IdPrescription == IdPrescription)
                    .Select(ee => ee.IdMedicament)
                .Contains(e.IdMedicament)).ToList()

        }
           );

        }
    }
}
