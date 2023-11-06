using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public int orderId { get; set; }
        public string clientName { get; set; }
        public string clientEmail { get; set; }
        public string addressForDelivery { get; set; }
        public DateTime dateOrdered { get; set; }
        public DateTime dateSent { get; set; }
        public DateTime dateDelivered { get; set; }
        public eCondition orderCondition { get; set; }
        public List<BO.OrderItem> items { get; set; }
        public double totalPrice { get; set; }

        public override string ToString() => $@"
       Order ID: {orderId},
       Client Name: {clientName},
       Client Email: {clientEmail},
       Address For Delivery: {addressForDelivery},
 Order Condition: {orderCondition},
       Date Ordered: {dateOrdered},
       Date Sent: {dateSent},
       Date Delivered: {dateDelivered},
total price:{totalPrice}

            ";
    }
}
