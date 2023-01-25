using Order.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.UOW
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
        ICheckoutService CheckoutService { get; }
    }
}
