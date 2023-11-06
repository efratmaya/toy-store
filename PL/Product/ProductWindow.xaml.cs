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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        BO.Product product = new();
        BO.Cart cart = new();
        private  IBL bl = BlApi.Factory.Get();
        public ProductWindow()
        {
            InitializeComponent();
            ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            confirm.Content = "add product";
        }
        public ProductWindow(  int id)
        {
            InitializeComponent();
            ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));

            product = bl.Product.GetProductsDetails(id);
                TextBoxName.Text = product.productName;
                TextBoxID.Text = product.productId.ToString();
                TextBoxPrice.Text = product.productPrice.ToString();
                TextBoxInStock.Text = product.productAmountInStock.ToString();
                ComboBoxCategory.SelectedItem = (BO.eCategory)product.productCategory;
            confirm.Content = "update product";
            deleteProduct.Visibility = Visibility.Visible;


        }
        public ProductWindow(Cart myCart, ProductItem productItem)
        {
            InitializeComponent();
            ComboBoxCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            ComboBoxCategory.SelectedItem = (BO.eCategory)product.productCategory;
            
            TextBoxName.IsReadOnly =true  ;
            TextBoxID.IsReadOnly = true;
            TextBoxPrice.IsReadOnly = true;
            TextBoxInStock.IsReadOnly = true;
            TextBoxName.IsReadOnly = true;
            ComboBoxCategory.IsEnabled = false;
            confirm.Content = "add to cart";
            confirm.IsEnabled = productItem.InStock;

           
            product = bl.Product.GetProductsDetails(productItem.IdProduct);
            TextBoxName.Text = product.productName;
            TextBoxID.Text = product.productId.ToString();
            TextBoxPrice.Text = product.productPrice.ToString();
            TextBoxInStock.Text = product.productAmountInStock.ToString();
           

            cart= myCart;

        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product.productCategory = (BO.eCategory)ComboBoxCategory.SelectedItem;
        }

      
        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            product.productName = TextBoxName.Text;
        }

        private void TextBoxID_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(TextBoxID.Text, out int idInt);
            product.productId = idInt;
        }

        private void TextBoxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(TextBoxPrice.Text, out double TextBoxPriceDouble);
            product.productPrice = TextBoxPriceDouble;
        }

        private void TextBoxInStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(TextBoxInStock.Text, out int inStockInt);
            product.productAmountInStock = inStockInt;
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            if (confirm.Content == "add product")
            {
                try
                {
                    bl.Product.AddProduct(product);
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("אחד מהפרטים שהכנסת לא נכונים");
                    Close();
                }
            }
            else
            {
                if(confirm.Content == "update product")
                {
                    try
                    {
                        bl.Product.UpdateProduct(product);
                        Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("אחד מהפרטים שהכנסת לא נכונים");
                        Close();
                    }
                }
               
                else// add to cart
                {
                    try
                    {
                       
                        bl.Cart.AddItem(cart, product.productId); 
                        Close();

                        
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("אין מספיק במלאי ");
                        
                        Close();
                    }
                }
            }
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.DeleteProduct(product.productId);
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("אירעה שגיאה");
                Close();
            }
        }
    }
}
