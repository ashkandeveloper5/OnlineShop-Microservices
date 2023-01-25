using Order.Core.Interfaces;
using Order.Core.Services;
using Order.Data.Context;
using Order.Data.Repositories;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderContext _context;
        public UnitOfWork(OrderContext context)
        {
            _context = context;
        }

        #region OrderService
        private ICheckoutService _checkoutService;
        public ICheckoutService CheckoutService
        {
            get
            {
                if (_checkoutService == null)
                {
                    _checkoutService = new CheckoutService(new AsyncRepository<OrderEntity>(_context));
                }
                return _checkoutService;
            }
        }
        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
