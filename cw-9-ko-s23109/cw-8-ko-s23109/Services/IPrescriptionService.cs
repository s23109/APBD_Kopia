using cw_8_ko_s23109.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cw_8_ko_s23109.Services
{
    public interface IPrescriptionService
    {
        public Task<PrescriptionGet> GetPrescription(int IdPrescription);
    }
}
