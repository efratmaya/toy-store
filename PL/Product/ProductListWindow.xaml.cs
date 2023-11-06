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

namespace PL.Product 
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBL bl = BlApi.Factory.Get();
        public ProductListWindow()
        {
            InitializeComponent();
           
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            ProductListview.ItemsSource = bl.Product.GetProducts();
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListview.ItemsSource = bl.Product.GetProductByCategory((BO.eCategory)CategorySelector.SelectedItem);
            
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            ProductListview.ItemsSource = "";
            ProductListview.ItemsSource = bl.Product.GetProducts();
        }

       

        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            new ProductWindow(/*bl,*/ ((BO.ProductForList)((sender as ListView).SelectedItem)).IdProduct).ShowDialog();

            ProductListview.ItemsSource = "";
            ProductListview.ItemsSource = bl.Product.GetProducts();

        }

        private void ViewAll_Click(object sender, RoutedEventArgs e)
        {
            ProductListview.ItemsSource = bl.Product.GetProducts();
            
        }
    }
}


