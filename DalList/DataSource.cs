using DO;
using System;
using System.Net;

namespace Dal;

static internal class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }

    readonly static Random random = new Random();
    static internal List<DO.Order> orders = new();
    private static void addOrder(DO.Order order)
    {
        orders.Add(order);
    }
    static internal List<DO.OrderItem> orderItems = new();
    private static void addOrderItem(DO.OrderItem orderItem)
    {
        orderItems.Add(orderItem);
    }
    static internal List<DO.Product> products = new();
    private static void addProduct(DO.Product product)
    {
        products.Add(product);
    }
    private static void s_Initialize()
    {
        int daysForSending, daysForDeliver;
        (DO.enumCategory productCategory, string productName)[] toys = new[] {
            (DO.enumCategory.boxGames, "domino"),
            (DO.enumCategory.boxGames,"monopol"),
            (DO.enumCategory.outdoorGames, "rope"),
            (DO.enumCategory.outdoorGames,"bikes"),
            (DO.enumCategory.outdoorGames,"ball"),
            (DO.enumCategory.assemblyGames,"clicks"),
            (DO.enumCategory.assemblyGames,"chapel"),
            (DO.enumCategory.assemblyGames,"lego"),
            (DO.enumCategory.dolls,"puppy"),
            (DO.enumCategory.dolls,"american doll")};

        for (int i = 0; i < 10; i++)
        {
            DO.Product product = new DO.Product();
            int randomProductId = config.ProductId;
            for (int j = 0; j < i; j++)//אולי קטן מ:i?
            {
                if (randomProductId == products[j].productId)
                {
                    randomProductId = config.ProductId;
                    j = -1;//-1
                }
            }
            product.productId = randomProductId;
            product.productName = toys[i].productName;
            product.productCategory = toys[i].productCategory;
            product.productPrice = random.NextDouble();
            product.productAmountInStock = (int)random.NextInt64(0, 20);
            addProduct(product);
        }
        (string clientName, string clientEmail, string addressForDelivery)[] ordersdata = new[] {
            ("Ester Haimovich", "sterhaimov@gmail.com","Nof Ramot 73"),
            ("Rachel Cohen", "Rachel1234@gmail.com","Argov 8"),
            ("Ruty Elyiach", "Ruty4567@gmail.com","David Meretz 72"),
            ("Batya Shapira", "Batya8520@gmail.com","Agasi 1"),
            ("Chana Rotenberg", "Chana7542@gmail.com","Hapisga 29"),
            ("Michal Levy", "Michal8888@gmail.com","Casuto 2"),
            ("Yudit Shub", "Yudit7832@gmail.com","Rashi 111"),
            ("Tzipi Chevroni", "Tzipi2222@gmail.com","Lilach 87"),
            ("Shira Rozenthal", "Shira8543@gmail.com","Kadish Looz 3"),
            ("Esther Ebert", "Esther7544@gmail.com","Bayit Vegan 75"),
            ("Leah Yudlov", "Leah5632@gmail.com","Elyashiv 19"),
            ("Chaya Rosenberg", "Chaya1111@gmail.com","Moshe Zilberg 15"),
            ("Shani Zeevi", "Shani1254@gmail.com","Mutzafi 79"),
            ("Hadassah Teib", "Hadassah4566@gmail.com","Broyer 84"),
            ("Orit Reiter", "Orit7520@gmail.com","Mintz 44"),
            ("Dasi Feld", "Dasi7899@gmail.com","sulam Yaacov 6"),
            ("Shifra Fisher", "Shifra4500@gmail.com","Zolti 96"),
            ("Tzvi Madmon", "Tzvi3333@gmail.com","Fatal 82"),
            ("Yaacov Goldsmidth", "Yaacov5566@gmail.com","Brand 102"),
            ("Yitzchak Omesi", "Yitzchak1010@gmail.com","Druk 73"),};

        //for (int i = 0; i < 20; i++)
        //{
        //    daysForSending = (int)random.NextInt64(2, 3);
        //    daysForDeliver = (int)random.NextInt64(4, 12);
        //    DO.Order order = new DO.Order();
        //    order.orderId = config.OrderId;
        //    order.clientName = orders[i].clientName;
        //    order.clientEmail = orders[i].clientEmail;
        //    order.addressForDelivery = orders[i].addressForDelivery;
        //    order.dateOrdered = DateTime.MinValue;
        //    TimeSpan tdaysForSending = new TimeSpan(daysForSending, 0, 0, 0);
        //    order.dateSent = order.dateOrdered.Add(tdaysForSending);
        //    TimeSpan tdaysForDeliver = new TimeSpan(daysForDeliver, 0, 0, 0);
        //    order.dateDelivered = order.dateSent.Add(tdaysForDeliver);
        //    addOrder(order);
        //}

        //for (int i = 0; i < 10; i++)
        //{
        //    int randomOrder = (int)random.NextInt64(0, orders.Count);
        //    int randomProduct = (int)random.NextInt64(0,products.Count);
        //    int randomAmount = (int)random.NextInt64(1, 20);
        //    DO.OrderItem orderItem = new DO.OrderItem();
        //    orderItem.orderItemId = config.OrderItemId;
        //    orderItem.orderId = orders[randomOrder].orderId;
        //    orderItem.itemId = products[randomProduct].productId;
        //    orderItem.priceForUnit = products[randomProduct].productPrice;
        //    orderItem.amount = randomAmount;
        //    addOrderItem(orderItem);
        //}


        for (int i = 0; i < ordersdata.Length; i++)
        {
            Order order = new Order();
            int index1 = (int)random.Next(20);
            daysForSending = (int)random.Next(1, 3);
            daysForDeliver = (int)random.Next(3, 7);
            order.orderId = config.OrderId;
            order.clientName = ordersdata[i].clientName;
            order.clientEmail = ordersdata[i].clientEmail;
            order.addressForDelivery = ordersdata[i].addressForDelivery;
            order.dateOrdered = DateTime.Now;
            if (i < ordersdata.Length * 0.2)//20% with just order date
            {
                order.dateSent = DateTime.MaxValue;
                order.dateDelivered = DateTime.MaxValue;
            }
            else
            {
                TimeSpan tDaysShip = new TimeSpan(daysForSending, 0, 0, 0);
                order.dateSent = order.dateOrdered.Add(tDaysShip);
                if (i < ordersdata.Length * 0.2 + (ordersdata.Length * 0.8 * 0.6))//60% of 80% with order, ship and delivery dates.
                {
                    TimeSpan tDaysDelivery = new TimeSpan(daysForDeliver, 0, 0, 0);
                    order.dateDelivered = order.dateOrdered.Add(tDaysDelivery);
                }
                else
                    order.dateDelivered = DateTime.MaxValue;//the other with just order and ship dates.
            }
            addOrder(order);
        }

        DO.OrderItem orderItem = new DO.OrderItem();
        for (int j = 0; j < orders.Count; j++)
        {
            int randomProduct = (int)random.NextInt64(0, products.Count);
            int randomAmount = (int)random.NextInt64(1, 4);
            orderItem.orderItemId = config.OrderItemId;
            orderItem.orderId = orders[j].orderId;
            orderItem.itemId = products[randomProduct].productId;
            orderItem.priceForUnit = products[randomProduct].productPrice;
            orderItem.amount = randomAmount;
            addOrderItem(orderItem);
        }
        int index = 0;
        for (int i = 0; i < 20; i += index)
        {
            index = (int)random.NextInt64(1, 3);
            int randomOrder = -1;
            randomOrder++;
            for (int j = 0; j < index; j++)
            {
                int randomProduct = (int)random.NextInt64(0, products.Count);
                int randomAmount = (int)random.NextInt64(1, 4);
                orderItem.orderItemId = config.OrderItemId;
                orderItem.orderId = orders[randomOrder].orderId;
                orderItem.itemId = products[randomProduct].productId;
                orderItem.priceForUnit = products[randomProduct].productPrice;
                orderItem.amount = randomAmount;
                addOrderItem(orderItem);
            }

        }

    }

    static internal class config
    {

        static private int productId = (int)random.NextInt64(100000, 1000000);
        static public int ProductId
        {
            get { return productId = (int)random.NextInt64(100000, 1000000); }

        }
        static private int orderId = 0;

        static public int OrderId
        {
            get { return orderId++; }
        }
        static private int orderItemId = 0;
        static public int OrderItemId
        {
            get { return orderItemId++; }

        }


    }

}