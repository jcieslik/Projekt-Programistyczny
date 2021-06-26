using Application.Common.Interfaces.DataServiceInterfaces;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.BackgroundServices
{
    public class OfferWatchBackgroundService : BackgroundService
    {
        private readonly IOfferService _offerService;

        public OfferWatchBackgroundService(IOfferService offerService)
        {
            _offerService = offerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _offerService.ChangeStatusOfOutdatedOffers();
            }
            throw new NotImplementedException();
        }
    }
}
