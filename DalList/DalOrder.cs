using DalApi;
using DO;
using System;
using System.Linq;
using static Dal.DataSource;

namespace Dal;
public class DalOrder : IOrder
{

    public int Add(DO.Order order)
    {

        Random random = new Random();
        order.orderId = config.OrderId;
        orders.Add(order);
        return order.orderId;
    }
    public DO.Order Get(int orderId)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].orderId == orderId)
                return orders[i];
        }
        throw new ObjectNotFound();
    }
    public DO.Order Get(Predicate<Order> p)
    {
        return orders.Find(p);
    }


    public void Delete(int orderId)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].orderId == orderId)
            {
                orders.RemoveAt(i);
                return;
            }
        }
        throw new ObjectNotFound();
    }



    public void Update(DO.Order order)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i].orderId == order.orderId)
            {
                orders[i] = order;
                return;
            }
        }
        throw new ObjectNotFound();
    }
    public IEnumerable<Order> ReadAll(Func<Order, bool>? func = null)
    {
        return (func == null) ? orders : orders.Where(func);
    }

    //public Order Read(int orderId)
    //{
    //    throw new NotImplementedException();
    //}
}