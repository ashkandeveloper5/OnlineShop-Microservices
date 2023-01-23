using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Interfaces.BaseInterfaces;
using Product.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.IoC.DependencyContainer
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
        }
    }
}
