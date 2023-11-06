using DalApi;
using DO;
using System;
using static Dal.DataSource;

namespace Dal;

public class DalProduct : IProduct
{

    public int Add(DO.Product product)
    {
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].productId == product.productId)
                throw new AlreadyExistsSuchAnObject();
        }
        products.Add(product);
        return product.productId;

    }

    public DO.Product Get(int productId)
    {
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].productId == productId)
                return products[i];
        }
        throw new ObjectNotFound();
    }

    public DO.Product Get(Predicate<Product> p)
    {
        return products.Find(p);
    }

    public void Delete(int productId)
    {
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].productId == productId)
            {
                products.RemoveAt(i);
                return;
            }
        }
        throw new ObjectNotFound();
    }

    public void Update(DO.Product product)
    {
      
        for (int i = 0; i < products.Count(); i++)
        {
            if (products[i].productId == product.productId)
            {
                products[i] = product;
                return;
            }
        }
        throw new ObjectNotFound();
    }

    public IEnumerable<Product> ReadAll(Func<Product, bool>? func = null)
    {
        return (func == null) ? products : products.Where(func);
    }


}