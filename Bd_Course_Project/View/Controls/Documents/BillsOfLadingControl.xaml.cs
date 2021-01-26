using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View
{
    /// <summary>
    /// Interaction logic for BillsOfLadingControl.xaml
    /// </summary>
    public partial class BillsOfLadingControl : UserControl
    {
        private readonly ObservableCollection<BillsOfLading> _billsOfLadings;

        public BillsOfLadingControl()
        {
            InitializeComponent();

            _billsOfLadings = new ObservableCollection<BillsOfLading>(DataModel.BillsOfLadings);
            dgGrid.ItemsSource = _billsOfLadings;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BillsOfLading billOfLading = new BillsOfLading(Convert.ToInt32(ListBox_RegistrationNumber.Text),
                ListBox_Customer.Text, ListBox_Consignor.Text, ListBox_LoadingPoint.Text, ListBox_ShippingPoint.Text);
                billOfLading.Id = billOfLading.Add();
                _billsOfLadings.Add(billOfLading);
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

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (dgGrid.SelectedItem is BillsOfLading billsOfLading)
            {
                billsOfLading.Remove();
                _billsOfLadings.Remove(dgGrid.SelectedItem as BillsOfLading);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is BillsOfLading billsOfLading)
                {
                    int id = DataModel.BillsOfLadings.FindIndex(x => x.Id == billsOfLading.Id);
                    int index = _billsOfLadings.IndexOf(dgGrid.SelectedItem as BillsOfLading);
                    if (index >= 0)
                    {
                        _billsOfLadings[index] = new BillsOfLading(billsOfLading.Id, DataModel.DataAdapter, Convert.ToInt32(ListBox_RegistrationNumber.Text),
                        ListBox_Customer.Text, ListBox_Consignor.Text, ListBox_LoadingPoint.Text, ListBox_ShippingPoint.Text);
                    }

                    DataModel.BillsOfLadings[id] = _billsOfLadings[index];
                    billsOfLading = _billsOfLadings[index];
                    billsOfLading.Update();
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
