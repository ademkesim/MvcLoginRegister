using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class JoinController : Controller
    {
        private IAuthService _authService;
        public JoinController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(AuthModel authModel)
        {
            if (!ModelState.IsValid)
            {
                return View(authModel);

            }

            var userToLogin = _authService.Login(authModel.userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);

        }
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExsits = _authService.UserExists(userForRegisterDto.Mail);
            if (!userExsits.Success)
            {
                return BadRequest(userExsits.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto,userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);

            }

            return BadRequest(result.Message);
        }

    }
}
