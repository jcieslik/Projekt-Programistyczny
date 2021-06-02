using Application.Common.Interfaces;
using System;

namespace Projekt_Programistyczny.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid Id { get; set; }
    }
}
