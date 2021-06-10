using Application.Common.Interfaces;
using Application.Common.Interfaces.DataServiceInterfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.Controllers
{
    public class ProductImageController : Controller
    {
        private readonly IProductImageService productImageService;
        private readonly IMapper mapper;

        public ProductImageController(IProductImageService productImageService, IMapper mapper)
        {
            this.productImageService = productImageService;
            this.mapper = mapper;
        }
    }
}
