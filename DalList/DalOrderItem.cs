using DalApi;
using DO;
using System;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem : IOrderItem
{


    public int Add(DO.OrderItem orderItem)
    {
        orderItem.orderItemId = config.OrderItemId;
        orderItems.Add(orderItem);
        return orderItem.orderId;
    }

    public DO.OrderItem Get(int orderItemId)
    {
        for (int i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i].orderItemId == orderItemId)
                return orderItems[i];
        }
        throw new ObjectNotFound();
    }
    public OrderItem Get(Predicate<OrderItem> p)
    {
        return orderItems.Find(p);
    }


    public void Delete(int orderItemId)
    {
        for (int i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i].orderItemId == orderItemId)
            {
                orderItems.RemoveAt(i);
                return;
            }

        }
        throw new ObjectNotFound();
    }


    public void Update(DO.OrderItem orderItem)
    {
       
        for (int i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i].orderItemId == orderItem.orderItemId)
            {
                orderItems[i] = orderItem;
                return;
            }
        }

        throw new ObjectNotFound();
    }
    public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func = null)
    {
        return (func == null) ? orderItems : orderItems.Where(func);
    }



    //function to  read all the orderProducts in specific order
    public List<OrderItem> ReadAllProductInOrder(int orderId)
    {

        int i;
        List<OrderItem> itemsInOrderArr = new List<OrderItem>();
        for (i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i].orderId == orderId)
            {
                itemsInOrderArr.Add(orderItems[i]);
            }
        }
        if (itemsInOrderArr.Count == 0)
        {
            throw new ObjectNotFound();
        }
        return itemsInOrderArr;
    }

    public DO.OrderItem ProductItemByOrderIDProductID(int orderId, int productId)
    {
        try
        {
            Get(orderId);
        }
        catch (Exception)
        {

            throw new ObjectNotFound();
        }
        try
        {
            Get(productId);
        }
        catch (Exception)
        {

            throw new ObjectNotFound();
        }
        for (int i = 0; i < config.OrderItemId; i++)
        {
            if (orderItems[i].orderId == orderId && orderItems[i].itemId == productId)
            {
                return orderItems[i];
            }

        }
        throw new ObjectNotFound();

    }
}
