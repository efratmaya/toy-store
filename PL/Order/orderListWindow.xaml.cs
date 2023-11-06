using BlApi;
using BlImplementation;
using PL.Order;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for orderListWindow.xaml
    /// </summary>
    public partial class orderListWindow : Window
    {
        private IBL bl = BlApi.Factory.Get();
        public orderListWindow()
        {
            InitializeComponent();
            OrderListview.ItemsSource = bl.Order.GetOrders();
        }
        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
      
            new orderWindow(/*bl*/ ((BO.OrderForList)((sender as ListView).SelectedItem)).IdOrder).ShowDialog();
            OrderListview.ItemsSource = "";
            OrderListview.ItemsSource = bl.Order.GetOrders();
        }
    }
}
