using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using Domain.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt_Programistyczny.Extensions;
using System.Collections.Generic;
using System.Linq;
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

        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            this.userService = userService;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("Authenticate")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

            if(user.Role == UserRole.Admin)
            {
                claims.Add(new Claim(type: "IsAdmin", value: "true"));
            }
            else
            {
                claims.Add(new Claim(type: "IsCustomer", value: "true"));
            }

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
        [Authorize]
        [Route("Deauthenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Deauthenticate()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            currentUserService.Id = 0;
            return Ok();

        }

        [HttpPost]
        [Route("CreateUser")]
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
            catch(NameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateUser")]
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
            catch (NameAlreadyInUseException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetUserInfo")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> GetUserInfo([FromQuery] long userId)
        {
            try
            {
                var user = await userService.GetUserById(userId);
                return Ok(user);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllMessageRecipients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RecipientDTO>> GetAllMessageRecipients()
        {
            var users = await userService.GetAllUsers();

            var currentUserId = HttpContext.User.GetUserId();

            users = users.Where(u => u.Id != currentUserId);

            return Ok(users);
        }

        [HttpPost]
        [Route("GetAllUsers")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDTO>> GetAllUsers([FromBody] PaginationProperties pagination)
        {
            var users = await userService.GetPaginatedUsers(pagination);

            return Ok(users);
        }

        [HttpPost]
        [Route("BanUser")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> BanUser([FromBody] BanDto banDto)
        {
            try
            {
                await userService.BanUser(banDto.BanInfo, banDto.UserId);
                return Ok(true);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UnbanUser")]
        [Authorize(Policy = "AdminOnly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UnbanUser([FromQuery] long userId)
        {
            try
            {
                await userService.UnbanUser(userId);
                return Ok(true);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
