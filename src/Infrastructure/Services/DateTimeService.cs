﻿using Application.Common.Interfaces;
using System;

namespace Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
