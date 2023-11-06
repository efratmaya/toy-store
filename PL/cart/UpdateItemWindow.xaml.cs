using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.cart
{
    /// <summary>
    /// Interaction logic for UpdateItemWindow.xaml
    /// </summary>
    public partial class UpdateItemWindow : Window
    {
        private IBL bl = BlApi.Factory.Get();
        BO.Cart cart = new();
        BO.OrderItem orderItem = new();
        int newAmount;

        public int IdProduct1 { get; }

        public UpdateItemWindow(BO.Cart myCart, BO.OrderItem myOrderitem)
        {
            InitializeComponent();
            cart=myCart;
            orderItem = myOrderitem;
            TextBoxOrderItemId.Text=orderItem.orderItemId.ToString();
            TextBoxItemId.Text=orderItem.itemId.ToString();
            TextBoxItemName.Text=orderItem.itemName.ToString();
            TextBoxPriceForUnit.Text=orderItem.priceForUnit.ToString();
            TextBoxSumPrice.Text=orderItem.sumPrice.ToString();
            TextBoxAmountProduct.Text= orderItem.amount.ToString();
            newAmount = orderItem.amount;
        }

        

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.UpdateProduct(cart, orderItem.itemId, newAmount);
                Close();
            }
            catch {
                MessageBox.Show("חסר במלאי");
               
            }
            
        }

        private void TextBoxAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(TextBoxAmountProduct.Text, out int newAmountInt);
            newAmount = newAmountInt;
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Cart.UpdateProduct(cart, orderItem.itemId, 0);
                Close();
            }
            catch
            {
                MessageBox.Show("אירעה שגיאה");

            }
        }
    }
}
