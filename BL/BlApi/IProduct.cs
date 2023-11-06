using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<ProductForList> GetProducts(Func<DO.Product, bool>? func = null);
        public IEnumerable<BO.ProductItem>

 GetCatalog(Func<DO.Product, bool>? func = null);
        public Product GetProductsDetails(int ProductId);
        public void AddProduct(Product product);
        public void DeleteProduct(int ProductId);
        public void UpdateProduct(Product product);
        public IEnumerable<BO.ProductForList> GetProductByCategory(BO.eCategory category);
        public IEnumerable<BO.ProductItem> GetCatalogByCategory(BO.eCategory category);
    }
}
