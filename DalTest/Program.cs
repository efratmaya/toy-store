namespace Dal;

using DalApi;
using DO;
using System;
enum type
{
    order, orderItem, product
}
class Program 

{
    private static DalList dalList = new DalList();
    private static DalOrder dalOrder = new DalOrder();
    private static DalOrderItem dalOrderItem = new DalOrderItem();
    private static DalProduct dalProduct = new DalProduct();

    public static void Main(string[] args)
    {
        
        Console.WriteLine("Please enter " +
            "\n0 for exit " +
            "\n1 for product " +
            "\n2 for order " +
            "\n3 for orderitem");
        int choice = int.Parse(Console.ReadLine());
        while (choice != 0)
        {
            switch (choice)
            {
                case 1:
                    _product();
                    break;
                case 2:
                    _order();
                    break;
                case 3:
                    _orderItem();
                    break;
            }
            Console.WriteLine(
                "Please enter " +
                "\n0 for exit " +
                "\n1 for product " +
                "\n2 for order" +
                "\n3 for orderitem");
            choice = int.Parse(Console.ReadLine());
        }

    }

    public static void _product()
    {
        Console.WriteLine("Please enter " +
    "\na to add one product" +
    "\nb to present one product" +
    "\nc to present all product" +
    "\nd to update product product" +
    "\ne to delete product product");
        char chosenAction;
        chosenAction = Convert.ToChar(Console.ReadLine());
        switch (chosenAction)
        {
            case 'a':
                //add product
                DO.Product product = new DO.Product();
                Console.WriteLine("please enter the products name");
                product.productName = Console.ReadLine();
                Console.WriteLine("please enter the products category:" +
                    "\n 0 for assemblyGames" +
                    "\n 1 for boxGames" +
                    "\n 2 for outdoorGames" +
                    "\n 3 for dolls ");
                product.productCategory = (enumCategory)int.Parse(Console.ReadLine());
                Console.WriteLine("please enter the products id");
                product.productId = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter the products price");
                product.productPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter the products amount in stock");
                product.productAmountInStock = int.Parse(Console.ReadLine());
                try
                {
                    dalList.product.Add(product);
                }
                catch (NoMoreSpaceInArray ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
            case 'b':
                //present product by id
                Console.WriteLine("enter id");
                int readProductId = int.Parse(Console.ReadLine());
                try
                {
                    Product product1 = dalList.product.Get(readProductId);
                    Console.WriteLine(product1);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c':
                //present all products
                try
                {
                    IEnumerable<Product> allProducts = dalList.product.ReadAll();
                    foreach (var item in allProducts)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }

                break;
            case 'd':
                //update product          
                DO.Product updateProduct = new DO.Product();
                Console.WriteLine("please enter the products id");
                int productId = int.Parse(Console.ReadLine());
                Console.WriteLine(dalProduct.Get(productId));
                Console.WriteLine("please enter the products name");
                updateProduct.productName = Console.ReadLine();
                Console.WriteLine("please enter the products category:" +
                    "\n 0 for assemblyGames" +
                    "\n 1 for boxGames" +
                    "\n 2 for outdoorGames" +
                    "\n 3 for dolls ");
                updateProduct.productCategory = (enumCategory)int.Parse(Console.ReadLine());
                Console.WriteLine("please enter the products id");
                updateProduct.productId = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter the products price");
                updateProduct.productPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter the products amount in stock");
                updateProduct.productAmountInStock = int.Parse(Console.ReadLine());
                try
                {
                    dalList.product.Update(updateProduct);
                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }

                break;
            case 'e':
                //delete product6
                try
                {
                    Console.WriteLine("enter id");
                    int deleteProductId = int.Parse(Console.ReadLine());
                    dalList.product.Delete(deleteProductId);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;

        }
    }
    private static void _order()
    {
        Console.WriteLine("Please enter " +
        "\na to add one order" +
        "\nb to present one order" +
        "\nc to present all orders" +
        "\nd to update product order" +
        "\ne to delete product order");
        char chosenAction;
        chosenAction = Convert.ToChar(Console.ReadLine());
        switch (chosenAction)
        {
            case 'a':
                //add order
                DO.Order order = new DO.Order();
                Console.WriteLine("please enter clientName");
                order.clientName = Console.ReadLine();
                Console.WriteLine("please enter clientEmail");
                order.clientEmail = Console.ReadLine();
                Console.WriteLine("please enter address For Delivery");
                order.addressForDelivery = Console.ReadLine();
                Console.WriteLine("please enter date ordered");
                order.dateOrdered = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("please enter date sent");
                order.dateSent = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("please enter date delivered");
                order.dateDelivered = Convert.ToDateTime(Console.ReadLine());
                try
                {
                    dalList.order.Add(order);
                }
                catch (NoMoreSpaceInArray ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 'b':
                //present order by id
                Console.WriteLine("enter id");
                int readOrderId = int.Parse(Console.ReadLine());
                try
                {
                    Order order1 = dalList.order.Get(readOrderId);
                    Console.WriteLine(order1);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c':
                //present all orders
                try
                {
                    IEnumerable<Order> allOrders = dalList.order.ReadAll();
                    foreach (var item in allOrders)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
                break;
            case 'd':
                //update order
                DO.Order updateOrder = new DO.Order();
                Console.WriteLine("please enter oreders id");
                int orderId = int.Parse(Console.ReadLine());
                Console.WriteLine(dalList.order.Get(orderId));
                Console.WriteLine("please enter clientName");
                updateOrder.clientName = Console.ReadLine();
                Console.WriteLine("please enter clientEmail");
                updateOrder.clientEmail = Console.ReadLine();
                Console.WriteLine("please enter address For Delivery");
                updateOrder.addressForDelivery = Console.ReadLine();
                Console.WriteLine("please enter date ordered");
                updateOrder.dateOrdered = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("please enter date sent");
                updateOrder.dateSent = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("please enter date delivered");
                updateOrder.dateDelivered = Convert.ToDateTime(Console.ReadLine());

                try
                {
                    dalList.order.Update(updateOrder);
                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e':
                //delete order
                int deleteOrderId = int.Parse(Console.ReadLine());
                try
                {
                    dalList.order.Delete(deleteOrderId);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }
                break;
        }
    }
    private static void _orderItem()
    {
        Console.WriteLine("Please enter " +
        "\na to add one orderItem" +
        "\nb to present one orderItem" +
        "\nc to present all orderItem" +
        "\nd to update product orderItem" +
        "\ne to delete product orderItem" +
        "\nf to present orderProduct by oderId and productId" +
        "\ng to present all orderProducts in a specific order by orderId");
        char chosenAction;
        chosenAction = Convert.ToChar(Console.ReadLine());
        switch (chosenAction)
        {
            case 'a':
                //add orderProduct
                DO.OrderItem orderItem = new DO.OrderItem();
                Console.WriteLine("please enter order id");
                orderItem.orderId = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter item id");
                orderItem.itemId = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter price for unit");
                orderItem.priceForUnit = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter amount");
                orderItem.amount = int.Parse(Console.ReadLine());
                try
                {
                    dalList.orderItem.Add(orderItem);
                }
                catch (NoMoreSpaceInArray ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
            case 'b':
                //present orderProduct by id
                Console.WriteLine("enter id");
                int id = int.Parse(Console.ReadLine());
                try
                {
                    OrderItem orderItem3 = dalList.orderItem.Get(id);
                    Console.WriteLine(orderItem3);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'c':
                //present all orderProducts
                try
                {
                    IEnumerable<OrderItem> allOrderItems = dalList.orderItem.ReadAll();
                    foreach (var item in allOrderItems)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 'd':
                //update orderProduct
                DO.OrderItem updateOrderItem = new DO.OrderItem();
                Console.WriteLine("please enter orderItemId");
                int orderItemId = int.Parse(Console.ReadLine());
                Console.WriteLine(dalList.orderItem.Get(orderItemId));
                Console.WriteLine("please enter order id");
                updateOrderItem.orderId = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter item id");
                updateOrderItem.itemId = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter price for unit");
                updateOrderItem.priceForUnit = int.Parse(Console.ReadLine());
                Console.WriteLine("please enter amount");
                updateOrderItem.amount = int.Parse(Console.ReadLine());
                try
                {
                    dalList.orderItem.Update(updateOrderItem);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 'e':
                //delete orderProduct
                int deleteOrderItemId = int.Parse(Console.ReadLine());
                try
                {
                    dalList.orderItem.Delete(deleteOrderItemId);

                }
                catch (ObjectNotFound ex)
                {

                    Console.WriteLine(ex.Message);
                }
                break;
            case 'f':
                //present orderProduct by oderId and productId
                Console.WriteLine("enter orderId and productId");
                int orderId = int.Parse(Console.ReadLine());
                int productId = int.Parse(Console.ReadLine());
                try
                {
                    OrderItem orderItem4 = dalList.orderItem.Get(element => element.orderId == orderId && element.itemId == productId);
                    Console.WriteLine(orderItem4);
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
            case 'g':
                // present all orderProducts in a specific order by orderId
                Console.WriteLine("enter orderId");
                int orderId2 = int.Parse(Console.ReadLine());
                try
                {
                    IEnumerable<OrderItem> arrOrderItems = dalList.orderItem.ReadAll(element => element.orderId == orderId2);

                    foreach (var item in arrOrderItems)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (ObjectNotFound ex)
                {
                    Console.WriteLine(ex.Message);
                }

                break;
        }
    }
}
