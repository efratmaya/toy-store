using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DO;

namespace BlImplementation
{
    internal class BLOrder : IOrder
    {
        private DalApi.IDal dal = DalApi.Factory.Get();

        public IEnumerable<OrderForList> GetOrders()
        {
            IEnumerable<DO.Order> orders = dal.order.ReadAll();
            List<BO.OrderForList> orderForLists = new List<BO.OrderForList>();
            foreach (DO.Order order in orders)
            {
                IEnumerable<DO.OrderItem> ordersItems = new List<DO.OrderItem>();
                try
                {
                    ordersItems = dal.orderItem.ReadAll(element => element.orderId == order.orderId);
                }
                catch (Exception ex)
                {
                    throw new BO.DalException(ex);
                }

                BO.OrderForList tempOrderForList = new BO.OrderForList();
                foreach (var item in ordersItems)
                {
                    tempOrderForList.AmountProducts += item.amount;
                    tempOrderForList.TotalPrice += item.amount * item.priceForUnit;
                }

                tempOrderForList.IdOrder = order.orderId;
                tempOrderForList.CustomersName = order.clientName;
                if (order.dateDelivered == DateTime.MaxValue && order.dateSent == DateTime.MaxValue)
                    tempOrderForList.OrderCondition = BO.eCondition.OrderConfirmed;
                else if (order.dateDelivered == DateTime.MaxValue)
                    tempOrderForList.OrderCondition = BO.eCondition.OrderSent;
                else
                    tempOrderForList.OrderCondition = BO.eCondition.OrderSupllied;
                try
                {
                    orderForLists.Add(tempOrderForList);
                }
                catch (Exception ex)
                {
                    throw new BO.DalException(ex);
                }
            }

           
            return orderForLists;
        }

