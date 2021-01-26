using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Controls.ListOfProducts
{
    /// <summary>
    /// Interaction logic for ArrivalProductsControl.xaml
    /// </summary>
    public partial class ArrivalProductsControl : UserControl
    {
        private readonly ObservableCollection<ArrivalProduct> _arrivalProducts;

        public ArrivalProductsControl()
        {
            InitializeComponent();

            _arrivalProducts = new ObservableCollection<ArrivalProduct>(DataModel.ArrivalProducts);
            dgGrid.ItemsSource = _arrivalProducts;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Product product = new Product(Convert.ToInt32(ListBox_IdOfProduct.Text));

                ArrivalProduct arrivalProduct = new ArrivalProduct(product, Convert.ToDateTime(ListBox_DateArrival.Text), Convert.ToInt32(ListBox_NumberOfProducts.Text));
                arrivalProduct.Id = arrivalProduct.Add();
                _arrivalProducts.Add(arrivalProduct);
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
            if (dgGrid.SelectedItem is ArrivalProduct arrivalProduct)
            {
                arrivalProduct.Remove();
                _arrivalProducts.Remove(dgGrid.SelectedItem as ArrivalProduct);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is ArrivalProduct arrivalProduct)
                {
                    int id = DataModel.ArrivalProducts.FindIndex(x => x.Id == arrivalProduct.Id);

                    int index = _arrivalProducts.IndexOf(dgGrid.SelectedItem as ArrivalProduct);
                    if (index >= 0)
                    {
                        _arrivalProducts[index] = new ArrivalProduct(arrivalProduct.Id, DataModel.DataAdapter,
                            new Product(Convert.ToInt32(ListBox_IdOfProduct.Text)), Convert.ToDateTime(ListBox_DateArrival.Text), Convert.ToInt32(ListBox_NumberOfProducts.Text));
                    }

                    DataModel.ArrivalProducts[id] = _arrivalProducts[index];
                    arrivalProduct = _arrivalProducts[index];
                    arrivalProduct.Update();
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
