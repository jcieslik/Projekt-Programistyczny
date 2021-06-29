using Application.Common.Interfaces.DataServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Projekt_Programistyczny.CronServices
{
    [DisallowConcurrentExecution]
    public class OfferStatusChangeJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public OfferStatusChangeJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _serviceProvider.CreateScope();
            var offerService = scope.ServiceProvider.GetService<IOfferService>();
            await offerService.ChangeStatusOfOutdatedOffers();
        }
    }
}
