using cw_8_ko_s23109.DTOs;
using cw_8_ko_s23109.Models;
using cw_8_ko_s23109.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace cw_8_ko_s23109.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class DoctorsController : ControllerBase
    {

        private readonly IMedService _service;

        public DoctorsController(IMedService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(DoctorPost doctorpost)
        {
            await _service.AddDoctor(doctorpost);
            return Created("",doctorpost);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctor(int idDoctor)
        {
            try { 
            await _service.RemoveDoctor(idDoctor);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetDoctor(int idDoctor)
        {
            try
            {
                return Ok(await _service.GetDoctor(idDoctor));  
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]

        public async Task<IActionResult> UpdateDoctor(int idDoctor, DoctorPost doctorpost)
        {
            try
            {
                return Ok(await _service.UpdateDoctor(idDoctor, doctorpost));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }

}
