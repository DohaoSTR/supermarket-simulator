using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Controls.ListOfProducts
{
    /// <summary>
    /// Interaction logic for StoredProductsControl.xaml
    /// </summary>
    public partial class StoredProductsControl : UserControl
    {
        private readonly ObservableCollection<StoredProduct> _storedProducts;

        public StoredProductsControl()
        {
            InitializeComponent();

            _storedProducts = new ObservableCollection<StoredProduct>(DataModel.StoredProducts);
            dgGrid.ItemsSource = _storedProducts;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Product product = new Product(Convert.ToInt32(ListBox_IdOfProduct.Text));

                StoredProduct storedProduct = new StoredProduct(product, Convert.ToDateTime(ListBox_DateArrival.Text), Convert.ToInt32(ListBox_NumberOfProducts.Text));
                storedProduct.Id = storedProduct.Add();
                _storedProducts.Add(storedProduct);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.ParamName);
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }

        private void Remove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dgGrid.SelectedItem is StoredProduct conformDecl)
            {
                conformDecl.Remove();
                _storedProducts.Remove(dgGrid.SelectedItem as StoredProduct);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is StoredProduct storedProduct)
                {
                    int id = DataModel.StoredProducts.FindIndex(x => x.Id == storedProduct.Id);

                    int index = _storedProducts.IndexOf(dgGrid.SelectedItem as StoredProduct);
                    if (index >= 0)
                    {
                        _storedProducts[index] = new StoredProduct(storedProduct.Id, DataModel.DataAdapter,
                            new Product(Convert.ToInt32(ListBox_IdOfProduct.Text)), Convert.ToDateTime(ListBox_DateArrival.Text), Convert.ToInt32(ListBox_NumberOfProducts.Text));
                    }

                    DataModel.StoredProducts[id] = _storedProducts[index];
                    storedProduct = _storedProducts[index];
                    storedProduct.Update();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.ParamName);
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }
    }
}
