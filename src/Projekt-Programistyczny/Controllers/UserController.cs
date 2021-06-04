using Application.Common.Interfaces;
using Application.DAL.DTO;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [Route("Authenticate")]
        public async Task<UserDTO> Authenticate(string login, string password)
        {
            var user = await userService.AuthenticateUser(login, password);
            if(user == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return null;
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
            return mapper.Map<UserDTO>(user);
        }

        [HttpGet]
        [Route("Deauthenticate")]
        public async Task Deauthenticate()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            currentUserService.Id = Guid.Empty;
        }

    }
}
