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

        //Category Service
        #region CategoryService
        private ICategoryService _categoryService;
        public ICategoryService CategoryService
        {
            get
            {
                if (_categoryService == null)
                {
                    _categoryService = new CategoryService(new AsyncRepository<ProductGroup>(_context));
                }
                return _categoryService;
            }
        }
        #endregion

        //Comment Service
        #region CommentService
        private ICommentService _commentService;
        public ICommentService CommentService
        {
            get
            {
                if (_commentService == null)
                {
                    _commentService = new CommentService(new AsyncRepository<ProductComment>(_context));
                }
                return _commentService;
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
