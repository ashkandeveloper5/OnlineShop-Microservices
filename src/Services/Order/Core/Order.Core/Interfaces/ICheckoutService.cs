using Order.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Interfaces
{
    public interface ICheckoutService:IDisposable
    {
        Task<GetOrderForCheckout> AddCheckout(GetOrderForCheckout orderForCheckout);
        Task<GetOrderForCheckout> GetCheckoutById(string ckeckoutId);
        Task<IEnumerable<GetOrderForCheckout>> GetAllCheckout();
    }
}
