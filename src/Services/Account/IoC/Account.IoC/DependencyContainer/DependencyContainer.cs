using Account.Repository.DataTransfer.Interfaces;
using Account.Repository.DataTransfer.Repository;
using Account.Service.Interfaces;
using Account.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.IoC.DependencyContainer
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            //service.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
        }
    }
}
