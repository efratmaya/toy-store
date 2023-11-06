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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private IBL bl = BlApi.Factory.Get();
        BO.Cart cart = new();
        public CartWindow( BO.Cart myCart)
        {
            InitializeComponent();
            cart = myCart;
            CartListView.ItemsSource = cart.ListOfItems;
            TextBoxTotalPrice.Text = cart.TotalPriceOfCart.ToString();
        }

   

        private void checkOut_Click(object sender, RoutedEventArgs e)
        {
            new CheckOutWindow(cart).Show();
            Close();
            //cart = new();
        }

        private void CartListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        
            new UpdateItemWindow(/*bl,*/ cart, ((BO.OrderItem)(sender as ListView).SelectedItem)).ShowDialog();
            CartListView.ItemsSource = "";
            TextBoxTotalPrice.Text = cart.TotalPriceOfCart.ToString();
             CartListView.ItemsSource = cart.ListOfItems;
        }

    }
}