        public BO.Order GetOrdersDetails(int OrderId)
        {
         
            try
            {
                DO.Order orderData = dal.order.Get(OrderId);
              
                try
                {
                    IEnumerable<DO.OrderItem> orderItemsData = dal.orderItem.ReadAll(element => element.orderId == OrderId);
                    BO.Order order = new();

                    order.clientName = orderData.clientName;
                    order.clientEmail = orderData.clientEmail;
                    order.orderId = OrderId;
                    order.addressForDelivery = orderData.addressForDelivery;
                    order.dateOrdered = orderData.dateOrdered;
                    order.dateSent = orderData.dateSent;
                    order.dateDelivered = orderData.dateDelivered;
                    if (order.dateDelivered <= DateTime.Now)
                        order.orderCondition = BO.eCondition.OrderSupllied;
                    else
                    {
                        if (order.dateSent <= DateTime.Now)
                            order.orderCondition = BO.eCondition.OrderSent;
                        else
                            order.orderCondition = BO.eCondition.OrderConfirmed;
                    }
                    List<BO.OrderItem> myOrderItems = new();
                    foreach (var item in orderItemsData)
                    {
                        BO.OrderItem tmpOrderItems = new();
                        tmpOrderItems.amount = item.amount;
                        tmpOrderItems.itemName = dal.product.Get(item.itemId).productName;
                        tmpOrderItems.orderItemId = item.orderItemId;
                        tmpOrderItems.itemId = item.itemId;
                        tmpOrderItems.priceForUnit = dal.product.Get(item.itemId).productPrice;
                        tmpOrderItems.sumPrice = tmpOrderItems.amount * tmpOrderItems.priceForUnit;
                        order.totalPrice += tmpOrderItems.sumPrice;
                        myOrderItems.Add(tmpOrderItems);
                    }

                    order.items = myOrderItems;
                    return order;
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }



        }

   

        public BO.Order UpdateSent(int OrderId)
        {
            try
            {

                DO.Order orderData = dal.order.Get(OrderId);

                BO.Order order = new();

                if (orderData.dateSent < DateTime.Now)
                {
                    throw new OrderAlreadySent();
                }
                orderData.dateSent = DateTime.Now;
                IEnumerable<DO.OrderItem> orderItems = dal.orderItem.ReadAll(element => element.orderId == OrderId);
                order.clientName = orderData.clientName;
                order.clientEmail = orderData.clientEmail;
                order.orderId = OrderId;
                order.addressForDelivery = orderData.addressForDelivery;
                order.dateOrdered = orderData.dateOrdered;
                order.dateSent = orderData.dateSent;
                order.dateDelivered = orderData.dateDelivered;
                List<BO.OrderItem> myOrderItem = new();
                foreach (DO.OrderItem item in orderItems)
                {
                    BO.OrderItem tmpOrderItem = new();
                    tmpOrderItem.amount = item.amount;
                    tmpOrderItem.itemName = dal.product.Get(item.itemId).productName;
                    tmpOrderItem.orderItemId = item.orderItemId;
                    tmpOrderItem.itemId = item.itemId;
                    tmpOrderItem.priceForUnit = dal.product.Get(item.itemId).productPrice;
                    tmpOrderItem.sumPrice = tmpOrderItem.amount * tmpOrderItem.priceForUnit;
                    myOrderItem.Add(tmpOrderItem);
                }

                order.items = myOrderItem;
                try
                {
                    dal.order.Update(orderData);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }

                return order;
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }

        public BO.Order UpdateDelivered(int OrderId)
        {
            try
            {
                IEnumerable<DO.OrderItem> orderItems = dal.orderItem.ReadAll(element => element.orderId == OrderId);
                DO.Order orderData = dal.order.Get(OrderId);
                BO.Order order = new();


                if (orderData.dateDelivered < DateTime.Now)
                {
                    throw new OrderAlreadyDelivered();
                }
                orderData.dateDelivered = DateTime.Now;
                order.clientName = orderData.clientName;
                order.clientEmail = orderData.clientEmail;
                order.orderId = OrderId;
                order.addressForDelivery = orderData.addressForDelivery;
                order.dateOrdered = orderData.dateOrdered;
                order.dateSent = orderData.dateSent;
                order.dateDelivered = orderData.dateDelivered;
                List<BO.OrderItem> myOrderItem = new();
                foreach (var item in orderItems)
                {
                    BO.OrderItem tmpOrderItem = new();
                    tmpOrderItem.amount = item.amount;
                    tmpOrderItem.itemName = dal.product.Get(item.itemId).productName;
                    tmpOrderItem.orderItemId = item.orderItemId;
                    tmpOrderItem.itemId = item.itemId;
                    tmpOrderItem.priceForUnit = dal.product.Get(item.itemId).productPrice;
                    tmpOrderItem.sumPrice = tmpOrderItem.amount * tmpOrderItem.priceForUnit;
                    myOrderItem.Add(tmpOrderItem);
                }

                order.items = myOrderItem;
                try
                {
                    dal.order.Update(orderData);
                }
                catch (Exception ex)
                {
                    throw new DalException(ex);
                }

                return order;
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }
        }

        public BO.OrderTracking orderTracking(int orderId)
        {
            DO.Order oDO;
            try
            {
                oDO = dal.order.Get(orderId);

            }
            catch (Exception ex)
            {

                throw new DalException(ex);
            }
            BO.Order order = new BLOrder().GetOrdersDetails(orderId);
            Dictionary<DateTime, string> statusDictionary = new();
            if (order.orderCondition >= BO.eCondition.OrderConfirmed) statusDictionary.Add(order.dateOrdered, "Confirmed");
            if (order.orderCondition >= BO.eCondition.OrderSent) statusDictionary.Add(order.dateSent, "sent");
            if (order.orderCondition >= BO.eCondition.OrderSupllied) statusDictionary.Add(order.dateDelivered, "supllied");

            return new BO.OrderTracking()
            {
                OrderId = order.orderId,
                OrderCondition = order.orderCondition,
                OrderConditionWithDate = statusDictionary
            };

        }
        public int getOrderToUpdate()
        {
            lock (dal ?? throw new Exception())
            {
                var result = (from order in GetOrders()
                              where order.OrderCondition != eCondition.OrderSupllied
                              select new { orderId = order.IdOrder, name = order.CustomersName, orderLastTreat = orderTracking(order.IdOrder).OrderConditionWithDate?.Last().Key })
                         .OrderBy(x => x.orderLastTreat)
                         .FirstOrDefault();
                return result == null ? 0 : result.orderId;
            }
        }

    }

}
