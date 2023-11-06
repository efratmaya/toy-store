using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

using Dal;

namespace BlImplementation
{
    internal class BLProduct : IProduct
    {
        private  DalApi.IDal dal = DalApi.Factory.Get();
        public void AddProduct(BO.Product product)
        {

            if (product.productId < 0 || product.productName == "" || product.productPrice <= 0 || product.productAmountInStock < 0)
                throw new BO.IncorrectData();
            DO.Product product1 = new DO.Product();
            product1.productName = product.productName;
            product1.productPrice = product.productPrice;
            product1.productId = product.productId;
            product1.productAmountInStock = product.productAmountInStock;
            product1.productCategory = (DO.enumCategory)product.productCategory;
            try
            {
                dal.product.Add(product1);
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }

        }

        public void DeleteProduct(int ProductId)
        {
            try
            {
                dal.product.Delete(ProductId);
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }

        }

        public BO.Product GetProductsDetails(int ProductId)
        {
            if (ProductId < 0)
                throw new BO.IncorrectData();
            try
            {
                DO.Product productData = dal.product.Get(ProductId);
                BO.Product productForCatalog = new();
                productForCatalog.productId = productData.productId;
                productForCatalog.productName = productData.productName;
                productForCatalog.productPrice = productData.productPrice;
                productForCatalog.productAmountInStock = productData.productAmountInStock;
                productForCatalog.productCategory = (BO.eCategory)productData.productCategory;
                return productForCatalog;
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }

        }

        public IEnumerable<BO.ProductForList> GetProducts(Func<DO.Product, bool>? func = null)
        {


            List<BO.ProductForList> ProductList = new();
            IEnumerable <DO.Product> productsData = dal.product.ReadAll(func);
            foreach (DO.Product product in productsData)
            {
                BO.ProductForList tempProduct = new();
                tempProduct.NameProduct = product.productName;
                tempProduct.IdProduct = product.productId;
                tempProduct.PriceProduct = product.productPrice;
                tempProduct.CategoryProduct = (BO.eCategory)product.productCategory;
                ProductList.Add(tempProduct);
            }
      
            return ProductList;
        }

        public IEnumerable<BO.ProductItem>

 GetCatalog(Func<DO.Product, bool>? func = null)
        {
           List< BO.ProductItem> productsInCatalog = new();
            try
            {
                IEnumerable<DO.Product> productsData = dal.product.ReadAll(func);
                foreach (var productData in productsData)
                {
                    BO.ProductItem productInCatalog = new();
                    productInCatalog.NameProduct= productData.productName;
                    productInCatalog.IdProduct = productData.productId;
                    productInCatalog.PriceProduct = productData.productPrice;
                    productInCatalog.ProductCategory = (BO.eCategory)productData.productCategory;
                    if (productData.productAmountInStock > 0)
                    {
                        productInCatalog.InStock = true;
                    }
                    else
                    {
                        productInCatalog.InStock = false;
                    }
                    productsInCatalog.Add(productInCatalog);
                }
                return productsInCatalog;
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }

        }

        public void UpdateProduct(BO.Product product)
        {
            try

            {
                DO.Product updateProduct = dal.product.Get(product.productId);
                try
                {
                    updateProduct.productId = product.productId;
                    updateProduct.productPrice = product.productPrice;
                    updateProduct.productName = product.productName;
                    updateProduct.productCategory = (DO.enumCategory)product.productCategory;
                    dal.product.Update(updateProduct);
                }
                catch (Exception ex)
                {
                    throw new BO.DalException(ex);
                }
            }
            catch (Exception ex)
            {
                throw new BO.DalException(ex);
            }
        }
        public IEnumerable<BO.ProductForList> GetProductByCategory(BO.eCategory category)
        {
            return GetProducts((item => item.productCategory == (DO.enumCategory)category));

        }

        public IEnumerable<BO.ProductItem> GetCatalogByCategory(BO.eCategory category)
        {
            return GetCatalog((item => item.productCategory == (DO.enumCategory)category));

        }
    }
}
