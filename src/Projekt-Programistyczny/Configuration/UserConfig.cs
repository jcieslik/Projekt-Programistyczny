using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Configuration
{
    public class UserConfig : IUserConfig
    {
        public string StripeSecret { get; set; }
    }
}
