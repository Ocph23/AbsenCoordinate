using AbsenCoordinatWeb.Data;
using AbsenCoordinatWeb.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbsenCoordinatWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MobileController : ControllerBase
    {
        private readonly AbsenService _absenService;
        private readonly CutiService _cutiService;
        private readonly UserService _userService;
        private readonly TempatService _tempatService;

        public MobileController( AbsenService absenService, CutiService cutiService, UserService userService, TempatService tempatService)
        {
            _absenService = absenService;
            _cutiService = cutiService;
            _userService = userService;
            _tempatService= tempatService;
        }
        [HttpGet("getabsen")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAbsen()
        {
            try
            {
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized("Maaf, Anda Tidak Memiliki Access !");
                IEnumerable<Absen> result = await _absenService.GetAbsenByUser(userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("absen")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DoAbsen()
        {
            try
            {
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized("Maaf, Anda Tidak Memiliki Access !");
                Absen result = await _absenService.DoAbsen(userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getcuti")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCuti()
        {
            try
            {
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized("Maaf, Anda Tidak Memiliki Access !");

                var result = await _cutiService.GetCutiByUser(userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createcuti")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateCuti(Cuti cuti)
        {
            try
            {
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized("Maaf, Anda Tidak Memiliki Access !");
                var result = await _cutiService.Save(cuti);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin login)
        {
            try
            {
               var result =  await _userService.Authenticate(login);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized("Maaf, Anda Tidak Memiliki Access !");

                var result = await _userService.GetProfile(userName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("tempat")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetTempatAbsen()
        {
            try
            {
                var userName = User.Identity.Name;
                if (string.IsNullOrEmpty(userName))
                    return Unauthorized("Maaf, Anda Tidak Memiliki Access !");


                var result = await _tempatService.GetTempat(userName);
                return Ok(result.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
