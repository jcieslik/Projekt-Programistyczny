using Application.Common.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    public class RateController : Controller
    {
        private readonly IRateService rateService;
        private readonly IMapper mapper;

        public RateController(IRateService rateService, IMapper mapper)
        {
            this.rateService = rateService;
            this.mapper = mapper;
        }
    }
}
