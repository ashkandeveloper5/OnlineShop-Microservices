using Basket.Application.Interfaces.BasketInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.UOW
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
        IBasketService BasketService { get; }
    }
}
