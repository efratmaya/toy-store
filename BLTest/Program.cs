using BO;
using BlImplementation;
using BlApi;
using Dal;
using DO;
using System.Diagnostics;

namespace BLTest
{
    public class Program
    {
        private static IBL ibl = new Bl();
        public static void Main(string[] args)
        {
            Cart cart = new Cart();
            cart.ListOfItems = new List<BO.OrderItem>();
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for Cart " + "\n2 for Order " + "\n3 for Product");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        _Cart(cart);
                        break;
                    case 2:
                        _Order();
                        break;
                    case 3:
                        _Product();
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for Cart " + "\n2 for Order " + "\n3 for Product");
                choice = int.Parse(Console.ReadLine());
            }

        }
        public static Cart _Cart(Cart cart)
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for AddItem " + "\n3 for CheckOut");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1://update product
                        Console.WriteLine("insert products id");
                        int id = int.Parse(Console.ReadLine());
                        Console.WriteLine("insert amount of product");
                        int amount = int.Parse(Console.ReadLine());
                        try
                        {
                            cart = ibl.Cart.UpdateProduct(cart, id, amount);
                            Console.WriteLine("product updated succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (ProductNotInStock e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (NoSuchProductInCart e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case 2://add item

                        Console.WriteLine("insert product id");
                        int productId = int.Parse(Console.ReadLine());
                        try
                        {
                            cart = ibl.Cart.AddItem(cart, productId);
                            Console.WriteLine("product added succesfully!");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (ProductNotInStock e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;
                    case 3://checkOut
                        Console.WriteLine("insert customer name");
                        string customerName = Console.ReadLine();
                        Console.WriteLine("insert customer email");
                        string customerEmail = Console.ReadLine();
                        Console.WriteLine("insert customer address");
                        string customerAddress = Console.ReadLine();
                        try
                        {
                            cart.CustomersEmail = customerEmail;
                            cart.CustomersName = customerName;
                            cart.CustomersAddress = customerAddress;
                            ibl.Cart.CheckOut(cart);
                            Console.WriteLine("order confirmed!" + "\n thank you for ordering at our shop!");
                            cart.CustomersEmail = "";
                            cart.CustomersAddress = "";
                            cart.CustomersName = "";
                            cart.ListOfItems.Clear();
                            cart.TotalPriceOfCart = 0;
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (ProductNotInStock e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for AddItem " + "\n3 for CheckOut");
                choice = int.Parse(Console.ReadLine());
            }
            return cart;
        }
        public static void _Order()
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for GetOrders " + "\n2 for GetOrdersDetails " + "\n3 for UpdateSent" + "\n4 for UpdateDelivered");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1://get orders
                        try
                        {
                            IEnumerable<OrderForList> allOrders = ibl.Order.GetOrders();
                            foreach (var item in allOrders)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 2://GetOrdersDetails
                        Console.WriteLine("insert order id:");
                        int orderId = int.Parse(Console.ReadLine());
                        try
                        {
                            BO.Order orderDetails = ibl.Order.GetOrdersDetails(orderId);
                            Console.WriteLine(orderDetails);
                            Console.WriteLine("items:");
                            foreach(var item in orderDetails.items)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (IncorrectData e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 3://Update sent
                        Console.WriteLine("insert order id:");
                        int orderId1 = int.Parse(Console.ReadLine());
                        try
                        {
                            ibl.Order.UpdateSent(orderId1);
                            Console.WriteLine("order updated succesfully!");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (OrderAlreadySent e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 4://Update delivered
                        Console.WriteLine("insert order id:");
                        int orderId2 = int.Parse(Console.ReadLine());
                        try
                        {
                            ibl.Order.UpdateDelivered(orderId2);
                            Console.WriteLine("order updated succesfully!");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (OrderAlreadyDelivered e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for GetOrders " + "\n2 for GetOrdersDetails " + "\n3 for UpdateSent" + "\n4 for UpdateDelivered");
                choice = int.Parse(Console.ReadLine());
            }
        }
        public static void _Product()
        {
            Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for Update Product " + "\n2 for Get Catalog " + "\n3 for GetProducts" + "\n4 for Get Products Details" + "\n5 for Delete Product" + "\n6 for Add Product");
            int choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1://Update product
                        BO.Product updateProduct = new BO.Product();
                        Console.WriteLine("please enter the products id");
                        updateProduct.productId = int.Parse(Console.ReadLine());
                        try
                        {
                            BO.Product previousoProduct = ibl.Product.GetProductsDetails(updateProduct.productId);
                            Console.WriteLine(previousoProduct);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                            break;
                        }
                        catch (IncorrectData e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                        Console.WriteLine("please reenter the products name");
                        updateProduct.productName = Console.ReadLine();
                        Console.WriteLine("please reenter the products category:" + "\n 0 for assemblyGames" + "\n 1 for boxGames" + "\n 2 for outdoorGames" + "\n 3 for dolls ");
                        updateProduct.productCategory = (BO.eCategory)int.Parse(Console.ReadLine());
                        Console.WriteLine("please reenter the products price");
                        updateProduct.productPrice = int.Parse(Console.ReadLine());
                        Console.WriteLine("please reenter the products amount in stock");
                        updateProduct.productAmountInStock = int.Parse(Console.ReadLine());
                        try
                        {
                            ibl.Product.UpdateProduct(updateProduct);
                            Console.WriteLine("product updated succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 2://Get catalog
                        try
                        {
                            //BO.ProductItem productForCatalog = new BO.ProductItem();
                            //Console.WriteLine("insert product's id");
                            //int productId = int.Parse(Console.ReadLine());

                            IEnumerable < BO.ProductItem> productsInCatalog = ibl.Product.GetCatalog();
                            foreach (var productInCatalog in productsInCatalog)

                            {
                                Console.WriteLine(productInCatalog);
                            }
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 3://Get products
                        try
                        {
                            IEnumerable<BO.ProductForList> products = new List<BO.ProductForList>();
                            products = ibl.Product.GetProducts();
                            foreach (var item in products)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 4://Get products details
                        Console.WriteLine("please enter the products id");
                        int productId1= int.Parse(Console.ReadLine());
                        try
                        {
                            BO.Product productDetails = new BO.Product();
                            productDetails = ibl.Product.GetProductsDetails(productId1);
                            Console.WriteLine(productDetails);
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (IncorrectData e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 5://Delete product
                        Console.WriteLine("insert product's id:");
                        int id = int.Parse(Console.ReadLine());
                        try
                        {
                            ibl.Product.DeleteProduct(id);
                            Console.WriteLine("product deleted succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        break;
                    case 6://Add product
                        BO.Product addProduct = new BO.Product();
                        Console.WriteLine("please enter the products id");
                        addProduct.productId = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the products name");
                        addProduct.productName = Console.ReadLine();
                        Console.WriteLine("please enter the products category:" + "\n 0 for assemblyGames" + "\n 1 for boxGames" + "\n 2 for outdoorGames" + "\n 3 for dolls ");
                        addProduct.productCategory = (BO.eCategory)int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the products price");
                        addProduct.productPrice = int.Parse(Console.ReadLine());
                        Console.WriteLine("please enter the products amount in stock");
                        addProduct.productAmountInStock = int.Parse(Console.ReadLine());
                        try
                        {
                            ibl.Product.AddProduct(addProduct);
                            Console.WriteLine("product added succesfully");
                        }
                        catch (DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                        }
                        catch (IncorrectData e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                }
                Console.WriteLine("Please enter " + "\n0 for exit " + "\n1 for UpdateProduct " + "\n2 for GetProductForCatalog " + "\n3 for GetProducts" + "\n4 for GetProductsDetails" + "\n5 for DeleteProduct" + "\n6 for AddProduct");
                choice = int.Parse(Console.ReadLine());
            }
        }

    }
}

