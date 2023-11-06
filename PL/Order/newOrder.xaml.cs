using BlApi;
using BO;
using PL.Product;
using PL.cart;
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
    /// Interaction logic for newOrder.xaml
    /// </summary>
    public partial class newOrder : Window
    {

        private IBL bl = BlApi.Factory.Get();
        BO.Cart? cart = new();
        
        public newOrder()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            CatalogListview.ItemsSource = bl.Product.GetCatalog();
           
              cart.ListOfItems = new List<BO.OrderItem>();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CatalogListview.ItemsSource =
                bl.Product.GetCatalogByCategory((BO.eCategory)CategorySelector.SelectedItem);

        }
        private void ViewAll_Click(object sender, RoutedEventArgs e)
        {
            CatalogListview.ItemsSource = bl.Product.GetCatalog();

        }
     
        private void CatalogListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ProductWindow(cart,(BO.ProductItem)((sender as ListView).SelectedItem)).Show();
        }

        private void myCart_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(cart).Show();
        }
    }
}
