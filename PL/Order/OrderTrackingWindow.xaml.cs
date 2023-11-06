using BlApi;
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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        BO.OrderTracking? orderStatus = new();
        int orderID;
       
        private IBL bl = BlApi.Factory.Get();
        public OrderTrackingWindow()
        {
            InitializeComponent();
        }

        private void follow_Click(object sender, RoutedEventArgs e)
        {

            if (TextBoxOrderId.Text == null || TextBoxOrderId.Text =="")

            {
                MessageBox.Show("הכנס מספר הזמנה");
                return;
            }
            try

            {

                orderStatus = bl.Order.orderTracking(orderID);

                dateListView.ItemsSource = orderStatus.OrderConditionWithDate.Keys;
                statusListView.ItemsSource = orderStatus.OrderConditionWithDate.Values;
            }
            catch(Exception ex)
            {
                MessageBox.Show("אירעה שגיאה");
            }


            
        }

        private void TextBoxOrderId_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(TextBoxOrderId.Text, out int orderId);
            orderID = orderId;
        }

        private void ordersDetails_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxOrderId.Text == null || TextBoxOrderId.Text == "")

            {
                MessageBox.Show("הכנס מספר הזמנה");
                return;
            }
            new orderWindow(orderID,false).Show();  
        }
    }
}
