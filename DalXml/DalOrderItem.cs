using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
       public int Add(OrderItem item)
        {
            if (item.orderItemId == 0)//status of add
            {
                XElement? Config = XDocument.Load("../xml/Config.xml").Root;
                XElement? orderItemId = Config?.Element("OrderItem");
                int newId = Convert.ToInt32(orderItemId?.Value);
                item.orderItemId = newId ++;

                orderItemId.Value = newId.ToString();/*.ToString()*/;
                Config?.Save("../xml/config.xml");
            }

            XElement ? OrdersItems = XDocument.Load("../xml/OrderItem.xml").Root;
            XElement? orderItem = new XElement("OrderItem");
            XElement? Id = new XElement("orderItemId", item.orderItemId);
            orderItem.Add(Id);
            XElement? ProductId = new XElement("itemId", item.itemId);
            orderItem.Add(ProductId);
            XElement? OrderID = new XElement("orderId", item.orderId);
            orderItem.Add(OrderID);
            XElement? Price = new XElement("priceForUnit", item.priceForUnit);
            orderItem.Add(Price);
            XElement? Amount = new XElement("amount", item.amount);
            orderItem.Add(Amount);
            OrdersItems?.Add(orderItem);
            OrdersItems?.Save("../xml/OrderItem.xml");
            return item.itemId;
        }

        public void Delete(int id)
        {
            XElement? OrdersItems = XDocument.Load("../xml/OrederItem.xml").Root;
            OrdersItems?.Elements().ToList().Find(orderItem => 
            Convert.ToInt32(orderItem?.Element("ID")?.Value) == id)?.Remove();
            OrdersItems?.Save("../xml/OrederItem.xml");
        }

        public OrderItem Get(int id)
        {
            XElement? OrdersItems = XDocument.Load("../xml/OrderItem.xml").Root;
            var found = OrdersItems?.Elements().ToList().Find(OrderItem => Convert.ToInt32(OrderItem?.Element("Id")?.Value) == id);
            if (found == null)
                throw new Exception();
            return new DO.OrderItem { itemId = Convert.ToInt32(found?.Element("ID")?.Value),
                orderItemId = Convert.ToInt32(found?.Element("itemId")?.Value),
                orderId = Convert.ToInt32(found?.Element("orderId")?.Value), 
                priceForUnit = Convert.ToInt32(found?.Element("priceForUnit")?.Value),
                amount = Convert.ToInt32(found?.Element("amount")?.Value) };
        }

        public OrderItem Get(Predicate<OrderItem> func)
        {
            IEnumerable<DO.OrderItem> lst = ReadAll();
            return lst.ToList().Find(func);
        }

        public OrderItem ProductItemByOrderIDProductID(int orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func=null)
        {
            XElement? OrdersItems = XDocument.Load("../xml/OrderItem.xml").Root;
            List<DO.OrderItem> lst = new List<DO.OrderItem> { };
            OrdersItems?.Elements().ToList().ForEach(o =>
            {
                lst.Add(new DO.OrderItem {
                    orderItemId = Convert.ToInt32(o?.Element("orderItemId")?.Value),
                    itemId = Convert.ToInt32(o?.Element("itemId")?.Value),
                    orderId = Convert.ToInt32(o?.Element("orderId")?.Value),
                    priceForUnit = Convert.ToInt32(o?.Element("priceForUnit")?.Value),
                    amount = Convert.ToInt32(o?.Element("amount")?.Value) });
            });
            if (func == null)
                return lst; 
            return lst.Where(func);
        }

        public List<OrderItem> ReadAllProductInOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderItem item)
        {
            throw new NotImplementedException();
        }
    }
}
