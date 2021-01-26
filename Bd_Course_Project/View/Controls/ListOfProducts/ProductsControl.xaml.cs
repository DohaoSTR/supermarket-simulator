using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View
{
    /// <summary>
    /// Interaction logic for ProductsControl.xaml
    /// </summary>
    public partial class ProductsControl : UserControl
    {
        private readonly ObservableCollection<Product> _products;

        public ProductsControl()
        {
            InitializeComponent();

            _products = new ObservableCollection<Product>(DataModel.Products);
            dgGrid.ItemsSource = _products;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                BillsOfLading billsOfLading = new BillsOfLading(Convert.ToInt32(ListBox_Bills_Of_Lading.Text));
                ConformityCertificate conformityCertificate = new ConformityCertificate(Convert.ToInt32(ListBox_ConformityCertificate.Text));
                ConformityDeclaration conformityDeclaration = new ConformityDeclaration(Convert.ToInt32(ListBox_ConformityDeclaration.Text));
                StateRegistrationCertificate stateRegistrationCertificate = new StateRegistrationCertificate(Convert.ToInt32(ListBox_StateRegistrationCertificate.Text));
                VeterinaryCertificate veterinaryCertificate = new VeterinaryCertificate(Convert.ToInt32(ListBox_VeterinaryCertificate.Text));

                Product product = new Product(ListBox_Name.Text,  Convert.ToDateTime(ListBox_ExpirationDate.Text),
                    Convert.ToInt32(ListBox_NetWeight.Text), Convert.ToInt32(ListBox_GrossWeight.Text), Convert.ToDecimal(ListBox_PurchasePrice.Text), billsOfLading, conformityCertificate,
                    conformityDeclaration, stateRegistrationCertificate, veterinaryCertificate);
                product.Id = product.Add();
                _products.Add(product);
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
            if (dgGrid.SelectedItem is Product product)
            {
                product.Remove();
                _products.Remove(dgGrid.SelectedItem as Product);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is Product product)
                {
                    int id = DataModel.Products.FindIndex(x => x.Id == product.Id);

                    int index = _products.IndexOf(dgGrid.SelectedItem as Product);
                    if (index >= 0)
                    {
                        _products[index] = new Product(product.Id, DataModel.DataAdapter, ListBox_Name.Text,  Convert.ToDateTime(ListBox_ExpirationDate.Text),
                                     Convert.ToInt32(ListBox_NetWeight.Text), Convert.ToInt32(ListBox_GrossWeight.Text), Convert.ToDecimal(ListBox_PurchasePrice.Text),
                                     new BillsOfLading(Convert.ToInt32(ListBox_Bills_Of_Lading.Text)), new ConformityCertificate(Convert.ToInt32(ListBox_ConformityCertificate.Text)),
                                     new ConformityDeclaration(Convert.ToInt32(ListBox_ConformityDeclaration.Text)), new StateRegistrationCertificate(Convert.ToInt32(ListBox_StateRegistrationCertificate.Text)),
                                     new VeterinaryCertificate(Convert.ToInt32(ListBox_VeterinaryCertificate.Text)));
                    }

                    DataModel.Products[id] = _products[index];
                    product = _products[index];
                    product.Update();
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
