using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;



namespace BlImplementation
{
    internal class BLCart : BlApi.ICart
    {
        private  DalApi.IDal dal = DalApi.Factory.Get();
        public BO.Cart AddItem(BO.Cart cart, int ProductId)
        {
            //int i;
            for (int i = 0; i < cart.ListOfItems.Count(); i++)
            {
                if (cart.ListOfItems[i].itemId == ProductId)//בודק האם הפריט קיים בסל
                {
                    int amountInStock = dal.product.Get(ProductId).productAmountInStock;//מקבלים את הכמות של המוצר במלאי
                    if (amountInStock > cart.ListOfItems[i].amount)
                    {
                        cart.ListOfItems[i].amount++;//מגדילים את הכמות מאותו המוצר
                        cart.ListOfItems[i].sumPrice = cart.ListOfItems[i].amount * cart.ListOfItems[i].priceForUnit;//מחשבים את המחיר של הפריט המרובה
                        cart.TotalPriceOfCart += cart.ListOfItems[i].priceForUnit;
                        return cart;
                    }
                    else
                        throw new ProductNotInStock();
                }
            }
            try
            {
                DO.Product productData = dal.product.Get(ProductId);

                if (productData.productAmountInStock > 0)
                {
                    BO.OrderItem product = new BO.OrderItem();
                    product.amount = 1;
                    product.priceForUnit = productData.productPrice;
                    product.sumPrice = productData.productPrice;
                    product.itemId = ProductId;
                    product.itemName = productData.productName;
                    product.orderItemId = 0;
                    cart.TotalPriceOfCart += productData.productPrice;

                    cart.ListOfItems.Add(product);
                    return cart;
                }
                throw new ProductNotInStock();
            }
            catch (Exception ex)
            {
                throw new DalException(ex);
            }

        }

        public void CheckOut(BO.Cart cart)
        {
            if (cart.CustomersName != null && cart.CustomersEmail != null && cart.CustomersAddress != null)
            {
                foreach (var product in cart.ListOfItems)
                {
                    try
                    {
                        DO.Product productData = dal.product.Get(product.itemId);
                        if (product.amount < 0 || product.amount > productData.productAmountInStock)
                        {
                            throw new ProductNotInStock();
                        }                     
                    }
                    catch (Exception ex)
                    {
                        throw new DalException(ex);
                    }

                }
                DO.Order order = new();
                order.dateOrdered = DateTime.Now;
                order.dateDelivered = DateTime.MaxValue;
                order.dateSent = DateTime.MaxValue;
                order.clientEmail = cart.CustomersEmail;
                order.clientName = cart.CustomersName;
                order.addressForDelivery = cart.CustomersAddress;
                
                try
                {
                    int orderId = dal.order.Add(order);
                    foreach (var product in cart.ListOfItems)
                    {
                        DO.OrderItem orderItemData = new();
                        orderItemData.orderItemId = 0;
                        orderItemData.amount = product.amount;
                        orderItemData.priceForUnit = product.priceForUnit;
                        orderItemData.orderId = orderId;
                        orderItemData.itemId = product.itemId;
                        try
                        {
                            dal.orderItem.Add(orderItemData);
                        }
                        catch (Exception ex)
                        {
                            throw new DalException(ex);
                        }
                    }


                }
                catch (Exception ex)
                {

                    throw new DalException(ex);
                }
            }
            foreach (var product in cart.ListOfItems)
            {
                DO.Product productData = dal.product.Get(product.itemId);
                productData.productAmountInStock -= product.amount;
                dal.product.Update(productData); //מעדכן את הכמות של הפריט במלאי בשכבת הנתונים
            }

            cart.ListOfItems.Clear();
            cart.TotalPriceOfCart = 0;
            cart.CustomersName = null;
            cart.CustomersAddress = null;
            cart.CustomersEmail = null;
        }

        public BO.Cart UpdateProduct(BO.Cart cart, int ProductId, int NewAmount)
        {
            
            foreach (var product in cart.ListOfItems)
            {
                if (product.itemId == ProductId)
                {
                    if (product.amount != NewAmount)
                    {
                        DO.Product productData = dal.product.Get(ProductId);
                        if (productData.productAmountInStock >= NewAmount)//יש אפשרות לשנות כמות
                        {
                            cart.TotalPriceOfCart+=  (NewAmount - product.amount) * (product.priceForUnit);//משנה את המחיר לפי ההפרש בין מספר הפריטים שהיו לעכשיו כפול מחיר לפריט

                            product.amount = NewAmount;
                            product.sumPrice =NewAmount * product.priceForUnit;
                            
                            
                        }
                       
                            else
                                throw new ProductNotInStock();
                       }
                        if (product.amount == 0)
                        {
                            cart.ListOfItems.Remove(product);
              
                        }

                    return cart;
                }
            }
            throw new BO.NoSuchProductInCart();

        }
    }
}