using Product.Application.Interfaces.ProductInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.UOW
{
    public interface IUnitOfWork:IDisposable
    {
        void Save();
        IProductService ProductService { get; }
        ICategoryService CategoryService{ get; }
        ICommentService CommentService { get; }
    }
}
