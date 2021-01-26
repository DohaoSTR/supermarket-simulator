using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Controls.Stocks
{
    /// <summary>
    /// Interaction logic for DiscountsControl.xaml
    /// </summary>
    public partial class DiscountsControl : UserControl
    {
        private readonly ObservableCollection<Discount> _discounts;

        public DiscountsControl()
        {
            InitializeComponent();

            _discounts = new ObservableCollection<Discount>(DataModel.Discounts);
            dgGrid.ItemsSource = _discounts;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Discount discountCard = new Discount(Convert.ToDecimal(ListBox_Percent.Value), ListBox_DateStart.DisplayDate, ListBox_DateEnd.DisplayDate);
                discountCard.Id = discountCard.Add();
                _discounts.Add(discountCard);
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
            if (dgGrid.SelectedItem is Discount discount)
            {
                discount.Remove();
                _discounts.Remove(dgGrid.SelectedItem as Discount);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is Discount discount)
                {
                    int id = DataModel.Discounts.FindIndex(x => x.Id == discount.Id);

                    int index = _discounts.IndexOf(dgGrid.SelectedItem as Discount);
                    if (index >= 0)
                    {
                        _discounts[index] = new Discount(discount.Id, DataModel.DataAdapter, Convert.ToDecimal(ListBox_Percent.Value), ListBox_DateStart.DisplayDate, ListBox_DateEnd.DisplayDate);
                    }

                    DataModel.Discounts[id] = _discounts[index];
                    discount = _discounts[index];
                    discount.Update();
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
