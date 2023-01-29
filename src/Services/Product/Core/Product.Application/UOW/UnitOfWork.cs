using Microsoft.EntityFrameworkCore;
using Product.Application.Interfaces.ProductInterfaces;
using Product.Application.Services.ProductServices;
using Product.Domain.Entities.ProductEntities;
using Product.Persistence.Context;
using Product.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProductContext _context;
        public UnitOfWork(ProductContext context)
        {
            _context = context;
        }

        //Product Service
        #region ProductService
        private IProductService _productService;
        public IProductService ProductService
        {
            get
            {
                if (_productService == null)
                {
                    _productService = new ProductService(new AsyncRepository<ProductEntity>(_context));
                }
                return _productService;
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
