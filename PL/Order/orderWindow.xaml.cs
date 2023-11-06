using BlApi;
using BlImplementation;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for orderWindow.xaml
   
    /// </summary>
    public partial class orderWindow : Window
    {
        BO.Order? order = new();
        private IBL bl = BlApi.Factory.Get();
        public orderWindow(int id, bool edit=true)
        {
            InitializeComponent();
            order = bl.Order.GetOrdersDetails(id);
            TextBoxID.Text = id.ToString();
            TextBoxName.Text = order.clientName;
            TextBoxEmail.Text = order.clientEmail;
            TextBoxAddressForDelivery.Text = order.addressForDelivery;
            TextBoxOrderCondition.Text = order.orderCondition.ToString();
            TextBoxTotalPrice.Text = order.totalPrice.ToString();
            itemsListview.ItemsSource = order.items;
            TextBoxOrdered.Text = order.dateOrdered.ToString();
            if(order.dateSent>DateTime.Now)
            {
                TextBoxSent.Text = "has not sent yet";
            }
            else
            {
                TextBoxSent.Text = order.dateSent.ToString();
                TextBoxSent.IsEnabled = false;
                updateSent.Visibility = Visibility.Collapsed;
                dateSent.IsEnabled = false;
            }
            if (order.dateDelivered > DateTime.Now)
            {
                TextBoxDelivered.Text = "has not delivered yet";
            }
            else
            {
                TextBoxDelivered.Text = order.dateDelivered.ToString();
                TextBoxDelivered.IsEnabled = false;
                updateDelivered.Visibility = Visibility.Collapsed;
                dateDelivered.IsEnabled = false;
            }
            if (edit==false)
            {
                TextBoxSent.IsEnabled = false;
                updateSent.Visibility = Visibility.Collapsed;
                dateSent.IsEnabled = false;
                TextBoxDelivered.IsEnabled = false;
                updateDelivered.Visibility = Visibility.Collapsed;
                dateDelivered.IsEnabled = false;
            }
            
         
        }



        private void UpdateSent(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateSent(order.orderId);
                order = bl.Order.GetOrdersDetails(order.orderId);
                TextBoxSent.Text = order.dateSent.ToString();
                TextBoxSent.IsEnabled = false;
                updateSent.Visibility = Visibility.Collapsed;
                dateSent.IsEnabled = false;


            }
            catch (Exception)
            {
                MessageBox.Show("already sent");
          
            }
        }

        private void UpdateDelivered(object sender, RoutedEventArgs e)
        {
            if(order.dateSent>DateTime.Now)
            {
                MessageBox.Show("ההזמנה עוד לא נשלחה");
                return;

            }
            try
            {
                bl.Order.UpdateDelivered(order.orderId);
                order = bl.Order.GetOrdersDetails(order.orderId);
                TextBoxDelivered.Text = order.dateDelivered.ToString();
                TextBoxDelivered.IsEnabled = false;
                updateDelivered.Visibility = Visibility.Collapsed;
                dateDelivered.IsEnabled = false;


            }
            catch (Exception)
            {
                MessageBox.Show("already delivered");

            }
        }
    }
}
