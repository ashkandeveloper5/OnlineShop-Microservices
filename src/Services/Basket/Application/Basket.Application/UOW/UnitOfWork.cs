using Basket.Application.Interfaces.BasketInterfaces;
using Basket.Application.Services.BasketServices;
using Basket.Core.Entities.Order;
using Basket.DataAccess.Interfaces;
using Basket.DataAccess.Persistence.Context;
using Basket.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BasketContext _context;
        public UnitOfWork(BasketContext context)
        {
            _context= context;
        }

        #region BasketService
        private IBasketService _basketService;
        public IBasketService BasketService
        {
            get
            {
                if (_basketService==null)
                {
                    _basketService = new BasketService(new AsyncRepository<Order>(_context), new AsyncRepository<OrderDetail>(_context));
                }
                return _basketService;
            }
        }
        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
