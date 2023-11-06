using BlApi;
using BO;
using PL.Product;
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
    /// Interaction logic for CheckOutWindow.xaml
    /// </summary>
    public partial class CheckOutWindow : Window
    {
        private IBL bl = BlApi.Factory.Get();
        BO.Cart cart = new();
        public CheckOutWindow(BO.Cart myCart)
        {
            InitializeComponent();
            cart = myCart;
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            cart.CustomersName = TextBoxName.Text;
        }

        private void TextBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            cart.CustomersEmail = TextBoxEmail.Text;
        }

        private void TextBoxAddressForDelivery_TextChanged(object sender, TextChangedEventArgs e)
        {
            cart.CustomersAddress=TextBoxAddressForDelivery.Text;
        }

        private void checkOut_Click(object sender, RoutedEventArgs e)
        {
            if (cart.CustomersAddress == null || cart.CustomersAddress == ""|| cart.CustomersName == null || cart.CustomersName == "" || cart.CustomersEmail == null || cart.CustomersEmail == "")
            {
                MessageBox.Show("אחד מהפרטים לא הוקשו");
                return;
            }
            try
            {
                bl.Cart.CheckOut(cart);
                Close();
            }
            catch(Exception ex)
            {
                //MessageBox.Show(" חסר במלאי מאחד הפרטים ");
                throw ex;
            }
        }
    }
}
