using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ICurrentUserService currentUserService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, ICurrentUserService currentUserService, IMapper mapper)
        {
            this.userService = userService;
            this.currentUserService = currentUserService;
            this.mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("Authenticate")]
        public async Task<ActionResult<UserDTO>> Authenticate(string login, string password)
        {
            var user = await userService.AuthenticateUser(login, password);
            if(user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            currentUserService.Id = user.Id;
            return Ok(user);
        }

        [HttpPost]
        [Route("Deauthenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Deauthenticate()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            currentUserService.Id = Guid.Empty;
            return Ok();

        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] CreateUserDTO userData)
        {
            try
            {
                var user = await userService.CreateUserAsync(userData);
                return Ok(user);
            }
            catch(EmailAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch(UsernameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromBody] UpdateUserDTO userData)
        {
            try
            {
                var user = await userService.UpdateUserAsync(userData);
                return Ok(user);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (EmailAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (UsernameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }
    }
}
