using cw_8_ko_s23109.DTOs;
using cw_8_ko_s23109.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Threading.Tasks;

namespace cw_8_ko_s23109.Services
{
    public interface IMedService
    {

        public Task AddDoctor(DoctorPost doctorpost);

        public Task RemoveDoctor(int idDoctor);

        public Task<Doctor> GetDoctor(int idDoctor);

        public Task<Doctor> UpdateDoctor(int idDoctor , DoctorPost doctorpost);
        
    }
}
